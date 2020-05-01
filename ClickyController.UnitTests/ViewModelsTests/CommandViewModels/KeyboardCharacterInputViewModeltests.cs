using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class KeyboardCharacterInputViewModeltests
    {
        [TestMethod]
        public void View_SetsOnInstantiation()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel();

            Assert.AreEqual(viewModel.View, "KeyboardCharacterInputView");
        }
        [TestMethod]
        public void InputViewModel_SetsCorrectly()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                ViewModel = "KeyboardCharacterInputViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "KeyboardCharacterInputViewModel");
        }

        [TestMethod]
        public void ViewModelParameter()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                ButtonAction = "Press",
                Character = "Ctrl"
            };

            Assert.AreEqual(viewModel.Parameters, "Ctrl - Key Press");
        }

        [TestMethod]
        public void ValidCharacter_True()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                Character = "Ctrl"
            };

            Assert.IsTrue(viewModel.ValidCharacter);
        }

        [TestMethod]
        public void ValidCharacter_False()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                Character = "TESTKEY"
            };

            Assert.IsFalse(viewModel.ValidCharacter);
        }

    }
}
