namespace WebApplication3
{
    public class TTT <TEntity> : ITTT<
  TEntity> where TEntity : class
    {
        private string _name;
        public TTT(string key)
        {
            _name = key;
        }
        public string GetString()
        {
            return _name;
        }

        public string GetString(string key)
        {
            return  key;
        }
    }
}
