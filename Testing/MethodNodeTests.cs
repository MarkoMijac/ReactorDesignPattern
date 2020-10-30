using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactorDesignPattern;
using Testing.Fakes;

namespace Testing
{
    [TestClass]
    public class MethodNodeTests
    {
        [TestMethod]
        public void Update_GivenUpdateMethodExists_MethodIsInvoked()
        {
            //Arrange
            var obj = new AlfaMock();
            var node = new MethodNode(nameof(obj.Update_B), obj);

            //Act
            node.Update();

            //Assert
            Assert.IsTrue(obj.Update_B_Invoked == true);
        }
    }
}
