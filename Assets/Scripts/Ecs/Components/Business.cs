using Data;
using UnityEngine;
using Views;

namespace Ecs.Components
{
    public struct Business : IComponent
    {
        public int EntityId { private get; set; }

        public IView View
        {
            get => view;
            set
            {
                view = value;
                view.Update(ref this);
            }
        }
        
        public IDataProvider DataProvider { get; set; }
        
        public string Name;
        public int Level;
        public float BaseIncome;
        public float BaseCost;
        public float ProgressDelay;
        public float CurrentIncome;
        public float CurrentLevelUpCost;
        public int[] Upgrades;
    
        private IView view;

        public void Load()
        {
            if (DataProvider is not null && DataProvider.LoadData(Name, out int level))
                Level = level;
        }

        public void Save()
        {
            DataProvider?.SaveData(Name, Level);
        }

        public void Reset()
        {
            Level = 0;
        }
    } 
}