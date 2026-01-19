using Challenge_ABM_Clientes.Data;
using Challenge_ABM_Clientes.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Challenge_ABM_Clientes.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cliente>> GetAll()
    {
        return await _context.Clientes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> GetById(Guid id)
    {
        return await _context.Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Cliente>> SearchByNombre(string nombre)
    {
        var parametro = new SqlParameter("@Nombre", nombre);

        return await _context.Clientes
            .FromSqlRaw("EXEC dbo.sp_SearchClientes @Nombre", parametro)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExisteCuit(string cuit)
    {
        return await _context.Clientes
            .AnyAsync(c => c.CUIT == cuit);
    }

    public async Task Add(Cliente cliente)
    {
        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Cliente cliente)
    {
        _context.Clientes.Update(cliente);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
            return;

        _context.Clientes.Remove(cliente);

        await _context.SaveChangesAsync();
    }
}