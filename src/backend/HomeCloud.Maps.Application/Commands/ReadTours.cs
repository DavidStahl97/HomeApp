using HomeCloud.Maps.Application.Database;
using HomeCloud.Maps.Shared.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Application.Commands
{
    public class ReadTours : IReadTours
    {
        private readonly IRepository _repository;

        public ReadTours(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TourInfoDto>> ExecuteAsync(string userId)
        {
            var tours = await _repository.TourInfoCollection.FindAsync(x => x.UserId == userId);

            return tours.Select(x => new TourInfoDto
            {
                TourId = x.TourId,
                Date = x.Date,
                Name = x.Name,
                Distance = x.Distance,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }
    }
}
