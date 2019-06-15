using UnityEngine;

namespace Util
{
    public class FPSCounter : MonoBehaviour
    {
        public float measurePeriod = 0.5f;

        private int _accumulator = 0;
        private float _nextPeriod = 0;
        private int _currentFPS;


        public int CurrentFPS => _currentFPS;

        private void Start()
        {
            _nextPeriod = Time.realtimeSinceStartup + measurePeriod;
        }


        private void Update()
        {
            _accumulator++;

            if (Time.realtimeSinceStartup > _nextPeriod)
            {
                _currentFPS = (int) (_accumulator / measurePeriod);
                _accumulator = 0;
                _nextPeriod += measurePeriod;
            }
        }
    }
}