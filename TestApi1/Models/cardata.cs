using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApi1.Models
{
    public class cardata
    {
        private string _lat;
        public string lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        private string _lon;

        public string lon
        {
            get { return _lon; }
            set { _lon = value; }
        }

        private int _sd;

        public int sd
        {
            get { return _sd; }
            set { _sd = value; }
        }

        private string _hd;

        public string hd
        {
            get { return _hd; }
            set { _hd = value; }
        }

        private string _time;

        public string time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}