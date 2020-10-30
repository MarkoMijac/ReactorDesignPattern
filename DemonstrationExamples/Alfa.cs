using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonstrationExamples
{
    public class Alfa
    {
        public int A { get; set; }
        public int B { get; set; }

        public virtual void Update_B()
        {
            B = A + 10;
        }
    }
}
