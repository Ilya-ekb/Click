using Data;
using UnityEngine;

namespace Configs
{
    public abstract class BaseDataProviderConfig : ScriptableObject 
    {
        public abstract IDataProvider GetDataProvider();
    }
}