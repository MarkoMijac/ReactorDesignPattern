using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class ConstructionPart
    {
        public List<Layer> Layers { get; set; }

        public string Name { get; set; }
        
        private double _height;
        public double Height
        {
            get { return _height; }
            set 
            {
                if (_height != value)
                {
                    _height = value;
                    var node = ReactorManager.GetDefaultReactor().GetNode(nameof(Height), this);
                    ReactorManager.GetDefaultReactor().PerformUpdate(node);
                }
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set 
            {
                if (_width != value)
                {
                    _width = value;
                    var node = ReactorManager.GetDefaultReactor().GetNode(nameof(Width), this);
                    ReactorManager.GetDefaultReactor().PerformUpdate(node);
                }
            }
        }

        public double SurfaceArea { get; set; }
        public double Thickness { get; set; }

        public ConstructionPart(string name, double w, double h)
        {
            Name = name;
            _width = w;
            _height = h;
        }

        public void Update_SurfaceArea()
        {
            SurfaceArea = Width * Height;
        }
    }
}
