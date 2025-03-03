using System;
namespace QuickTools.Scripts.TimeSystem
{
    public static class TimeFormat
    {
        public static string FormatTime(float seconds)
        {
            var time = TimeSpan.FromSeconds(seconds);
            string result;
            if (time.Hours > 0)
            {
                result = string.Format("{0}h {1}m {2}s", time.Hours, time.Minutes, time.Seconds);
            }
            else if (time.Minutes > 0)
            {
                result = string.Format("{0}m {1}s", time.Minutes, time.Seconds);
            }
            else
            {
                result = string.Format("{0}s", time.Seconds);
            }
            return result;
        }
    }
}