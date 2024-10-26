using App.Commands;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CommandHandlers
{
    public interface ICommandHandler<TCommand>
        where TCommand: class, ICommand
    {
        public Task<Result> HandlAsync(TCommand command);
    }
}
