using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class CommandViewModelParameterTests
    {
        [TestMethod]
        public void MouseClickViewModelParameter()
        {
            MouseClickViewModel mouseClickViewModel = new MouseClickViewModel
            {
                Button = "Left",
                Action = "Down"
            };

            Assert.AreEqual(mouseClickViewModel.Parameters, "Left Down");
        }

        [TestMethod]
        public void MouseMoveViewModelParameter_Relative_False()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = 100,
                YCoordinates = 600,
                MoveRelative = false
            };

            Assert.AreEqual(viewModel.Parameters, "100, 600");
        }

        [TestMethod]
        public void MouseMoveViewModelParameter_Relative_True()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = 100,
                YCoordinates = 600,
                MoveRelative = true
            };

            Assert.AreEqual(viewModel.Parameters, "Relative: 100, 600");
        }

        [TestMethod]
        public void KeyboardCharacterInputViewModelParameter()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                ButtonAction = "Press",
                Character = "Ctrl"
            };

            Assert.AreEqual(viewModel.Parameters, "Ctrl - Key Press");
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
        public void WaitViewModelParameter()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                Seconds = 5
            };

            Assert.AreEqual(viewModel.Parameters, "5 seconds");
        }
    }
}
