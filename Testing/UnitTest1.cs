using System;
using Examples;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactorDesignPattern;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
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

        [TestMethod]
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
    }
}
