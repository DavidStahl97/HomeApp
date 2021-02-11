using HomeCloud.Maps.Application.Komoot;
using HomeCloud.Maps.Infrastructure.GPX.Model;
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
        private readonly IGPXSerializer _gpxSerializer;

        public KomootService(IHttpClientFactory clientFactory, IGPXSerializer gpxSerializer)
        {
            _clientFactory = clientFactory;
            _gpxSerializer = gpxSerializer;
        }

        public async Task<IEnumerable<Domain.Tours.Tour>> GetAllTours(string userId, string cookies)
        {
            var toursInfos = await GetToursAsync(userId, cookies);
            toursInfos = toursInfos.Where(x => x.Type == "tour_recorded").ToList();

            var tours = new List<Domain.Tours.Tour>();
            foreach (var info in toursInfos)
            {
                var route = await GetRouteAsync(info.Id, cookies);

                var positions = route.Track.Points.Select(x => new Domain.Tours.Position
                {
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                }).ToList();

                var tour = new Domain.Tours.Tour
                {
                    Info = new Domain.Tours.TourInfo
                    {
                        TourId = info.Id.ToString(),
                        Date = info.Date,
                        Distance = info.Distance,
                        Name = info.Name,
                        ImageUrl = info.MapImagePreview.Source
                    },
                    Route = new Domain.Tours.Route
                    {
                        TourId = info.Id.ToString(),
                        Positions = positions
                    }
                };

                tours.Add(tour);
            }

            return tours;
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

        private async Task<Route> GetRouteAsync(int tourId, string cookies)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{URL}/tours/{tourId}.gpx");
            request.Headers.Add("Cookie", cookies);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            var stream = await response.Content.ReadAsStreamAsync();
            var route = _gpxSerializer.Deserialize(stream);
            return route;
        }
    }
}