namespace BlackBoxModuleApi.Cache
{
    public interface ICacheManager
    {
        void Set<T>(string key, T data);

        T Get<T>(string key);
    }

}
