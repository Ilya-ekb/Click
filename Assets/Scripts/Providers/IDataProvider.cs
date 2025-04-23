namespace Data
{
    public interface IDataProvider
    {
        bool LoadData<T>(string key, out T value);
        void SaveData<T>(string key, T value);
    }
}