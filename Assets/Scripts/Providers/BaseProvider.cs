using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using Views;

namespace Providers
{
    public abstract class BaseProvider<T> where T : struct, IComponent
    {
        public int EntityId => entityId;
        
        protected IDataProvider dataProvider;
        protected int entityId;
        
        private EcsPool<T> ecsPool;
        
        protected BaseProvider(EcsWorld world, IDataProvider dataProvider)
        {
            entityId = world.NewEntity();
            ecsPool = world.GetPool<T>();
            this.dataProvider = dataProvider;
            ref var comp = ref ecsPool.Add(entityId);
            comp.EntityId = entityId;
            comp.DataProvider = dataProvider;
            comp.Load();
        }
        
        public abstract void SetView(IView view);

        public void Reset()
        {
            GetData().Reset();
            OnReset();
        }
        
        public void Dispose()
        {
            OnDispose();
            GetData().Save();
            entityId = -1;
            ecsPool = null;
            dataProvider = null; 
        }

        protected virtual void OnReset()
        {
        }

        protected virtual void  OnDispose()
        {
        }
        
        protected ref T GetData() => ref ecsPool.Get(entityId);
    }
}