using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class BillChangeType : ValueObject
    {
        public static BillChangeType Simple => new BillChangeType(1);
        public static BillChangeType Compilated => new BillChangeType(2);

        private static List<BillChangeType> _allAvaliable = [BillChangeType.Simple, BillChangeType.Compilated];

        public int Value { get; init; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        protected BillChangeType(int value)
        {
            Value = value;
        }

        public static Result<BillChangeType> Create(int value)
        {
            if(_allAvaliable.Any(ex => ex.Value == value) == false)
            {
                return Result.Failure<BillChangeType>("not awaliable value");
            }

            return Result.Success(new BillChangeType(value));
        }

    }
}
