using System;

namespace UDPListener
{
    public class ActiveTime
    {
        public int MessageCount { get; set; }
        public int ElapsedTime { get; set; }

        public void AddSecond()
        {
            ElapsedTime++;
        }

        public void AddMessage()
        {
            MessageCount++;
        }

        public void Reset()
        {
            MessageCount = ElapsedTime = 0;
        }

        public void ClearMessageCount()
        {
            MessageCount = 0;
        }
        public string GetActiveTimeString()
        {
            return String.Format("Read {0} messages in {1} seconds.", MessageCount, ElapsedTime);
        }
    }
}
