using CSharpFunctionalExtensions;
using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.DTOs.Client
{
    public static class ClientConvertExtensions
    {
        public static ClientEntity ToDTO(this Domain.Entitys.Client client)
        {
            return new ClientEntity()
            {
                Id = client.Id,
                Name = client.Name
            };
        }
        public static Result<Domain.Entitys.Client> ToDomain(this ClientEntity client)
        {
            var validateResult = Domain.Entitys.Client.Create(client.Id, client.Name);

            return validateResult;
        }
    }
}
