using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(BusinessConfig), menuName = "Configs/Business Config")]
    public class BusinessConfig : ScriptableObject
    {
        public int initLevel;
        public float incomeDelay;
        public float baseCost;
        public float baseIncome;
        public UpgradeConfig[] upgrades;
    }
}