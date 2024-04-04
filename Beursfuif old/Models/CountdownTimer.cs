using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;

namespace Beursfuif.Models
{
    public class CountdownTimer
    {
        private readonly IDispatcher _dispatcher;
        private CancellationTokenSource _cts;

        public int TimeRemaining { get; private set; }
        public event Action<int> TimeUpdated;
        public event Action CountdownCompleted;

        public CountdownTimer(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Start(int countdownFrom)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            TimeRemaining = countdownFrom;

            TimerCallback();
        }

        public void Reset()
        {
            TimeRemaining = 10;
            _cts?.Cancel();
            _dispatcher.Dispatch(() => TimeUpdated?.Invoke(TimeRemaining));
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        private void TimerCallback()
        {
            if (_cts.IsCancellationRequested)
                return;

            _dispatcher.Dispatch(() =>
            {
                if (TimeRemaining > 0)
                {
                    TimeUpdated?.Invoke(TimeRemaining);
                    TimeRemaining--;
                }
                else
                {
                    CountdownCompleted?.Invoke();
                    Stop();
                    return;
                }
            });

            Task.Delay(1000, _cts.Token)
                .ContinueWith(t =>
                {
                    if (!t.IsCanceled)
                    {
                        TimerCallback();
                    }
                }, TaskScheduler.Default); 
        }

    }
}
