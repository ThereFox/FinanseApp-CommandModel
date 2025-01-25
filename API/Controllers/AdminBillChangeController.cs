using API.InputObject;
using App.CommandHandlers;
using App.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/admin/")]
    [Controller]
    public class AdminBillChangeController : Controller
    {
        private readonly ICommandHandler<DebetBillCommand> _debetHandler;
        private readonly ICommandHandler<CreditBillCommand> _creditHandler;

        public AdminBillChangeController(
            ICommandHandler<DebetBillCommand> debetHandler,
            ICommandHandler<CreditBillCommand> creditHandler
            )
        {
            _debetHandler = debetHandler;
            _creditHandler = creditHandler;
        }

        [HttpPost("debet")]
        public async Task<IActionResult> AdminDebit([FromBody] DebetRequest request)
        {
            var command = new DebetBillCommand(request.BillId, request.Amount);
            var executeResult = await _debetHandler.HandlAsync(command);

            return Ok(executeResult.IsSuccess);
        }

        [HttpPost("credit")]
        public async Task<IActionResult> AdminCredit([FromBody] CreditRequest request)
        {
            var command = new CreditBillCommand(request.BillId, request.Amount);
            var executeResult = await _creditHandler.HandlAsync(command);

            return Ok(executeResult.IsSuccess);
        }


    }
}
