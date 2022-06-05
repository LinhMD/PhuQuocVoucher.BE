namespace CrudApiTemplate.CustomException
{
    public class UserRequestException : ArgumentException
    {

        public UserRequestException(string error) : base(error)
        {

        }
    }
}
