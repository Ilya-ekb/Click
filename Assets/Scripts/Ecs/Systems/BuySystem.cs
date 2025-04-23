using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class BuySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter buyFilter;
        private EcsFilter playerFilter;

        private EcsPool<Balance> playerPool;
        private EcsPool<Purchased> purchasedPool;
        private EcsPool<Buy> buyPool;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            playerFilter = world.Filter<Balance>().End();
            buyFilter = world.Filter<Buy>().End();

            playerPool = world.GetPool<Balance>();
            purchasedPool = world.GetPool<Purchased>();
            buyPool = world.GetPool<Buy>();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var buyIndex in buyFilter)
            {
                foreach (var playerIndex in playerFilter)
                {
                    ref var player = ref playerPool.Get(playerIndex);
                    ref var buy = ref buyPool.Get(buyIndex);
                    
                    if (!(player.Value >= buy.Price)) continue;
                    player.Value -= buy.Price;
                    player.View.Update(ref player);
                    purchasedPool.Add(buyIndex);
                }
                buyPool.Del(buyIndex);
            }
        }
    }
}