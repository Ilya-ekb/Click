using Data;
using UnityEngine;

namespace Providers
{
    public class PrefDataProvider : IDataProvider
    {
        public void SaveData<T>(string name, T data)
        {
            switch (data)
            {
                case string stringData:
                    PlayerPrefs.SetString(name, stringData);
                    break;
                case int intData:
                    PlayerPrefs.SetInt(name, intData);
                    break;
                case float floatData:
                    PlayerPrefs.SetFloat(name, floatData);
                    break;
            }
        }

        public bool LoadData<T>(string name, out T data)
        {
            data = default;
            switch (data)
            {
                case string:
                {
                    var val = PlayerPrefs.GetString(name, string.Empty);
                    data = (T)(object)val;
                    return !string.IsNullOrEmpty(val);
                }
                case int:
                {
                    var val = PlayerPrefs.GetInt(name, 0);
                    data = (T)(object)val;
                    return !val.Equals(0);
                }
                case float:
                {
                    var val = PlayerPrefs.GetFloat(name, 0);
                    data = (T)(object)val;
                    return !Mathf.Approximately(val, 0f);
                }
                default:
                    return false;
            }
        }
    }
}