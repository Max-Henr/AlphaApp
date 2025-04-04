using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;
public class ClientRepository(AppDBContext context) : BaseRepository<ClientEntity, Client>(context), IClientRepository
{
}
