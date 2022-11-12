using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.CustomException;

namespace CrudApiTemplate.Utilities
{
    public static class Validation
    {
        public static T Validate<T>(this T obj)
        {

            var vc = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, vc, results, true);
            if (isValid) return obj;
            var error = "";
            foreach (var item in results)
            {
                error += item.ErrorMessage;
                error += "\n";
            }
            throw new ModelValueInvalidException(error);
        }

    }
}
