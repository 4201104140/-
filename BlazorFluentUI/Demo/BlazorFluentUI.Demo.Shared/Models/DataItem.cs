using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFluentUI.Demo.Shared.Models
{
    public class DataItem
    {
        public DataItem(int num)
        {
            Key = num.ToString();
        }

        public string Key { get; set; }
    }
}
