using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Handlers
{
    public class BusinessLevelUpHandler : IncomeUpHandler
    {
        private readonly EcsPool<UpToLevel> upToLevelPool;
        private readonly EcsPool<Business> businessPool;
        private readonly EcsPool<Purchased> purchasedPool;
        private readonly EcsPool<Progress> progressPool;

        public BusinessLevelUpHandler(EcsWorld world) : base(world)
        {
            upToLevelPool = world.GetPool<UpToLevel>();
            businessPool = world.GetPool<Business>();
            purchasedPool = world.GetPool<Purchased>();
            progressPool = world.GetPool<Progress>();
        }
        
        public override void OnEntityAdded(int entity)
        {
            var newLevel = upToLevelPool.Get(entity).value;
            SetLevel(entity, newLevel);
            upToLevelPool.Del(entity);
            purchasedPool.Del(entity);
        }

        private void SetLevel(int entity, int level)
        {
            ref var business = ref businessPool.Get(entity);
            business.Level = level;
            business.CurrentLevelUpCost = GetLevelUpCost(business);
            business.CurrentIncome = GetCurrentIncome(business);
            business.View?.Update(ref business);
            business.Save();
        }


        private float GetLevelUpCost(Business business) => (business.Level + 1) * business.BaseCost;
    }
}