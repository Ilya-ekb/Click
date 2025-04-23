using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(UpgradeConfig), menuName = "Configs/Upgrade Config")]
    public class UpgradeConfig : ScriptableObject
    {
        public float cost;
        public float incomeMultiplier;
    }
}