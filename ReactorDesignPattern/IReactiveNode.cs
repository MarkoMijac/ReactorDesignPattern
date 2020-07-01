using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public interface IReactiveNode
    {
        List<IReactiveNode> Predecessors { get; }
        List<IReactiveNode> Successors { get;}

        int Identifier { get; }
        string Member { get; }
        object OwnerObject { get; }

        void Update();
        void AddSuccessor(IReactiveNode node);
        void RemoveSuccessor(IReactiveNode node);
        void AddPredecessor(IReactiveNode node);
        void RemovePredecessor(IReactiveNode node);
    }
}
