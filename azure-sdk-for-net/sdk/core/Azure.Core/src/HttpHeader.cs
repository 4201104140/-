using System;

namespace Azure.Core
{
    /// <summary>
    /// Represents an HTTP header.
    /// </summary>
    public readonly struct HttpHeader : IEquatable<HttpHeader>
    {
        /// <summary>
        /// Creates a new instance of <see cref="HttpHeader"/> with provided name and value.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="value">The header value.</param>
        public HttpHeader(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"{nameof(name)} shouldn't be null or empty", nameof(name));
            }

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets header name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets header value. If the header has multiple values they would be joined with a comma. To get separate values use <see cref=""/>
        /// </summary>
        public string Value { get;}

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = new HashCodeBuilder();
            hashCode.Ad
        }
    }
}
