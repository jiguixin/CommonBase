using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Layer
    {
        public string sysId { get; set; }
        public int layerId { get; set; }
        public string layer { get; set; }
        public List<LayerClass> children { get; set; }

    }
    public class LayerClass
    {
        public int order { get; set; }
        public string name { get; set; }
        public string ico { get; set; }
    }

}