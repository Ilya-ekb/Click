using Configs;
using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using Views;

namespace Providers
{
    public class BusinessProvider : PurchasableProvider<Business>
    {
        private readonly EcsPool<Progress> progressPool;
        private readonly EcsPool<UpToLevel> upToLevelPool;
        private readonly BusinessConfig config;
        private UpgradeProvider[] upgradeProviders;

        public BusinessProvider(BusinessConfig config, EcsWorld world, IDataProvider dataProvider) : base(world, dataProvider)
        {
            this.config = config;
            upToLevelPool = world.GetPool<UpToLevel>();
            progressPool = world.GetPool<Progress>();
            GetData().DataProvider = dataProvider;
            
            SetupBaseValues();
            SetupUpgrades(world);
            SetupLevel(world);
        }

        public override void SetView(IView view)
        {
            ref var business = ref GetData();
            business.View = view;

            if (progressPool.Has(entityId))
                business.View.Update(ref progressPool.Get(entityId));

            if (view is not BusinessItem businessItem) return;

            businessItem.OnLevelUp += Purchase;
            for (var index = 0; index < upgradeProviders.Length; index++)
                upgradeProviders[index].SetView(businessItem.GetUpgradeView(index));
        }

        protected override float GetCurrentPrice() => GetData().CurrentLevelUpCost;

        protected override void OnPurchase()
        {
            base.OnPurchase();
            ref var upToLevel = ref upToLevelPool.Add(entityId);
            upToLevel.value = GetData().Level + 1;
        }

        protected override void OnReset()
        {
            base.OnReset();
            if(progressPool.Has(entityId))
                progressPool.Get(entityId).Reset();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            if(progressPool.Has(entityId))
                progressPool.Get(entityId).Save();
        }

        private void SetupBaseValues()
        {
            ref var business = ref GetData();
            business.Name = config.name;
            business.ProgressDelay = config.incomeDelay;
            business.BaseIncome = config.baseIncome;
            business.BaseCost = config.baseCost;
            business.CurrentLevelUpCost = config.baseCost;
            business.Load();
        }

        private void SetupUpgrades(EcsWorld world)
        {
            var upgradesLength = config.upgrades.Length;
            ref var business = ref GetData();
            business.Upgrades = new int[upgradesLength];
            upgradeProviders = new UpgradeProvider[upgradesLength];
            for (var i = 0; i < upgradesLength; i++)
            {
                upgradeProviders[i] = new UpgradeProvider(entityId, config.upgrades[i], world, dataProvider);
                business.Upgrades[i] = upgradeProviders[i].EntityId;
            }
        }

        private void SetupLevel(EcsWorld world)
        {
            var level = GetData().Level;
            level = level <= 0 ? config.initLevel : level;
            if(level <= 0) return;
            var purchasedPool = world.GetPool<Purchased>();
            ref var upToLevel = ref upToLevelPool.Add(entityId);
            upToLevel.value = level;
            purchasedPool.Add(entityId);
        }
    }
}