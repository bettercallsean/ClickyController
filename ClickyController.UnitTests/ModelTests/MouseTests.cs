using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyController;

namespace ClickyController.UnitTests.ModelTests
{
    [TestClass]
    public class MouseTests
    {
        [TestMethod]
        public void MoveMouse_NotRelative()
        {
            int x = 400;
            int y = 350;

            Mouse.MoveMouse(x, y);

            Assert.AreEqual(Mouse.XCoordinate, x);
            Assert.AreEqual(Mouse.YCoordinate, y);
        }

        [TestMethod]
        public void MoveMouse_Relative()
        {
            int x = 100;
            int y = 50;
            Mouse.MoveMouse(500, 500);

            Mouse.MoveMouse(x, y, true);

            Assert.AreEqual(Mouse.XCoordinate, 600);
            Assert.AreEqual(Mouse.YCoordinate, 550);
        }

        [TestMethod]
        public void MoveMouse_Relative_2()
        {
            int x = -150;
            int y = -400;
            Mouse.MoveMouse(500, 500);

            Mouse.MoveMouse(x, y, true);

            Assert.AreEqual(Mouse.XCoordinate, 350);
            Assert.AreEqual(Mouse.YCoordinate, 100);
        }

        [TestMethod]
        public void MoveMouse_OffScreen()
        {
            int x = -100;
            int y = -100;
            Mouse.MoveMouse(0, 0);

            Mouse.MoveMouse(x, y);

            Assert.AreEqual(Mouse.XCoordinate, 0);
            Assert.AreEqual(Mouse.YCoordinate, 0);
        }

    }
}
