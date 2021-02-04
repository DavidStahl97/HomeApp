using BlazorLeaflet.Models;
using HomeCloud.Maps.Client.GPX.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client
{
    public class RouteDrawer
    {
        public IEnumerable<Polygon> Draw(IEnumerable<GPX.Model.Point> routePoints)
        {
            var points = routePoints.Select(x => new PointF(x.Latitude, x.Longitude)).ToList();

            var polygons = points.Skip(1).Select((point, i) =>
            {
                return new Polygon
                {
                    Shape = new[]
                    {
                        new []
                        {
                            points.ElementAt(i),
                            point
                        }
                    }
                };
            }).ToList();

            return polygons;
        }
    }
}
