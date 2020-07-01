using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class Layer
    {
        public string Name { get; set; }

        private double _thickness;

        public double Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public Layer(string name, double t)
        {
            Name = name;
            Thickness = t;
        }
    }
}
