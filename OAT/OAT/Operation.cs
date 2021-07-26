namespace Microsoft.CST.OAT
{
    /// <summary>
    ///     Operations available for Analysis rules.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        ///     Invalid Operation
        /// </summary>
        NoOperation,
        /// <summary>
        ///     Specifies that a custom operation has been specified
        /// </summary>
        Custom,
        /// <summary>
        ///     Generates regular expressions from the Data list provided and tests them against the specified
        ///     field. If any match it is a success.
        /// </summary>
        Regex,

        /// <summary>
        ///     Checks that any value in the Data list or DictData dictionary have a match in the specified
        ///     field's object as appropriate.
        /// </summary>
        Equals,


    }
}
