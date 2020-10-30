using System;
using DemonstrationExamples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactorDesignPattern;

namespace Testing
{
    [TestClass]
    public class ReactiveNodeTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_GivenEmptyMember_ThrowsException()
        {
            var node = new PropertyNode("", new object());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_GivenNullOwner_ThrowsException()
        {
            var node = new PropertyNode("A", null);
        }

        [TestMethod]
        public void Constructor_GivenValidArguments_CreatesReactiveNode()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node = new PropertyNode(nameof(obj.A), obj);

            //Assert
            Assert.IsTrue(node != null 
                && node.Member == nameof(obj.A) 
                && node.Owner == obj);
        }

        [TestMethod]
        public void Constructor_GivenValidArguments_IdentifierIsGenerated()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node = new PropertyNode(nameof(obj.A), obj);

            //Assert
            Assert.IsTrue(node.Identifier != 0);
        }

        [TestMethod]
        public void Constructor_GivenSameObjectMemberPair_NodesAreEqual()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node1 = new PropertyNode(nameof(obj.A), obj);
            var node2 = new PropertyNode(nameof(obj.A), obj);

            //Assert
            Assert.AreEqual(node1, node2);
        }

        [TestMethod]
        public void Constructor_GivenSameObjectMemberPair_NodeHaveSameIdentifier()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node1 = new PropertyNode(nameof(obj.A), obj);
            var node2 = new PropertyNode(nameof(obj.A), obj);

            //Assert
            Assert.IsTrue(node1.Identifier == node2.Identifier);
        }

        [TestMethod]
        public void Constructor_GivenDifferentObjectMemberPair_NodesAreNotEqual()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node1 = new PropertyNode(nameof(obj.A), obj);
            var node2 = new PropertyNode(nameof(obj.B), obj);

            //Assert
            Assert.AreNotEqual(node1, node2);
        }

        [TestMethod]
        public void Constructor_GivenDifferentObjectMemberPair_NodeHaveSameIdentifier()
        {
            //Arrange
            var obj = new Alfa();

            //Act
            var node1 = new PropertyNode(nameof(obj.A), obj);
            var node2 = new PropertyNode(nameof(obj.B), obj);

            //Assert
            Assert.IsFalse(node1.Identifier == node2.Identifier);
        }

        #endregion

        #region AddSuccessor

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddSuccessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);

            //Act
            nodeA.AddSuccessor(null);
        }

        [TestMethod]
        public void AddSuccessor_GivenValidNode_SuccessorIsAdded()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);

            //Act
            nodeA.AddSuccessor(nodeB);

            //Assert
            Assert.IsTrue(nodeA.Successors.Contains(nodeB));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddSuccessor_AddingItself_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);

            //Act
            nodeA.AddSuccessor(nodeA);
        }

        [TestMethod]
        public void AddSuccessor_GivenAlreadyExistingSuccessor_SuccessorIsNotAdded()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            nodeA.AddSuccessor(nodeB);

            //Act
            nodeA.AddSuccessor(nodeB);

            //Assert
            Assert.IsTrue(nodeA.Successors.Count == 1);
        }

        #endregion

        #region AddPredecessors

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddPredecessor_GivenNullNode_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);

            //Act
            nodeA.AddPredecessor(null);
        }

        [TestMethod]
        public void AddPredecessor_GivenValidNode_PredecessorIsAdded()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);

            //Act
            nodeB.AddPredecessor(nodeA);

            //Assert
            Assert.IsTrue(nodeB.Predecessors.Contains(nodeA));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddPredecessor_AddingItself_ThrowsException()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);

            //Act
            nodeA.AddPredecessor(nodeA);
        }

        [TestMethod]
        public void AddPredecessor_GivenAlreadyExistingPredecessor_PredecessorIsNotAdded()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            nodeA.AddPredecessor(nodeB);

            //Act
            nodeA.AddPredecessor(nodeB);

            //Assert
            Assert.IsTrue(nodeA.Predecessors.Count == 1);
        }

        #endregion

        [TestMethod]
        public void RemoveSuccessor_GivenExistingSuccessor_SuccessorIsRemoved()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            nodeA.AddSuccessor(nodeB);

            //Act
            nodeA.RemoveSuccessor(nodeB);

            //Assert
            Assert.IsFalse(nodeA.Successors.Contains(nodeB));
        }

        [TestMethod]
        public void RemovePredecessor_GivenExistingPredecessor_PredecessorIsRemoved()
        {
            //Arrange
            var obj = new Alfa();
            var nodeA = new PropertyNode(nameof(obj.A), obj);
            var nodeB = new PropertyNode(nameof(obj.B), obj);
            nodeB.AddPredecessor(nodeA);

            //Act
            nodeB.RemovePredecessor(nodeA);

            //Assert
            Assert.IsFalse(nodeB.Predecessors.Contains(nodeA));
        }
    }
}
