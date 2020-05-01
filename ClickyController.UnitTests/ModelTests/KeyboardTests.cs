using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClickyController.UnitTests.ModelTests
{
    [TestClass]
    public class KeyboardTests
    {
        [TestMethod]
        public void VirtualKeyExists_True()
        {
            string character = "]";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void VirtualKeyExists_True_2()
        {
            string character = "ctrl";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void VirtualKeyExists_True_3()
        {
            string character = "SHIFT";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void VirtualKeyExists_False()
        {
            string character = "ŵ";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void VirtualKeyExists_False_2()
        {
            string character = "Ś";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void VirtualKeyExists_False_3()
        {
            string character = "Word";

            bool result = Keyboard.VirtualCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void ScanCodeExists_True()
        {
            string character = "]";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ScanCodeExists_True_2()
        {
            string character = "ctrl";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ScanCodeExists_True_3()
        {
            string character = "SHIFT";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ScanCodeExists_False()
        {
            string character = "ŵ";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void ScanCodeExists_False_2()
        {
            string character = "Ś";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void ScanCodeExists_False_3()
        {
            string character = "Word";

            bool result = Keyboard.ScanCodeKeyExists(character);

            Assert.AreEqual(result, false);
        }

    }
}
