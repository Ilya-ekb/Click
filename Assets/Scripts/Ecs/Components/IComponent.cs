using Data;
using Views;

namespace Ecs.Components
{
    public interface IComponent
    {
        int EntityId { set; }
        IView View { set; }
        IDataProvider DataProvider { set; }

        void Load();
        void Save();
        void Reset();
    }
}