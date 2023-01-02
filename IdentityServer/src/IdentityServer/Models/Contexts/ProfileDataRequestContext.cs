using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Models;

/// <summary>
/// Class describing the profile data request
/// </summary>
public class ProfileDataRequestContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileDataRequestContext"/> class.
    /// </summary>
    public ProfileDataRequestContext()
    { }

    /// <summary>
    /// Gets or sets the validatedRequest.
    /// </summary>
    /// <value>
    /// The validatedRequest.
    /// </value>
    public ValidatedRequest ValidatedRequest { get; set; }
}
