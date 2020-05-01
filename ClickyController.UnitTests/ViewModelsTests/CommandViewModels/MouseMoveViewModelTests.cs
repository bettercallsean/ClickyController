using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class MouseMoveViewModelTests
    {
        [TestMethod]
        public void View_SetsOnInstantiation()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel();

            Assert.AreEqual(viewModel.View, "MouseMoveView");
        }

        [TestMethod]
        public void ViewModel_SetsCorrectly()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                ViewModel = "MouseMoveViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "MouseMoveViewModel");
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
        public void XCoordinate_ValidCoordinate_True()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = 500
            };

            Assert.IsTrue(viewModel.ValidXCoordinates);
        }

        [TestMethod]
        public void YCoordinate_ValidCoordinate_True()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                YCoordinates = 500
            };

            Assert.IsTrue(viewModel.ValidYCoordinates);
        }

        // I haven't figured out how to pass a string to the Parameters like what is done when the textbox 
        // passes through its value. I've tried (int) casting but that just throws an error.


        [TestMethod]
        public void XCoordinate_SetValue()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = 500
            };

            Assert.AreEqual(viewModel.XCoordinates, 500);
        }

        [TestMethod]
        public void YCoordinate_SetValue()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                YCoordinates = 500
            };

            Assert.AreEqual(viewModel.YCoordinates, 500);
        }

        [TestMethod]
        public void MoveRelative_IsTrue()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                MoveRelative = true
            };

            Assert.IsTrue(viewModel.MoveRelative);
        }

        [TestMethod]
        public void MoveRelative_IsFalse()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                MoveRelative = false
            };

            Assert.IsFalse(viewModel.MoveRelative);
        }

    }
}
