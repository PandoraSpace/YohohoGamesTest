namespace Client.Services
{
    public class Timer
    {
        private float _time;
        private float _currentTime;

        public bool IsTimeElapsed => _currentTime >= _time;

        public Timer(float time)
        {
            _time = time;
        }

        public void Timing(float time)
        {
            _currentTime += time;
        }

        public void ReleaseTimer()
        {
            _currentTime = 0f;
        }
    }
}