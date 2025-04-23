using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Providers
{
    [CreateAssetMenu(fileName = nameof(NamesProvider), menuName = "Providers/Names Provider")]
    public class NamesProvider : ScriptableObject
    {
        public string this[string key] => names.GetValueOrDefault(key);

        private Dictionary<string, string> names = new();

        [SerializeField, TableList] private List<NamePair> namesList = new();

        public void Init()
        {
            names = namesList.ToDictionary(k => k.key, v => v.value);
        }
    }

    [Serializable]
    public class NamePair
    {
#if UNITY_EDITOR
        [Dropdown(nameof(GetKeys))]
#endif
        public string key;

        public string value;

#if UNITY_EDITOR
        private const string pathToConfigs = "Assets/Configs/Businesses";

        private TriDropdownList<string> GetKeys()
        {
            var assets =
                AssetDatabase.FindAssets("t:" + nameof(ScriptableObject), new[] { pathToConfigs });
            var result = new TriDropdownList<string>();
            foreach (var guid in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                result.Add(new TriDropdownItem<string>
                {
                    Text = path.Split('/').Last().Split('.').First(),
                    Value = path.Split('/').Last().Split('.').First()
                });
            }

            return result;
        }
#endif
    }
}