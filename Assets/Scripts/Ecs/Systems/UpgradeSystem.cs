using Ecs.Components;
using Ecs.Handlers;
using Leopotam.EcsLite;

namespace Ecs.Systems
{
    public class UpgradeSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var upgradeFilter = world.Filter<Upgrade>().Inc<Purchased>().End();
            upgradeFilter.AddEventListener(new UpgradeBusinessHandler(world));
        }
    }
}