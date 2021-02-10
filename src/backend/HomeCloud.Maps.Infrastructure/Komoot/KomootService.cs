using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Infrastructure.Komoot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Infrastructure.Komoot
{
    public class KomootService : IKomootService
    {
        private const string URL = "https://www.komoot.de/api/v007";
        private readonly IHttpClientFactory _clientFactory;

        public KomootService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<Domain.Tours.Tour>> GetAllTours(string userId, string cookies)
        {
            var tours = await GetToursAsync(userId, cookies);
            return tours.Select(x => new Domain.Tours.Tour
            {
                Info = new Domain.Tours.TourInfo
                {
                    UserId = userId,
                    TourId = x.Id.ToString(),
                    Date = x.Date,
                    Distance = x.Distance,
                    Name = x.Name,
                    ImageUrl = x.MapImagePreview.Source
                }
            }).ToList();
        }

        private async Task<IEnumerable<Tour>> GetToursAsync(string userId, string cookies)
        {
            int page = 0;
            bool hasNext = true;
            static string createUrl(string id, int page) =>
                $"{URL}/users/{id}/tours/?page={page}&limit=100";

            var tours = new List<Tour>();

            do
            {
                var root = await GetRootAsync(createUrl(userId, page), cookies);
                tours.AddRange(root.Embedded.Tours);

                if (root.Links is null || root.Links.Next is null)
                {
                    hasNext = false;
                }
                else
                {
                    page++;
                }
            }
            while (hasNext);

            return tours;
        }

        private async Task<Root> GetRootAsync(string url, string cookies)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Cookie", cookies);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var s = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<Root>(s);

            return root;
        }
    }
}
