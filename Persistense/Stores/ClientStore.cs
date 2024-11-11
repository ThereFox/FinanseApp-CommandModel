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
using Application.Events.Realisation;
using Infrastructure.EvensSourcerer.EventProduce.Abstractions;

namespace Persistense.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly ApplicationDBContext _dbContext;

        private readonly IEventProducer<ClientCreatedEvent> _eventProducer;
        
        public ClientStore(ApplicationDBContext context, IEventProducer<ClientCreatedEvent> eventProducer)
        {
            _dbContext = context;
            _eventProducer = eventProducer;
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

            var transaction = await _dbContext.Database.BeginTransactionAsync();
            
            var clientDTO = client.ToDTO();

            await _dbContext.Clients.AddAsync(clientDTO);
            await _dbContext.SaveChangesAsync();

            var eventData = new ClientCreatedEvent()
            {
                ClientId = client.Id,
                ClientName = client.Name
            };
            
            var notifyUpdate = await _eventProducer.ProduceAsync(eventData);

            if (notifyUpdate.IsFailure)
            {
                await transaction.RollbackAsync();
                return Result.Failure<Guid>("cant notify stend");
            }

            await transaction.CommitAsync();
            
            return Result.Success(clientDTO.Id);
        }
    }
}
