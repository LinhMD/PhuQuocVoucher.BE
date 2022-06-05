namespace CrudApiTemplate.CustomException
{
    public class ModelNotFoundException<TModel> : UserRequestException
    {
        public ModelNotFoundException(string error): base(error)
        {

        }
    }
}
