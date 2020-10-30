using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemonstrationExamples
{
    public class Gama
    {
        private Alfa _alfa;
        private Beta _beta;

        public int E { get; set; }
        public int F { get; set; }

        public Gama(Alfa alfa, Beta beta)
        {
            _alfa = alfa;
            _beta = beta;
        }

        public void Update_E()
        {
            E = _alfa.A + _alfa.B;
        }

        public void Update_F()
        {
            F = _alfa.B + _beta.D + E;
        }
    }
}
