using Ecs.Components;

namespace Views
{
    public interface IView
    {
        void Update<TData>(ref TData data) where TData : IComponent;
    }
}