using DemonstrationExamples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Fakes
{
    public class AlfaMock : Alfa
    {
        public bool Update_B_Invoked { get; set; } = false;
        public override void Update_B()
        {
            base.Update_B();
            Update_B_Invoked = true;
        }
    }
}
