using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public abstract class ReactiveNode : IReactiveNode
    {
        public int Identifier { get; private set; }
        public string Member { get; private set; }
        public object Owner 
        {
            get
            {
                if (_weakOwner == null || _weakOwner.IsAlive == false)
                {
                    return null;
                }
                return _weakOwner.Target;
            }
        }
        private WeakReference _weakOwner;
        public List<IReactiveNode> Predecessors { get; private set; }
        public List<IReactiveNode> Successors { get; private set; }

        public ReactiveNode(string member, object ownerObject)
        {
            Validate(member, ownerObject);

            Member = member;
            _weakOwner = new WeakReference(ownerObject);

            GenerateIdentifier();

            Predecessors = new List<IReactiveNode>();
            Successors = new List<IReactiveNode>();
        }

        private void Validate(string member, object ownerObject)
        {
            if (member == "")
            {
                throw new ArgumentException("Member has to be specified!");
            }
            else if (ownerObject == null)
            {
                throw new ArgumentException("Object owner has to be specified!");
            }
        }

        protected virtual void GenerateIdentifier()
        {
            Identifier = GetHashCode();
        }

        public abstract void Update();

        public void AddSuccessor(IReactiveNode node)
        {
            ValidateSuccessor(node);
            if (Successors.Contains(node) == false)
            {
                Successors.Add(node);
            }
        }

        private void ValidateSuccessor(IReactiveNode node)
        {
            if (node == null)
            {
                throw new ArgumentException("Successor cannot be null!");
            }
            if (node == this)
            {
                throw new ArgumentException("Node cannot be successor to itself!");
            }
        }

        public void RemoveSuccessor(IReactiveNode node)
        {
            Successors.Remove(node);
        }

        public void AddPredecessor(IReactiveNode node)
        {
            ValidatePredecessor(node);
            if (Predecessors.Contains(node) == false)
            {
                Predecessors.Add(node);
            }
        }

        private void ValidatePredecessor(IReactiveNode node)
        {
            if (node == null)
            {
                throw new ArgumentException("Predecessor cannot be null!");
            }
            if (node == this)
            {
                throw new ArgumentException("Node cannot be predecessor to itself!");
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
            int hashCode = 1675533775;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Member);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Owner);
            return hashCode;
        }
    }
}
