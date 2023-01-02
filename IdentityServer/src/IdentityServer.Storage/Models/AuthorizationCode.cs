using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Models;

/// <summary>
/// Models an authorization code.
/// </summary>
public class AuthorizationCode
{
    /// <summary>
    /// Gets or sets the creation time.
    /// </summary>
    public DateTime CreationTime { get; set; }

    public int Lifetime { get; set; }

    public string ClientId { get; set; }

    public bool isOpenId { get; set; }
}
