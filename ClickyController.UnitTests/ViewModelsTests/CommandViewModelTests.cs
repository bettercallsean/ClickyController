using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClickyControllerGUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickyControllerGUI.ViewModels.Tests
{
    [TestClass]
    public class CommandViewModelTests
    {
        [TestMethod]
        public void ViewModel_SetsCorrectly()
        {
            string viewmodel = "MouseClickViewModel";
            MouseClickViewModel mouseClickViewModel = new MouseClickViewModel();

            mouseClickViewModel.ViewModel = viewmodel;

            Assert.AreEqual(mouseClickViewModel.ViewModel, viewmodel);
        }

        [TestMethod]
        public void View_IsSetOnViewModelInstantiation()
        {
            string view = "MouseClickView";
            MouseClickViewModel mouseClickViewModel = new MouseClickViewModel();

            Assert.AreEqual(mouseClickViewModel.View, view);
        }

        [TestMethod]
        public void MouseClickViewModelParameter_Test()
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
    }
}