namespace IdentityServer.EntityFramework.Entities;

public class ApiResourceSecret : Secret
{
    public int ApiResourceId { get; set; }
    public ApiResource ApiResource { get; set; }
}