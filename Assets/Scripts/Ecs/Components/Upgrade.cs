using Data;
using Views;

namespace Ecs.Components
{
    public struct Upgrade : IComponent
    {
        public int EntityId { get; set; }

        public IView View
        {
            get => view;
            set
            {
                view = value;
                view.Update(ref this);
            }
        }

        public IDataProvider DataProvider
        {
            private get;
            set;
        }
        
        public string Name;
        public int BusinessId;
        public bool IsPurchased;
        public float Cost;
        public float Multiplier;
        
        private IView view;

        public void Load()
        {
            DataProvider?.LoadData(Name, out IsPurchased);
        }

        public void Save()
        {
            DataProvider.SaveData(Name, IsPurchased);
        }

        public void Reset()
        {
            IsPurchased = false;
        }
    }

    public struct UpToLevel
    {
        public int value;
    }
}