using System.Text;
using Jhooa.UI.Data;
using Microsoft.EntityFrameworkCore;

namespace Jhooa.UI.Services;

public interface ICodeGeneratorService
{
    Task<string> GenerateUniqueCodeAsync();
}

public class CodeGeneratorService (ApplicationDbContext context) : ICodeGeneratorService
{
    private static readonly Random Random = new();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    public async Task<string> GenerateUniqueCodeAsync()
    {
        string code;

        do
        {
            code = GenerateCode();
        }
        while (await CodeExistsAsync(code));

        return code;
    }

    private static string GenerateCode()
    {
        var sb = new StringBuilder(9);

        for (int i = 0; i < 8; i++)
        {
            sb.Append(Chars[Random.Next(Chars.Length)]);
            if (i == 3) sb.Append('-');
        }

        return sb.ToString();
    }

    private async Task<bool> CodeExistsAsync(string code)
    {
        return await context.CompanyCodes.AnyAsync(c => c.Code == code);
    }
}