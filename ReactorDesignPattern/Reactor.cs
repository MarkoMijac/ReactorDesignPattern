using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactorDesignPattern
{
    public class Reactor : IReactor
    {
        private List<IReactiveNode> Nodes { get; set; } = new List<IReactiveNode>();

        public void AddNode(IReactiveNode node)
        {
            if (Nodes.Contains(node) == false)
            {
                Nodes.Add(node);
            }
        }

        public void RemoveNode(IReactiveNode node)
        {
            Nodes.Remove(node);
        }

        public IReactiveNode GetNode(string member, object ownerObject)
        {
            return Nodes.FirstOrDefault(n => n.Member == member && n.OwnerObject == ownerObject);
        }

        public void CreateDependency(IReactiveNode predecessor, IReactiveNode successor)
        {
            if (predecessor != null && successor != null)
            {
                if (Nodes.Contains(predecessor) == false)
                {
                    Nodes.Add(predecessor);
                }

                if (Nodes.Contains(successor) == false)
                {
                    Nodes.Add(successor);
                }

                predecessor.AddSuccessor(successor);
                successor.AddPredecessor(predecessor);
            }
        }

        public void RemoveDependency(IReactiveNode predecessor, IReactiveNode successor)
        {
            if (predecessor != null && predecessor != null)
            {
                predecessor.RemoveSuccessor(successor);
                successor.RemovePredecessor(predecessor);
            }
        }

        public void PerformUpdate()
        {
            var sortedNodes = Sort(Nodes);
            foreach (var node in sortedNodes)
            {
                node.Update();
            }
        }

        public void PerformUpdate(IReactiveNode initialNode)
        {
            var sortedNodes = Sort(Nodes, initialNode);
            foreach (var node in sortedNodes)
            {
                node.Update();
            }
        }

        private List<IReactiveNode> Sort(List<IReactiveNode> unsortedNodes)
        {
            var sorted = new List<IReactiveNode>();
            var visited = new Dictionary<IReactiveNode, bool>();

            if (unsortedNodes != null)
            {
                foreach (var currentNode in unsortedNodes)
                {
                    Visit(currentNode, visited, sorted);
                }
            }

            sorted.Reverse();
            return sorted;
        }

        private List<IReactiveNode> Sort(IEnumerable<IReactiveNode> unsortedNodes, IReactiveNode initialNode)
        {
            var sorted = new List<IReactiveNode>();
            var visited = new Dictionary<IReactiveNode, bool>();

            if (unsortedNodes != null && unsortedNodes.Contains(initialNode))
            {
                Visit(initialNode, visited, sorted);
            }

            sorted.Reverse();
            return sorted;
        }

        private void Visit(IReactiveNode currentNode, Dictionary<IReactiveNode, bool> visitedNodes, List<IReactiveNode> sortedNodes)
        {
            bool inProcess;
            var alreadyVisited = visitedNodes.TryGetValue(currentNode, out inProcess);

            if (alreadyVisited == true)
            {
                if (inProcess == true)
                {
                    throw new Exception("Cyclic dependencies identifier!");
                }
            }
            else
            {
                visitedNodes[currentNode] = true;

                var successors = currentNode.Successors;
                foreach (var successor in successors)
                {
                    Visit(successor, visitedNodes, sortedNodes);
                }

                visitedNodes[currentNode] = false;
                sortedNodes.Add(currentNode);
            }
        }

        public void PerformUpdate(string member, object ownerObject)
        {
            IReactiveNode initialNode = GetNode(member, ownerObject);
            PerformUpdate(initialNode);
        }
    }
}
