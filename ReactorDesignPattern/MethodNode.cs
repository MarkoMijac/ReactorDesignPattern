using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public class MethodNode : ReactiveNode
    {
        public MethodNode(string member, object ownerObject) : base(member, ownerObject)
        {
        }

        public override void Update()
        {
            Type type = Owner.GetType();
            MethodInfo method = type.GetMethod(Member);
            if (method != null)
            {
                method.Invoke(Owner, null);
            }
        }
    }
}
