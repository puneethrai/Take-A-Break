using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace takeabreak
{
    public struct TimerInfo
    {
        public int breakTimeLeft;
        public int breakDurationLeft;

    }
    public struct TimerConfig
    {
        public int breakTime, breakDuration;
        public bool autoStart;
        public delegate TimerInfo Callback(TimerInfo state);
        public Callback cb;
        public TimerConfig(int ibreakTime, int ibreakDuration, Func<TimerInfo, TimerInfo> icb, bool iautoStart = true)
        {
            breakTime = ibreakTime;
            breakDuration = ibreakDuration;
            autoStart = iautoStart;
            if (icb != null)
            {
                cb = new Callback(icb);
            } else
            {
                cb = null;
            }
        }
    }

    class Timer
    {
        #region private
        private TimerConfig tc;
        private void resetCount()
        {
            
        }
        #endregion
        #region public
        public Timer():this(new TimerConfig(20*60, 20, null))
        {
            
        }
        public Timer(TimerConfig iConfig)
        {
            tc = iConfig;
            if(tc.autoStart)
            {
                start();
            }
        }
        public void pause()
        {

        }
        public void resume()
        {

        }
        public void start()
        {

        }
        public void stop()
        {

        }
        public void addCallback(Func<TimerInfo, TimerInfo> icb)
        {
            if (icb != null)
            {
                tc.cb = new TimerConfig.Callback(icb);
            }
        }
        #endregion
    }
}
