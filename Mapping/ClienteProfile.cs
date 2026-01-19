using AutoMapper;
using Challenge_ABM_Clientes.Domain;
using Challenge_ABM_Clientes.DTOs;

namespace Challenge_ABM_Clientes.Mapping;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteCreateDto, Cliente>();
        CreateMap<ClienteUpdateDto, Cliente>();
    }
}