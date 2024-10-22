using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys
{
    public class Client : Entity<Guid>
    {
        public string Name { get; set; }

        protected Client(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Client> Create(Guid id, string name)
        {
            return Result.Success(new Client(id, name));
        }

    }
}
