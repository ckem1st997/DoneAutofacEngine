namespace WebApplication3
{
    public interface ITTT<TEntity> where TEntity : class
    {
        public string GetString(string key);
        public string GetString();
    }
}
