using Ecs.Components;
using Ecs.Handlers;
using Leopotam.EcsLite;
using UnityEngine;

namespace Ecs.Systems
{
    public class IncomeSystem : IEcsInitSystem, IEcsPreInitSystem, IEcsRunSystem
    {

        private EcsFilter businessFilter;
        private EcsFilter purchasedBusinessFilter;
        private EcsFilter playerFilter;
        private EcsPool<Business> businessPool;
        private EcsPool<Progress> progressPool;
        private EcsPool<Balance> balancePool;

        private const float emptyProgress = 0f;
        private const float fullProgress = 1f;

        public void Init(EcsSystems systems)
        {
        }

        public void Run(EcsSystems systems)
        {
            foreach (var businessIndex in businessFilter)
            {
                ref var business = ref businessPool.Get(businessIndex);
                ref var progress = ref progressPool.Get(businessIndex);
                progress.View?.Update(ref progress);
                progress.Value += Time.deltaTime / progress.Delay;
                
                if (progress.Value < fullProgress) continue;
                
                foreach (var playerIndex in playerFilter)
                {
                    ref var balance = ref balancePool.Get(playerIndex);
                    balance.Value += business.CurrentIncome;
                    balance.View?.Update(ref balance);
                    balance.Save();
                }

                progress.Value = emptyProgress;
            }
        }

        public void PreInit(EcsSystems systems)
        {
            var world = systems.GetWorld();
            
            businessFilter = world.Filter<Business>().Inc<Progress>().End();
            purchasedBusinessFilter = world.Filter<Business>().Inc<Purchased>().Inc<UpToLevel>().End();
            purchasedBusinessFilter.AddEventListener(new BusinessBuyHandler(world));
            purchasedBusinessFilter.AddEventListener(new BusinessLevelUpHandler(world));
            
            playerFilter = world.Filter<Balance>().End();
            progressPool = world.GetPool<Progress>();
            businessPool = world.GetPool<Business>();
            balancePool = world.GetPool<Balance>();
        }
    }
}