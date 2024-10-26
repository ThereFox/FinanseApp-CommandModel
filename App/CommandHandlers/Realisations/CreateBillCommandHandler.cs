using App.Commands.Realisations;
using App.Interfaces;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CommandHandlers.Realisations
{
    public class CreateBillCommandHandler : ICommandHandler<CreateBillCommand>
    {
        private readonly IBillStore _billStore;
        private readonly IClientStore _clientStore;

        public CreateBillCommandHandler(IBillStore billStore, IClientStore clientStore)
        {
            _billStore = billStore;
            _clientStore = clientStore;
        }

        public async Task<Result> HandlAsync(CreateBillCommand command)
        {
            var getOwnerResult = await _clientStore.GetById(command.OwnerId);

            if (getOwnerResult.IsFailure)
            {
                return getOwnerResult;
            }

            var createBillResult = Bill.Create(Guid.NewGuid(), getOwnerResult.Value, []);

            if (createBillResult.IsFailure)
            {
                return createBillResult;
            }

            return await _billStore.CreateNew(createBillResult.Value);
        }
    }
}
