using Ecs.Components;
using Leopotam.EcsLite;

namespace Ecs.Handlers
{
    public class BusinessBuyHandler: IEcsFilterEventListener
    {
        private readonly EcsPool<Progress> progressPool;
        private readonly EcsPool<Business> businessPool;
        
        public BusinessBuyHandler(EcsWorld world)
        {
            progressPool = world.GetPool<Progress>();
            businessPool = world.GetPool<Business>();
        }
        
        public void OnEntityAdded(int entity)
        {
            if(progressPool.Has(entity)) return;
            ref var progress = ref progressPool.Add(entity);
            ref var business = ref businessPool.Get(entity);
            progress.BusinessKey = business.Name;
            progress.Delay = business.ProgressDelay;
            progress.DataProvider = business.DataProvider;
            progress.Load();
            business.View?.Update(ref progress);
        }

        public void OnEntityRemoved(int entity)
        {
        }
    }
}