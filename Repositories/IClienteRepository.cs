using Challenge_ABM_Clientes.Domain;

namespace Challenge_ABM_Clientes.Repositories;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAll();
    Task<Cliente?> GetById(Guid id);
    Task<List<Cliente>> SearchByNombre(string nombre);
    Task<bool> ExisteCuit(string cuit);
    Task Add(Cliente cliente);
    Task Update(Cliente cliente);
    Task Delete(Guid id);
}