
using System.Collections.Generic;

namespace Microsoft.CST.OAT
{
    /// <summary>
    ///     Clause contain an Operation and associated data
    /// </summary>
    public class Clause
    {
        /// <summary>
        /// Create a Clause
        /// </summary>
        /// <param name="operation">The Operation to Perform</param>
        /// <param name="field">Optionally, the path to the field to operate on</param>
        public Clause(Operation operation, string? field = null)
        {
            Operation = operation;
            Field = field;
        }

        public List<string> Data { get; set; } = new List<string>();


        public string? Field { get; set; }

        public string? Label { get; set; }

        public Operation Operation { get; set; }
        /// <summary>
        ///     A string indicating what custom operation should be performed, if Operation is CUSTOM
        /// </summary>
        public string? CustomOperation { get; set; }


    }
}
