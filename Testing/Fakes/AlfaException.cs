using DemonstrationExamples;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Fakes
{
    public class AlfaException : Alfa
    {
        public override void Update_B()
        {
            base.Update_B();
            throw new NotImplementedException();
        }
    }
}
