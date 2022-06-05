namespace CrudApiTemplate.CustomException;

public class CodingException : Exception
{
    public CodingException(string message) : base(message)
    {

    }
}