using System.ComponentModel.DataAnnotations;

namespace API.InputObject
{
    public class CreditRequest
    {
        public Guid BillId { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Amount {  get; set; }
    }
}
