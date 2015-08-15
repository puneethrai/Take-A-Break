using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace takeabreak
{
    public struct TimerInfo
    {
        public int breakTimeLeft;
        public int breakDurationLeft;
        public TimerInfo(int ibreakTimeLeft, int ibreakDurationLeft)
        {
            breakTimeLeft = ibreakTimeLeft;
            breakDurationLeft = ibreakDurationLeft;
        }

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
        private Boolean paused;
        private DispatcherTimer timer;
        private Int16 ONESECOND = 1;
        private TimerInfo ti;
        #endregion
        #region public
        public Timer():this(new TimerConfig(20*60, 20, null))
        {
            
        }
        public Timer(TimerConfig iConfig)
        {
            tc = iConfig;
            reset();
            if(tc.autoStart)
            {
                start();
            } else
            {
                pause();
            }
        }
        public void pause()
        {
            paused = true;
        }
        public void resume()
        {
            paused = false;
        }
        public Boolean isPaused()
        {
            return paused;
        }
        public void start()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0,ONESECOND);
            timer.Start();
            timer.Tick += Timer_Tick;
            resume();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!this.isPaused())
            {
                if(ti.breakTimeLeft <= 0)
                {
                    ti.breakTimeLeft--;
                } else if (ti.breakDurationLeft <= 0)
                {
                    ti.breakDurationLeft--;
                } else
                {
                    reset();
                }
                if(tc.cb != null)
                {
                    tc.cb(ti);
                }
            }
        }

        public void stop()
        {
            timer.Stop();
        }
        public void reset()
        {
            ti = new TimerInfo(tc.breakTime, tc.breakDuration);
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
