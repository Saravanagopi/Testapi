using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApi1.Models
{
    public class Vehicleparam
    {
        private string _number;
        public string id
        {
            get { return _number; }
            set { _number = value; }
        }

        public List<cardata> cardata { get; set; }
    }
}