using Data;
using UnityEngine;
using Views;

namespace Ecs.Components
{
    public struct Progress : IComponent
    {
        public int EntityId { get; set; }

        public IView View
        {
            get => progressView;
            set
            {
                progressView = value;
                progressView.Update(ref this);
            }
        }

        public IDataProvider DataProvider { private get; set; }

        public string BusinessKey;
        public float Value;
        public float Delay;

        private IView progressView;

        public void Load()
        {
            var key = BusinessKey + nameof(Progress);
            if (DataProvider is not null && DataProvider.LoadData(key, out float value))
                Value = value;
        }

        public void Save()
        {
            var key = BusinessKey + nameof(Progress);
            DataProvider?.SaveData(key, Value);
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}