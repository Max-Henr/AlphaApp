﻿using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Domain.Extensions;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    public async Task<ClientResult> GetClientAsync()
    {
        var result = await _clientRepository.GetAllAsync();
        return result.MapTo<ClientResult>();
    }
}
