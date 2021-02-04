using BlazorLeaflet;
using BlazorLeaflet.Models;
using HomeCloud.Maps.Client.GPX;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Pages
{
    public partial class Index : ComponentBase
    {
        private Map _map;

        private float Lat { get; set; } = 47.5574007f;

        private float Lng { get; set; } = 16.3918687f;

        private PointF _startAt = new PointF(50.433216f, 8.497126f);

        protected override void OnInitialized()
        {
            _map = new Map(jsRuntime);

            _map.Layers.Add(new TileLayer
            {
                UrlTemplate = "https://a.tile.opentopomap.org/{z}/{x}/{y}.png",
                Attribution = "&copy; <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors",
            });
        }

        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            var fileStream = e.File.OpenReadStream();
            var bytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(bytes.AsMemory(0, (int)fileStream.Length));

            var serializer = new GPXSerializer();
            var route = serializer.Deserialize(new MemoryStream(bytes));

            var polygons = new RouteDrawer().Draw(route.Track.Points);

            foreach (var polygon in polygons)
            {
                _map.Layers.Add(polygon);
            }
        }
    }
}
