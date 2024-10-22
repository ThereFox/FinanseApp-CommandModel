using API.InputObject;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminBillChangeController : Controller
    {

        public IActionResult AdminDebit(DebetRequest request)
        {
            return BadRequest();
        }

        public IActionResult AdminCredit(CreditRequest request)
        {
            return BadRequest();
        }


    }
}
