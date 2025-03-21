namespace Jhooa.UI.Features.Companies.Models;

public class Company
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
    public string? Address { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
    public string? City { get; init; }
    
    public List<CompanyCode> Codes { get; init; } = [];

}