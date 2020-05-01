using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class CommandViewModelViewTests
    {
        [TestMethod]
        public void MouseClickView_SetsOnInstantiation()
        {
            MouseClickViewModel viewModel = new MouseClickViewModel();

            Assert.AreEqual(viewModel.View, "MouseClickView");
        }

        [TestMethod]
        public void MouseMoveView_SetsOnInstantiation()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel();

            Assert.AreEqual(viewModel.View, "MouseMoveView");
        }

        [TestMethod]
        public void KeyboardCharacterInputView_SetsOnInstantiation()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel();

            Assert.AreEqual(viewModel.View, "KeyboardCharacterInputView");
        }

        [TestMethod]
        public void KeyboardTextInputView_SetsOnInstantiation()
        {
            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel();

            Assert.AreEqual(viewModel.View, "KeyboardTextInputView");
        }

        [TestMethod]
        public void WaitView_SetsOnInstantiation()
        {
            WaitViewModel viewModel = new WaitViewModel();

            Assert.AreEqual(viewModel.View, "WaitView");
        }
    }
}
