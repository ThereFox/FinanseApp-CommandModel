using API.InputObject;
using App.CommandHandlers;
using App.Commands.Realisations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Controller]
    [Route("createNew")]
    public class CreateController : Controller
    {
        private readonly ICommandHandler<CreateBillCommand> _createBillCommandHandler;
        private readonly ICommandHandler<CreateClientCommand> _createClientCommandHandler;

        public CreateController(
            ICommandHandler<CreateBillCommand> billHandler,
            ICommandHandler<CreateClientCommand> clientHandler
            )
        {
            _createClientCommandHandler = clientHandler;
            _createBillCommandHandler = billHandler;
        }

        [HttpPost("bill")]
        public async Task<IActionResult> CreateBill(CreateBillRequest request)
        {
            var command = new CreateBillCommand()
            {
                OwnerId = request.ClientId
            };
            var result = await _createBillCommandHandler.HandlAsync(command);

            return Ok(result.IsSuccess ? result.IsSuccess : result.Error);
        }

        [HttpPost("client")]
        public async Task<IActionResult> CreateClient(CreateClientRequest request)
        {
            var command = new CreateClientCommand()
            {
                Name = request.Name
            };
            var result = await _createClientCommandHandler.HandlAsync(command);

            return Ok(result.IsSuccess ? result.IsSuccess : result.Error);
        }
    }
}
