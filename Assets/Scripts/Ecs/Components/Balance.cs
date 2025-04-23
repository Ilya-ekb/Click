using Data;
using Views;

namespace Ecs.Components
{
    public struct Balance : IComponent
    {
        public int EntityId { get; set; }

        public IView View
        {
            get => balanceView;
            set
            {
                balanceView = value;
                balanceView.Update(ref this);
            }
        }
        
        public IDataProvider DataProvider { private get; set; }
        
        public float Value;

        private IView balanceView;
        
        public void Load()
        {
            DataProvider?.LoadData(nameof(Balance), out Value);
        }

        public void Save()
        {
            DataProvider?.SaveData(nameof(Balance), Value);
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}