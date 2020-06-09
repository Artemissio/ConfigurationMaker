using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationMaker
{
    public class ConfigModel
    {
        public string StartUrl { get; set; }
        public string Point { get; set; }
        public string PointKey { get; set; }

        public ConfigModel()
        {

        }

        public ConfigModel(string url, string point, string pointkey)
        {
            StartUrl = url;
            Point = point;
            PointKey = pointkey;
        }
    }
}
