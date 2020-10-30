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
        public string Identifier { get; private set; }

        public List<IReactiveNode> Nodes { get; private set; } = new List<IReactiveNode>();

        public List<IReactiveNode> LastUpdateLog;

        public Reactor(string identifier)
        {
            Identifier = identifier;
        }

        public IReactiveNode GetNode(string member, object owner)
        {
            return Nodes.FirstOrDefault(n => n.Member == member && n.Owner == owner);
        }

        public IReactiveNode GetNode(int identifier)
        {
            return Nodes.FirstOrDefault(n => n.Identifier == identifier);
        }

        public void AddNode(IReactiveNode node)
        {
            if (node == null)
            {
                throw new ArgumentException("Cannot add null node!");
            }

            if (Nodes.Contains(node) == false)
            {
                Nodes.Add(node);
            }
        }

        public void RemoveNode(IReactiveNode node)
        {
            Nodes.Remove(node);
        }

        public void CreateDependency(IReactiveNode predecessor, IReactiveNode successor)
        {
            ValidateDependency(predecessor, successor);

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

        private void ValidateDependency(IReactiveNode predecessor, IReactiveNode successor)
        {
            if (predecessor == null)
            {
                throw new ArgumentException("Predecessor node cannot be null!");
            }

            if (successor == null)
            {
                throw new ArgumentException("Successor node cannot be null!");
            }

            if (predecessor == successor)
            {
                throw new ArgumentException("Predecessor and successor cannot be the node!");
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
            try
            {
                OnUpdateStarted();

                var sortedNodes = Sort(Nodes);
                UpdateNodes(sortedNodes);

                OnUpdateSuccessful();
            }
            catch (Exception)
            {
                OnUpdateFailed();
            }
        }

        private void UpdateNodes(List<IReactiveNode> sortedNodes)
        {
            foreach (var node in sortedNodes)
            {
                node.Update();
            }

            LastUpdateLog = sortedNodes.ToList();
        }

        public void PerformUpdate(IReactiveNode initialNode)
        {
            try
            {
                OnUpdateStarted();

                var sortedNodes = Sort(Nodes, initialNode);
                UpdateNodes(sortedNodes);

                OnUpdateSuccessful();
            }
            catch (Exception)
            {
                OnUpdateFailed();
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

        #region Events

        public event EventHandler UpdateStarted;

        private void OnUpdateStarted()
        {
            UpdateStarted?.Invoke(this, null);
        }

        public event EventHandler UpdateSuccessful;

        private void OnUpdateSuccessful()
        {
            UpdateSuccessful?.Invoke(this, null);
        }

        public event EventHandler UpdateFailed;

        private void OnUpdateFailed()
        {
            UpdateFailed?.Invoke(this, null);
        }

        #endregion
    }
}
