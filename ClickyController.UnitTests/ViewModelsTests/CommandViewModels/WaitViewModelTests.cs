using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class WaitViewModelTests
    {
        [TestMethod]
        public void WaitView_SetsOnInstantiation()
        {
            WaitViewModel viewModel = new WaitViewModel();

            Assert.AreEqual(viewModel.View, "WaitView");
        }

        [TestMethod]
        public void WaitViewModel_SetsCorrectly()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                ViewModel = "WaitViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "WaitViewModel");
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

        [TestMethod]
        public void Seconds_Valid_True()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                Seconds = 10
            };

            Assert.AreEqual(viewModel.Seconds, 10);
        }

        [TestMethod]
        public void Seconds_Valid_False()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                Seconds = -10
            };

            Assert.AreEqual(viewModel.Seconds, 0);
        }

        [TestMethod]
        public void ValidSeconds_True()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                Seconds = 10
            };

            Assert.IsTrue(viewModel.ValidSeconds);
        }

        [TestMethod]
        public void ValidSeconds_False()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                Seconds = -10
            };

            Assert.IsFalse(viewModel.ValidSeconds);
        }

        // I can't figure out how to pass a string to an int parameter to imitate textbox input
    }
}
