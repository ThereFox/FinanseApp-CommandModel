using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface IClientStore
    {
        public Task<Result<Client>> GetById(Guid id);
        public Task<Result<Guid>> SaveNew(Client client);
    }
}
