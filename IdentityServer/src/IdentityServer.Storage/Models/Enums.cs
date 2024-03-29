﻿namespace IdentityServer.Models;

/// <summary>
/// Access token types.
/// </summary>
public enum AccessTokenType
{
    /// <summary>
    /// Self-contained Json Web Token
    /// </summary>
    Jwt = 0,

    /// <summary>
    /// Reference token
    /// </summary>
    Reference = 1
}

/// <summary>
/// Token usage types.
/// </summary>
public enum TokenUsage
{
    /// <summary>
    /// Re-use the refresh token handle
    /// </summary>
    ReUse = 0,

    /// <summary>
    /// Issue a new refresh token handle every time
    /// </summary>
    OneTimeOnly = 1
}

/// <summary>
/// Token expiration types.
/// </summary>
public enum TokenExpiration
{
    /// <summary>
    /// Sliding token expiration
    /// </summary>
    Sliding = 0,

    /// <summary>
    /// Absolute token expiration
    /// </summary>
    Absolute = 1
}

/// <summary>
/// Content Security Policy Level
/// </summary>
public enum CspLevel
{
    /// <summary>
    /// Level 1
    /// </summary>
    One = 0,

    /// <summary>
    /// Level 2
    /// </summary>
    Two = 1
}