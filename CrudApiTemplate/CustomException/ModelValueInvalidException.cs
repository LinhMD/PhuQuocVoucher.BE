namespace CrudApiTemplate.CustomException
{
    public class ModelValueInvalidException : UserRequestException 
    {

        public ModelValueInvalidException(Exception ex) : base(ex.Message)
        {

        }

        public ModelValueInvalidException(string error) : base(error)
        {
            
        }
    }
}
