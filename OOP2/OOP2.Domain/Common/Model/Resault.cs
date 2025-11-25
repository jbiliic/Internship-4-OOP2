using OOP2.Domain.Common.Validation;

namespace OOP2.Domain.Common.Model
{
    public class Resault<TValue>
    {
        public TValue Value { get; private set; }
        public ValidationResault ValidationResault { get; private set; }

        public Resault(TValue value, ValidationResault validationResault)
        {
            Value = value;
            ValidationResault = validationResault;
        }
    }
}
