using System;
using DemonstrationExamples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactorDesignPattern;
using Testing.Fakes;

namespace Testing
{
    [TestClass]
    public class PropertyNodeTests
    {
        [TestMethod]
        public void Update_GivenUpdateMethodExists_MethodIsInvoked()
        {
            //Arrange
            var obj = new AlfaMock();
            var node = new PropertyNode(nameof(obj.B), obj);

            //Act
            node.Update();

            //Assert
            Assert.IsTrue(obj.Update_B_Invoked == true);
        }
    }
}
