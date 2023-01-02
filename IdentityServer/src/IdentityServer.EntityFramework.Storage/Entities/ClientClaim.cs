using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.EntityFramework.Entities;

public class ClientClaim
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; }
}
