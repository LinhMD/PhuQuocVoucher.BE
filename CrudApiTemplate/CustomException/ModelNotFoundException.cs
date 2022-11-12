namespace CrudApiTemplate.CustomException
{
    public class ModelNotFoundException : UserRequestException
    {
        public ModelNotFoundException(string error): base(error)
        {

        }
    }
}
