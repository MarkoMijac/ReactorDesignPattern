using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public interface IReactor
    {
        void AddNode(IReactiveNode node);
        void RemoveNode(IReactiveNode node);
        void CreateDependency(IReactiveNode predecessor, IReactiveNode successor);
        void RemoveDependency(IReactiveNode predecessor, IReactiveNode successor);
        void PerformUpdate();
        void PerformUpdate(IReactiveNode initialNode);
        void PerformUpdate(string member, object ownerObject);
    }
}
