using Challenge_ABM_Clientes.Domain;
using Challenge_ABM_Clientes.DTOs;
using Challenge_ABM_Clientes.Services;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_ABM_Clientes.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService _service;

    public ClientesController(ClienteService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtiene todos los clientes
    /// </summary>
    /// <returns>Listado completo de clientes</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _service.GetAll();

        return Ok(new
        {
            message = clientes.Any()
                ? "Clientes encontrados"
                : "No existen clientes registrados",
            total = clientes.Count,
            data = clientes
        });
    }


    /// <summary>
    /// Obtiene un cliente por su identificador
    /// </summary>
    /// <param name="id">ID del cliente</param>
    /// <returns>Cliente encontrado</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cliente = await _service.GetById(id);
        if (cliente == null)
            return NotFound(new { message = "Cliente no encontrado" });

        return Ok(cliente);
    }

    /// <summary>
    /// Busca clientes por nombre (coincidencia parcial)
    /// </summary>
    /// <param name="nombre">Nombre o parte del nombre a buscar</param>
    /// <returns>Listado de clientes que coinciden con el criterio</returns>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Search([FromQuery] string nombre)
    {
        var clientes = await _service.Search(nombre);

        if (!clientes.Any())
        {
            return NotFound(new
            {
                message = $"No se encontraron coincidencias para el nombre ingresado '{nombre}'"
            });
        }

        return Ok(new
        {
            message = "Se encontraron coincidencias",
            total = clientes.Count,
            data = clientes
        });
    }

    /// <summary>
    /// Crea un nuevo cliente
    /// </summary>
    /// <param name="dto">Datos del cliente a crear</param>
    /// <returns>Cliente creado</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] ClienteCreateDto dto)
    {
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            RazonSocial = dto.RazonSocial,
            CUIT = dto.CUIT,
            FechaNacimiento = dto.FechaNacimiento,
            TelefonoCelular = dto.TelefonoCelular,
            Email = dto.Email
        };

        await _service.Add(cliente);

        return CreatedAtAction(
            nameof(GetById),
            new { id = cliente.Id },
            new
            {
                message = "Cliente creado correctamente",
                data = cliente
            }
        );
    }

    /// <summary>
    /// Actualiza los datos básicos de un cliente existente
    /// </summary>
    /// <param name="id">ID del cliente</param>
    /// <param name="dto">Datos a modificar</param>
    /// <response code="200">Cliente actualizado correctamente</response>
    /// <response code="404">Cliente no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClienteUpdateDto dto)
    {
        var cliente = await _service.GetById(id);
        if (cliente == null)
            return NotFound(new
            {
                message = "Cliente no encontrado"
            });

        cliente.Nombre = dto.Nombre;
        cliente.Apellido = dto.Apellido;
        cliente.TelefonoCelular = dto.TelefonoCelular;
        cliente.Email = dto.Email;

        await _service.Update(cliente);

        return Ok(new
        {
            message = "Cliente actualizado correctamente"
        });
    }

    /// <summary>
    /// Elimina un cliente por su identificador
    /// </summary>
    /// <param name="id">ID del cliente</param>
    /// <response code="200">Cliente eliminado correctamente</response>
    /// <response code="404">Cliente no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);

        return Ok(new
        {
            message = "Cliente eliminado correctamente"
        });
    }

}