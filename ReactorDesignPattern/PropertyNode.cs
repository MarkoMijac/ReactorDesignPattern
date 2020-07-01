using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public class PropertyNode : ReactiveNode
    {
        public PropertyNode(string member, object ownerObject) : base(member, ownerObject)
        {
        }

        public override void Update()
        {
            Type type = OwnerObject.GetType();
            MethodInfo method = type.GetMethod($"Update_{Member}");
            if (method != null)
            {
                method.Invoke(OwnerObject, null);
            }
        }
    }
}
