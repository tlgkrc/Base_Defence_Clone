using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static void SaveValue <T>(string key,T value)
        {
            ES3.Save(key,value);
        }

        public static T LoadValue<T>(string key, T defaultValue)
        {
            return ES3.Load<T>(key, defaultValue);
        }
    }
}