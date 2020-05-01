using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class MouseClickViewModelTests
    {
        [TestMethod]
        public void MouseClickViewModel_SetsCorrectly()
        {
            MouseClickViewModel viewModel = new MouseClickViewModel
            {
                ViewModel = "MouseClickViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "MouseClickViewModel");
        }

        [TestMethod]
        public void MouseClickView_SetsOnInstantiation()
        {
            MouseClickViewModel viewModel = new MouseClickViewModel();

            Assert.AreEqual(viewModel.View, "MouseClickView");
        }

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

    }
}
