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

        uint Identifier { get; }
        string Member { get; }
        object Owner { get; }

        void Update();
        void AddSuccessor(IReactiveNode node);
        void RemoveSuccessor(IReactiveNode node);
        void AddPredecessor(IReactiveNode node);
        void RemovePredecessor(IReactiveNode node);
    }
}
