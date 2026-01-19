namespace Challenge_ABM_Clientes.Domain;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string RazonSocial { get; set; } = string.Empty;
    public string CUIT { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string TelefonoCelular { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}