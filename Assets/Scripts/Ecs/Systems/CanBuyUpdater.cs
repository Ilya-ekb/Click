using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class CanBuyUpdater : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter playerFilter;
        private EcsFilter businessFilter;
        private EcsFilter upgradeFilter;
        
        private EcsPool<Balance> playerPool;
        private EcsPool<Business> businessPool;
        private EcsPool<Upgrade> upgradePool;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            upgradeFilter = world.Filter<Upgrade>().End();
            businessFilter = world.Filter<Business>().End();
            playerFilter = world.Filter<Balance>().End();
            upgradePool = world.GetPool<Upgrade>();
            businessPool = world.GetPool<Business>();
            playerPool = world.GetPool<Balance>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var playerIndex in playerFilter)
            {
                ref var player = ref playerPool.Get(playerIndex);
                foreach (var businessIndex in businessFilter)
                {
                    ref var business = ref businessPool.Get(businessIndex);
                    business.View?.Update(ref player);
                }

                foreach (var upgradeIndex in upgradeFilter)
                {
                    ref var upgrade = ref upgradePool.Get(upgradeIndex);
                    upgrade.View?.Update(ref player);
                }
            }
        }
    }
}