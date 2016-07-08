using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterUI
{
    public class ddlFree
    {
        public ddlFree(int value,string display)
        {
            this.Value = value;
            this.DisPlay = display;
        }
        public int Value { get; set; }
        public string DisPlay { get; set; }
    }
}
