using System.ComponentModel.DataAnnotations;

namespace Challenge_ABM_Clientes.DTOs;

public class ClienteUpdateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono celular es obligatorio")]
    [RegularExpression(@"^\d{10,15}$", ErrorMessage = "El teléfono debe contener solo números")]
    public string TelefonoCelular { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    public string Email { get; set; } = string.Empty;
}
