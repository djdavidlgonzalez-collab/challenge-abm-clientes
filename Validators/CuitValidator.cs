using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Challenge_ABM_Clientes.Validators;

public class CuitValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("El CUIT es obligatorio");

        var cuit = value.ToString()!.Replace("-", "");

        if (!Regex.IsMatch(cuit, @"^\d{11}$"))
            return new ValidationResult("El CUIT debe tener 11 dígitos numéricos");

        int[] coef = { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
        int sum = 0;

        for (int i = 0; i < coef.Length; i++)
            sum += int.Parse(cuit[i].ToString()) * coef[i];

        int mod = 11 - (sum % 11);
        int digito = mod == 11 ? 0 : mod == 10 ? 9 : mod;

        return digito == int.Parse(cuit[10].ToString())
            ? ValidationResult.Success
            : new ValidationResult("El CUIT ingresado no es válido");
    }
}