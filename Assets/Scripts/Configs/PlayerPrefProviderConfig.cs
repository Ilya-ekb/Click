using Data;
using Providers;
using TriInspector;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(PrefDataProvider), menuName = "Providers/Prefs Data Config")]
    public class PlayerPrefProviderConfig : BaseDataProviderConfig
    {
        public override IDataProvider GetDataProvider() => new PrefDataProvider();

        [Button("Clear Prefs")]
        private void ClearData() => PlayerPrefs.DeleteAll();
    }
}