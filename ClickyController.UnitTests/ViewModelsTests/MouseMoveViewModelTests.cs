using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class MouseMoveViewModelTests
    {
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

        [TestMethod]
        public void XCoordinate_ValidCoordinate_True_TryParse()
        {
            object value = "200";
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = (int)value
            };

            Assert.IsTrue(viewModel.ValidXCoordinates);
        }

        [TestMethod]
        public void YCoordinate_ValidCoordinate_True_TryParse()
        {
            object value = "200";
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                YCoordinates = (int)value
            };

            Assert.IsTrue(viewModel.ValidYCoordinates);
        }

        [TestMethod]
        public void XCoordinate_ValidCoordinate_False()
        {
            object value = "Hello";
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                XCoordinates = (int)value
            };

            Assert.IsFalse(viewModel.ValidXCoordinates);
        }

        [TestMethod]
        public void YCoordinate_ValidCoordinate_False()
        {
            object value = "Hello";
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                YCoordinates = (int)value
            };

            Assert.IsFalse(viewModel.ValidYCoordinates);
        }
    }
}
