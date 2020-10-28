using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public abstract class ReactiveNode : IReactiveNode
    {
        public uint Identifier { get; private set; }
        public string Member { get; private set; }
        public object Owner { get; private set; }
        public List<IReactiveNode> Predecessors { get; private set; } = new List<IReactiveNode>();
        public List<IReactiveNode> Successors { get; private set; } = new List<IReactiveNode>();

        public ReactiveNode(string member, object ownerObject)
        {
            Member = member;
            Owner = ownerObject;

            GenerateIdentifier();
        }

        protected virtual void GenerateIdentifier()
        {
            if (Owner != null && Member != "")
            {
                Identifier = (uint)(Owner.GetHashCode() ^ Member.GetHashCode());
            }
        }

        public abstract void Update();

        public void AddSuccessor(IReactiveNode node)
        {
            if (node != null && node != this && Successors.Contains(node) == false)
            {
                Successors.Add(node);
            }
        }

        public void RemoveSuccessor(IReactiveNode node)
        {
            Successors.Remove(node);
        }

        public void AddPredecessor(IReactiveNode node)
        {
            if (node != null && node != this && Predecessors.Contains(node) == false)
            {
                Predecessors.Add(node);
            }
        }

        public void RemovePredecessor(IReactiveNode node)
        {
            Predecessors.Remove(node);
        }

        public override bool Equals(object obj)
        {
            return obj is ReactiveNode node &&
                   Member == node.Member &&
                   EqualityComparer<object>.Default.Equals(Owner, node.Owner);
        }

        public override int GetHashCode()
        {
            int hashCode = 1536950782;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Member);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Owner);
            return hashCode;
        }
    }
}
