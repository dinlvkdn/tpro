using System.Diagnostics;

namespace trpo.Lab2
{
    public class Timer
    {
        private readonly Stopwatch stopwatch;

        public Timer()
        {
            stopwatch = new Stopwatch();
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Stop()
        {
            stopwatch.Stop();
        }

        public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;
    }
}
