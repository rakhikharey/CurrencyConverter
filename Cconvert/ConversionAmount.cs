using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cconvert
{
    public class ConversionAmount
    {

        public string fromCur { get; set; }
        public string toCur { get; set; }
        public double amount { get; set; }
        public double rate { get; set; }
        public double convertedamount
        {
            get
            {
                return amount * rate;
            }
        }
    }
}