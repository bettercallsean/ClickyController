using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyController.UnitTests.ViewModelsTests
{
    [TestClass]
    public class CommandViewModelTests
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
        public void MouseMoveViewModel_SetsCorrectly()
        {
            MouseMoveViewModel viewModel = new MouseMoveViewModel
            {
                ViewModel = "MouseMoveViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "MouseMoveViewModel");
        }

        [TestMethod]
        public void KeyboardCharacterInputViewModel_SetsCorrectly()
        {
            KeyboardCharacterInputViewModel viewModel = new KeyboardCharacterInputViewModel
            {
                ViewModel = "KeyboardCharacterInputViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "KeyboardCharacterInputViewModel");
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
        public void WaitViewModel_SetsCorrectly()
        {
            WaitViewModel viewModel = new WaitViewModel
            {
                ViewModel = "WaitViewModel"
            };

            Assert.AreEqual(viewModel.ViewModel, "WaitViewModel");
        }





    }
}