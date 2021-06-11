
namespace Yarp.ReverseProxy.Configuration
{
    /// <summary>
    /// How to match header values.
    /// </summary>
    public enum HeaderMatchMode
    {
        /// <summary>
        /// The header must match in its entirety, subject to case sensitivity settings.
        /// Only single headers are supported. If there are multiple headers with the same name when the match fails.
        /// </summary>
        ExactHeader,

        /// <summary>
        /// The header must match by prefix, subject to case sensitivity settings.
        /// Only single headers are supported. If there are multiple headers with the same name then the match fails.
        /// </summary>
        HeaderPrefix,

        /// <summary>
        /// The header must exist and contain any non-empty value.
        /// </summary>
        Exists,
    }
}
