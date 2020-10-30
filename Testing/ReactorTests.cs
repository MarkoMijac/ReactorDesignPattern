using System;
using DemonstrationExamples;
using Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactorDesignPattern;

namespace Testing
{
    [TestClass]
    public class ReactorTests
    {
        //[TestMethod]
        public void TestMethod1()
        {
            IReactor r = ReactorManager.GetDefaultReactor();

            ConstructionPart part = new ConstructionPart("CP1", 5, 2.5);
            IReactiveNode widthNode = new PropertyNode(nameof(part.Width), part);
            IReactiveNode heightNode = new PropertyNode(nameof(part.Height), part);
            IReactiveNode surfaceAreaNode = new PropertyNode(nameof(part.SurfaceArea), part);

            r.CreateDependency(widthNode, surfaceAreaNode);
            r.CreateDependency(heightNode, surfaceAreaNode);

            r.PerformUpdate();

            Assert.AreEqual(12.5, part.SurfaceArea);
        }

        //[TestMethod]
        public void TestMethod2()
        {
            IReactor r = ReactorManager.GetDefaultReactor();

            ConstructionPart part = new ConstructionPart("CP1", 5, 2.5);
            IReactiveNode widthNode = new PropertyNode(nameof(part.Width), part);
            IReactiveNode heightNode = new PropertyNode(nameof(part.Height), part);
            IReactiveNode surfaceAreaNode = new PropertyNode(nameof(part.SurfaceArea), part);

            r.CreateDependency(widthNode, surfaceAreaNode);
            r.CreateDependency(heightNode, surfaceAreaNode);

            r.PerformUpdate();

            Assert.AreEqual(12.5, part.SurfaceArea);

            part.Height = 3;

            Assert.AreEqual(15, part.SurfaceArea);

            part.Width = 6;

            Assert.AreEqual(18, part.SurfaceArea);
        }

        #region GetNode

        [TestMethod]
        public void GetNode_GivenNoMatchingNode1_ReturnsNull()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            var fetchedNode = reactor.GetNode(node.Member, node.Owner);

            //Assert
            Assert.IsNull(fetchedNode);
        }

        [TestMethod]
        public void GetNode_GivenNoMatchingNode2_ReturnsNull()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            var fetchedNode = reactor.GetNode(node.Identifier);

            //Assert
            Assert.IsNull(fetchedNode);
        }

        [TestMethod]
        public void GetNode_GivenExistingNode1_ReturnsMatchingNode()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");
            reactor.AddNode(node);

            //Act
            var fetchedNode = reactor.GetNode(node.Member, node.Owner);

            //Assert
            Assert.IsNotNull(fetchedNode);
        }

        [TestMethod]
        public void GetNode_GivenExistingNode2_ReturnsMatchingNode()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");
            reactor.AddNode(node);

            //Act
            var fetchedNode = reactor.GetNode(node.Identifier);

            //Assert
            Assert.IsNotNull(fetchedNode);
        }

        #endregion

        #region AddNode

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNode_GivenNodeIsNull_ThrowsException()
        {
            //Arrange
            var reactor = new Reactor("R1");

            //Act
            reactor.AddNode(null);
        }

        [TestMethod]
        public void AddNode_GivenValidNode_NodeIsAdded()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.AddNode(node);

            //Assert
            Assert.IsTrue(reactor.GetNode(node.Identifier) != null);
        }

        [TestMethod]
        public void AddNode_GivenExistingNode_NodeIsNotAdded()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");
            reactor.AddNode(node);

            //Act
            reactor.AddNode(node);

            //Assert
            Assert.IsTrue(reactor.Nodes.Count == 1);
        }

        #endregion

        #region RemoveNode

        [TestMethod]
        public void RemoveNode_GivenExistingNode_NodeIsRemoved()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");
            reactor.AddNode(node);

            //Act
            reactor.RemoveNode(node);

            //Assert
            Assert.IsTrue(reactor.GetNode(node.Identifier) == null);
        }

        #endregion

        #region CreateDependency

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDependency_GivenPredecessorIsNull_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.CreateDependency(null, node);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDependency_GivenSuccessorIsNull_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.CreateDependency(node, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDependency_GivenPredecessorAndSuccessorAreSame_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var node = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.CreateDependency(node, node);
        }

        [TestMethod]
        public void CreateDependency_GivenValidPredecessorAndSuccessor_DependencyCreated()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.CreateDependency(nodeA, nodeB);

            //Assert
            var predecessor = reactor.GetNode(nodeA.Identifier);
            var successor = reactor.GetNode(nodeB.Identifier);

            Assert.IsTrue(predecessor != null
                && successor != null
                && predecessor.Successors.Contains(successor)
                && successor.Predecessors.Contains(predecessor));
        }

        #endregion

        #region RemoveDependency

        [TestMethod]
        public void RemoveDependency_GivenValidDependency_DependencyRemoved()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            var reactor = new Reactor("R1");
            reactor.CreateDependency(nodeA, nodeB);

            //Act
            reactor.RemoveDependency(nodeA, nodeB);

            //Assert
            var predecessor = reactor.GetNode(nodeA.Identifier);
            var successor = reactor.GetNode(nodeB.Identifier);

            Assert.IsTrue(predecessor.Successors.Contains(successor) == false
                && successor.Predecessors.Contains(predecessor) == false);
        }

        [TestMethod]
        public void RemoveDependency_GivenPredecessorAndSuccessorAreNull_IgnoresRemoval()
        {
            //Arrange
            var reactor = new Reactor("R1");

            //Act
            reactor.RemoveDependency(null, null);

            //Assert
        }

        #endregion

        #region PerformUpdate

        [TestMethod]
        public void PerformUpdate_GivenEmptyGraph_ReturnsEmptyUpdateLog()
        {
            //Arrange
            var reactor = new Reactor("R1");

            //Act
            reactor.PerformUpdate();

            //Assert
            Assert.IsTrue(reactor.LastUpdateLog.Count == 0);
        }

        [TestMethod]
        public void PerformUpdate_GivenEmptyGraph1_ReturnsEmptyUpdateLog()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var reactor = new Reactor("R1");

            //Act
            reactor.PerformUpdate(nodeA);

            //Assert
            Assert.IsTrue(reactor.LastUpdateLog.Count == 0);
        }

        [TestMethod]
        public void PerformUpdate_GivenNonEmptyGraph_ReturnsCorrectUpdateLog()
        {
            //Arrange
            var alfa = new Alfa();
            var beta = new Beta(alfa);
            var gama = new Gama(alfa, beta);

            var nodeA = new PropertyNode(nameof(alfa.A), alfa);
            var nodeB = new PropertyNode(nameof(alfa.B), alfa);
            var nodeC = new PropertyNode(nameof(beta.C), beta);
            var nodeD = new PropertyNode(nameof(beta.D), beta);
            var nodeE = new PropertyNode(nameof(gama.E), gama);
            var nodeF = new PropertyNode(nameof(gama.F), gama);

            var reactor = new Reactor("R1");
            reactor.CreateDependency(nodeA, nodeE);
            reactor.CreateDependency(nodeA, nodeB);
            reactor.CreateDependency(nodeA, nodeD);
            reactor.CreateDependency(nodeC, nodeD);
            reactor.CreateDependency(nodeB, nodeE);
            reactor.CreateDependency(nodeB, nodeF);
            reactor.CreateDependency(nodeD, nodeF);
            reactor.CreateDependency(nodeE, nodeF);

            //Act
            reactor.PerformUpdate();

            //Assert
            Assert.IsTrue(reactor.LastUpdateLog.Count > 0);
        }

        private IReactor CreateFilledReactor()
        {
            var alfa = new Alfa();
            var beta = new Beta(alfa);
            var gama = new Gama(alfa, beta);

            var nodeA = new PropertyNode(nameof(alfa.A), alfa);
            var nodeB = new PropertyNode(nameof(alfa.B), alfa);
            var nodeC = new PropertyNode(nameof(beta.C), beta);
            var nodeD = new PropertyNode(nameof(beta.D), beta);
            var nodeE = new PropertyNode(nameof(gama.E), gama);
            var nodeF = new PropertyNode(nameof(gama.F), gama);

            var reactor = new Reactor("R1");
            reactor.CreateDependency(nodeA, nodeE);
            reactor.CreateDependency(nodeA, nodeB);
            reactor.CreateDependency(nodeA, nodeD);
            reactor.CreateDependency(nodeC, nodeD);
            reactor.CreateDependency(nodeB, nodeE);
            reactor.CreateDependency(nodeB, nodeF);
            reactor.CreateDependency(nodeD, nodeF);
            reactor.CreateDependency(nodeE, nodeF);

            return reactor;
        }

        #endregion
    }
}
