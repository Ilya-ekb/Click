using Data;
using Ecs.Components;
using Leopotam.EcsLite;

namespace Providers
{
    public abstract class PurchasableProvider<T> : BaseProvider<T> where T : struct, IComponent
    {
        private readonly EcsPool<Buy> buyPool;

        protected PurchasableProvider(EcsWorld world, IDataProvider dataProvider) : base(world, dataProvider)
        {
            buyPool = world.GetPool<Buy>();
        }
        
        protected void Purchase()
        { 
            ref var buy = ref buyPool.Add(entityId);
            buy.Price = GetCurrentPrice();
            OnPurchase();
        }

        protected abstract float GetCurrentPrice();
        protected virtual void OnPurchase(){}
    }
}