using Configs;
using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using Views;

namespace Providers
{
    public class UpgradeProvider : PurchasableProvider<Upgrade>
    {
        public UpgradeProvider(int businessId, UpgradeConfig config, EcsWorld world, IDataProvider dataProvider) : base(world, dataProvider)
        {
            ref var upgrade = ref GetData();
            upgrade.Name = config.name;
            upgrade.Cost = config.cost;
            upgrade.BusinessId = businessId;
            upgrade.Multiplier = config.incomeMultiplier / 100f;
            GetData().Load();
            if (upgrade.IsPurchased)
                world.GetPool<Purchased>().Add(entityId);
        }

        public override void SetView(IView view)
        {
            if (view is UpgradeItem upgradeItem)
                upgradeItem.OnUpgradeClick += Purchase;
            GetData().View = view;
        }

        protected override float GetCurrentPrice() => GetData().Cost;
    }
}