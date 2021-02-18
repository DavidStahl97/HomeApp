using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Client.Components
{
    public sealed class Timer : ComponentBase, IDisposable
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer();

        [Parameter]
        public double TimeInSeconds { get; set; }

        [Parameter]
        public EventCallback Tick { get; set; }

        public void Dispose()
        {
            _timer.Stop();  
        }

        protected override void OnInitialized()
        {
            _timer.Elapsed += (sender, e) => InvokeAsync(() => Tick.InvokeAsync());
            _timer.Interval = TimeSpan.FromSeconds(TimeInSeconds).TotalMilliseconds;
            _timer.Start();
        }
    }
}
