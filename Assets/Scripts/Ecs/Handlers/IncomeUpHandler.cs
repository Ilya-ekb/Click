using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Handlers
{
    public abstract class IncomeUpHandler : IEcsFilterEventListener
    {
        protected readonly EcsPool<Upgrade> upgradePool;
        private readonly EcsPool<Purchased> purchasedPool;

        protected IncomeUpHandler(EcsWorld world)
        {
            upgradePool = world.GetPool<Upgrade>();
            purchasedPool = world.GetPool<Purchased>();
        }
        
        public virtual void OnEntityAdded(int entity)
        {
        }

        public virtual void OnEntityRemoved(int entity)
        {
        }
        
        protected float GetCurrentIncome(Business business)
        {
            return business.Level * business.BaseIncome * GetUpgradeMultiply(business);
        }

        private float GetUpgradeMultiply(Business business)
        {
            var value = 1f;
            foreach (var index in business.Upgrades)
            {
                var upgrade = upgradePool.Get(index);
                value += purchasedPool.Has(upgrade.EntityId) ? upgrade.Multiplier : 0;
            }

            return value;
        }

    }
}