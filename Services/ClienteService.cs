using Challenge_ABM_Clientes.Domain;
using Challenge_ABM_Clientes.Repositories;

namespace Challenge_ABM_Clientes.Services;

public class ClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Cliente>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Cliente?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<List<Cliente>> Search(string nombre)
    {
        return await _repository.SearchByNombre(nombre);
    }

    public async Task Add(Cliente cliente)
    {
        if (await _repository.ExisteCuit(cliente.CUIT))
            throw new InvalidOperationException("Ya existe un cliente con el CUIT ingresado.");

        await _repository.Add(cliente);
    }

    public async Task Update(Cliente cliente)
    {
        await _repository.Update(cliente);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }
}