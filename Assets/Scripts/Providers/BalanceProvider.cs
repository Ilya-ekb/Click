using Data;
using Ecs.Components;
using Leopotam.EcsLite;
using Views;

namespace Providers
{
    public class BalanceProvider : BaseProvider<Balance>
    {
        public BalanceProvider(EcsWorld world, IDataProvider dataProvider) : base(world, dataProvider)
        {
        }

        public override void SetView(IView view)
        {
            GetData().View = view;
        }
    }
}