using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;
namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class KeyboardTextInputViewModelTests
    {
        [TestMethod]
        public void KeyboardTextInputView_SetsOnInstantiation()
        {
            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel();

            Assert.AreEqual(viewModel.View, "KeyboardTextInputView");
        }

        [TestMethod]
        public void KeyboardTextInputViewModel_SetsCorrectly()
        {
            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel
            {
                ViewModel = "KeyboardTextInputViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "KeyboardTextInputViewModel");
        }

        [TestMethod]
        public void KeyboardTextInputViewModelParameter()
        {
            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel
            {
                Text = "Hey now, brown cow"
            };

            Assert.AreEqual(viewModel.Parameters, "Hey now, brown cow");
        }

        [TestMethod]
        public void SetText()
        {
            string value = "This is a public service announcement, this is only a test";

            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel
            {
                Text = value
            };

            Assert.AreEqual(viewModel.Text, value);
        }

        [TestMethod]
        public void SetText_EmptyString()
        {
            string value = "";

            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel
            {
                Text = value
            };

            Assert.AreEqual(viewModel.Text, value);
        }

        [TestMethod]
        public void SetText_Whitespace()
        {
            string value = "               ";

            KeyboardTextInputViewModel viewModel = new KeyboardTextInputViewModel
            {
                Text = value
            };

            Assert.AreEqual(viewModel.Text, value);
        }
    }
}
