using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CST.OAT.VehicleDemo
{
    public static class VehicleDemoClasses
    {


    }

    public class VehicleRule : Rule
    {
        public int Cost;

        public VehicleRule(string name) : base(name)
        {
        }
    }
}
