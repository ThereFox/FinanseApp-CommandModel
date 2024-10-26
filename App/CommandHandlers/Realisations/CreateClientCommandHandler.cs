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
    internal class CreateClientCommandHandler : ICommandHandler<CreateClientCommand>
    {
        private readonly IClientStore _clientStore;
        public CreateClientCommandHandler(IClientStore store)
        {
            _clientStore = store;
        }
        public async Task<Result> HandlAsync(CreateClientCommand command)
        {
            var createClientResult = Client.Create(Guid.NewGuid(), command.Name);

            if (createClientResult.IsFailure)
            {
                return createClientResult;
            }

            return await _clientStore.SaveNew(createClientResult.Value);
        }
    }
}
