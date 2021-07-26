
using Microsoft.CST.OAT.VehicleDemo;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.CST.OAT.Blazor
{
    public class AppState
    {
        public List<Assembly> Assemblies { get; set; } = new List<Assembly>();

        public List<Rule> Rules { get; set; } = new List<Rule>();
        public List<object> TestObjects { get; set; } = new List<object>();

        public List<Rule> DemoRules { get; set; } = new List<Rule>()
        {
            new VehicleRule("Overweight Truck by Delegate")
            {
                Cost = 50,
                Severity = 9,
                Expression = "Overweight AND IsTruck",
                Target = "Vehicle",
                Clauses = new List<Clause>()
                {
                    new Clause(Operation.Custom)
                    {
                        Label = "Overweight",
                        CustomOperation = "OverweightOperation"
                    },
                    new Clause(Operation.Equals, "VehicleType")
                    {
                        Label = "IsTruck",
                        Data = new List<string>()
                        {
                            "Truck"
                        }
                    }
                }
            },
        };
    }
}
