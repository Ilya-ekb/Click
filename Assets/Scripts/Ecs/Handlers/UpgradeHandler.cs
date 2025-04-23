using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Handlers
{
    public class UpgradeBusinessHandler : IncomeUpHandler
    {
        private readonly EcsPool<Business> businessPool;
        public UpgradeBusinessHandler(EcsWorld world) : base(world)
        {
            businessPool = world.GetPool<Business>();
        }
        
        public override void OnEntityAdded(int entity)
        {
            ref var upgrade = ref upgradePool.Get(entity);
            upgrade.IsPurchased = true;
            upgrade.View.Update(ref upgrade);
            SetIncome(businessPool.Get(upgrade.BusinessId));
        }

        private void SetIncome(Business business)
        {
            business.CurrentIncome = GetCurrentIncome(business);
            business.View?.Update(ref business);
        }
    }
}