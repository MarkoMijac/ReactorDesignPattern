using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonstrationExamples
{
    public class Beta
    {
        private Alfa _alfa;
        public int C { get; set; }
        public int D { get; set; }

        public Beta(Alfa alfa)
        {
            _alfa = alfa;
        }

        public void Update_D()
        {
            D = _alfa.A + C;
        }
    }
}
