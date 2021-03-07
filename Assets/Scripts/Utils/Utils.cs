using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static Vector2 ScreenBounds(Camera cam)
        {
            return cam.ScreenToWorldPoint(Screen.safeArea.size);
        }
        
        public static T Clone<T>(T source)
        {
            var serialized = JsonUtility.ToJson(source);
            return JsonUtility.FromJson<T>(serialized);
        }
    }
}
