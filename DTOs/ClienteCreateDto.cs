using System.ComponentModel.DataAnnotations;
using Challenge_ABM_Clientes.Validators;

namespace Challenge_ABM_Clientes.DTOs;

public class ClienteCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La razón social es obligatoria")]
    [MaxLength(150, ErrorMessage = "La razón social no puede superar los 150 caracteres")]
    public string RazonSocial { get; set; } = string.Empty;

    [Required(ErrorMessage = "El CUIT es obligatorio")]
    [CuitValidator]
    public string CUIT { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no tiene un formato válido")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El teléfono celular es obligatorio")]
    [MaxLength(30, ErrorMessage = "El teléfono celular no puede superar los 30 caracteres")]
    public string TelefonoCelular { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [MaxLength(150, ErrorMessage = "El email no puede superar los 150 caracteres")]
    public string Email { get; set; } = string.Empty;
}