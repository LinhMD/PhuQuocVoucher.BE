namespace CrudApiTemplate.CustomException
{
    public class DbQueryException : Exception
    {
        public DbQueryException(string message) : base(message)
        {

        }
    }
}
