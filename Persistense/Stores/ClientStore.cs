using App.Interfaces;
using CSharpFunctionalExtensions;
using Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using Persistense.DTOs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistense.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly ApplicationDBContext _dbContext;

        public ClientStore(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<Client>> GetById(Guid id)
        {
            if(await _dbContext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<Client>("database unawaliable");
            }

            var client = await _dbContext
                .Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(ex => ex.Id == id);

            if(client == default)
            {
                return Result.Failure<Client>($"database dont contain client with Id: {id}");
            }

            return client.ToDomain();
        }

        public async Task<Result<Guid>> SaveNew(Client client)
        {
            if (await _dbContext.Database.CanConnectAsync() == false)
            {
                return Result.Failure<Guid>("database unawaliable");
            }

            var clientDTO = client.ToDTO();

            await _dbContext.Clients.AddAsync(clientDTO);
            await _dbContext.SaveChangesAsync();

            return Result.Success(clientDTO.Id);
        }
    }
}
