using System.ComponentModel.DataAnnotations;

namespace Api.Validators
{
    public class GreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int valueParsed;
            try
            {
                valueParsed = (int) value;
                return (valueParsed > 0);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }
    }
}