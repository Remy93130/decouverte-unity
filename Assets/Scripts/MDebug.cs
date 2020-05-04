using UnityEngine;

namespace Tools
{
    public static class MDebug
    {
        public static void Log(string msg, Object obj=null)
        {
            Debug.Log(string.Format("<{0}><{1}><{2}>", Time.frameCount, msg, obj == null ? "null": obj.GetType().ToString()));
        }
    }

}
