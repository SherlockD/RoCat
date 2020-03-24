using System;

namespace Game.Generic.FunctionTimer
{
    public struct FunctionTimerVO
    {
        public float Progress { get; }
        public float SecondsPassed { get; }
        public TimeSpan Time { get; }

        public FunctionTimerVO(float progress, float secondsPassed, TimeSpan time)
        {
            Progress = progress;
            SecondsPassed = secondsPassed;
            Time = time;
        }
    }
}

