using System;
using UnityEngine;

namespace Game.Generic.FunctionTimer
{
    public class FunctionTimer : MonoBehaviour
    {
        private DateTime _startTime;
        private DateTime _pausedTime;
        private DateTime _endTime;

        private bool _timerStarted;
        private bool _timerPaused;

        private bool _isApplicationPaused = false;

        private event Action _onTimerRestart;
        private event Action _onTimerResume;
        private event Action<FunctionTimerVO> _onTimerUpdate;
        private event Action<FunctionTimerVO> _onTimerPause;
        private event Action _onTimerStop;

        private event Action _onTimerEnd;

        public FunctionTimer StartTimer(float seconds, Action onStartCallback = null)
        {
            _endTime = DateTime.Now.AddSeconds(seconds);
            _startTime = DateTime.Now;
            _timerPaused = false;
            _timerStarted = true;

            onStartCallback?.Invoke();

            return this;
        }

        public void RestartTimer()
        {
            _onTimerRestart?.Invoke();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                _isApplicationPaused = pauseStatus;
                if (!_timerPaused && _timerStarted)
                {
                    _pausedTime = DateTime.Now;
                    _onTimerPause?.Invoke(new FunctionTimerVO(GetCurrentProgress(), GetSecondsPassed(), GetTimeLeft()));
                }
            }
            else if (_isApplicationPaused)
            {
                _isApplicationPaused = false;
                if (!_timerPaused && _timerStarted)
                {
                    ResumeTimer();
                }
            }
        }

        public FunctionTimer PauseTimer()
        {
            _pausedTime = DateTime.Now;

            _timerPaused = true;
            _onTimerPause?.Invoke(new FunctionTimerVO(GetCurrentProgress(), GetSecondsPassed(), GetTimeLeft()));

            return this;
        }

        public FunctionTimer ResumeTimer()
        {
            double passedPauseTime = (DateTime.Now - _pausedTime).TotalSeconds;

            _startTime = _startTime.AddSeconds(passedPauseTime);
            _endTime = _endTime.AddSeconds(passedPauseTime);

            _timerPaused = false;
            _onTimerResume?.Invoke();

            return this;
        }

        public FunctionTimer StopTimer()
        {
            _timerStarted = false;
            _onTimerStop?.Invoke();

            return this;
        }

        private void FixedUpdate()
        {
            if (_timerStarted && !_timerPaused)
            {
                _onTimerUpdate?.Invoke(new FunctionTimerVO(GetCurrentProgress(), GetSecondsPassed(), GetTimeLeft()));

                if (DateTime.Now >= _endTime)
                {
                    _timerStarted = false;

                    _onTimerEnd?.Invoke();
                }
            }
        }

        private float GetSecondsPassed()
        {
            return (float)(DateTime.Now - _startTime).TotalSeconds;
        }

        private float GetCurrentProgress()
        {
            return 1f - (float)(_endTime - DateTime.Now).TotalSeconds / (float)(_endTime - _startTime).TotalSeconds;
        }

        private TimeSpan GetTimeLeft()
        {
            TimeSpan timeDifference = _endTime - DateTime.Now;

            return timeDifference;
        }

        #region CallbackFunctions

        public FunctionTimer OnUpdate(Action<FunctionTimerVO> onUpdateCallback)
        {
            _onTimerUpdate += onUpdateCallback;

            return this;
        }

        public FunctionTimer OnPause(Action<FunctionTimerVO> onPauseCallback)
        {
            _onTimerPause += onPauseCallback;

            return this;
        }

        public FunctionTimer OnResume(Action onResumeCallback)
        {
            _onTimerResume += onResumeCallback;

            return this;
        }

        public FunctionTimer OnTimerStop(Action onStopCallback)
        {
            _onTimerStop += onStopCallback;

            return this;
        }

        public FunctionTimer OnRestart(Action onRestartCallback)
        {
            _onTimerRestart += onRestartCallback;

            return this;
        }

        public FunctionTimer OnTimerEnd(Action onStopCallback)
        {
            _onTimerEnd += onStopCallback;

            return this;
        }

        #endregion
    }
}
