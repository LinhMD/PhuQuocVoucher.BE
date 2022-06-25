namespace CrudApiTemplate.CustomException;

public class DbQueryException : Exception
{
    public DbError Error { get; init; }

    public DbQueryException(string message, DbError errorCode) : base(message)
    {
        Error = errorCode;
    }
}

public enum DbError
{
    Create,
    Update,
    Delete
}