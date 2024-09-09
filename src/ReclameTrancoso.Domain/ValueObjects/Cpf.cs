using System.Text.RegularExpressions;

namespace ReclameTrancoso.Domain.ValueObjects;

public class Cpf
{
    private const string CpfPattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
    public string Numero { get; private set; }

    private Cpf(string numero)
    {
        Numero = numero;
    }

    public static Cpf Create(string cpf)
    {
        if (!Regex.IsMatch(cpf, CpfPattern))
        {
            throw new ArgumentException("CPF no formato inválido. O formato deve ser XXX.XXX.XXX-XX");
        }

        if (!IsValid(cpf))
        {
            throw new ArgumentException("CPF inválido.");
        }

        return new Cpf(cpf);
    }

    public static bool IsValid(string cpf)
    {
        var cpfNumeros = cpf.Replace(".", "").Replace("-", "");

        if (cpfNumeros.Length != 11)
            return false;

        if (new string(cpfNumeros[0], 11) == cpfNumeros)
            return false;

        var soma = 0;
        for (var i = 0; i < 9; i++)
        {
            soma += (cpfNumeros[i] - '0') * (10 - i);
        }

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        soma = 0;
        for (var i = 0; i < 10; i++)
        {
            soma += (cpfNumeros[i] - '0') * (11 - i);
        }

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return cpfNumeros[9] - '0' == digito1 && cpfNumeros[10] - '0' == digito2;
    }

    public override string ToString()
    {
        return Numero;
    }
}
