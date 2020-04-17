﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ClickyController;
using ClickyControllerGUI.Models;
using ClickyControllerGUI.Views.CommandViews;
using ClickyControllerGUI.Utilities;

namespace ClickyControllerGUI.ViewModels
{
    public abstract class CommandViewModel : BaseViewModel
    {
        // Stores thee ViewModel name, which is used when adding new commands to the CommandList
        // and is also used when deserializing a script as the deserializer doesn't work with 
        // abstract classes
        private string _viewModel;
        public string ViewModel 
        { 
            get => _viewModel;
            set { _viewModel = value; OnPropertyChanged(); }
        }

        // I know this breaks MVVM compliance, but my head is aching and 
        // I just want something that works :(
        
        // Stores the corresponding View for each command, which is then opened
        // when the user edits the command
        private string _view;
        public string View
        {
            get => _view;
            set { _view = value; OnPropertyChanged(); }
        }

        // Acts as a string representation of the saved parameters. This is what is displayed to the user on the MainWindow
        public abstract string Parameters
        {
            get;
        }


        // Executes the ViewModels command
        public abstract void Execute();

    }

    public class MouseClickViewModel : CommandViewModel
    {
        readonly MouseClick _mouseClick = new MouseClick();

        public MouseClickViewModel()
        {
            View = "MouseClickView";
            ButtonSelection = new List<string>()
            {
                "Left",
                "Middle",
                "Right"
            };
        }



        public override string Parameters => string.Format("{0} Click", Button);

        public string Button
        {
            get => _mouseClick.Button;
            set 
            { 
                _mouseClick.Button = value; 
                OnPropertyChanged();
                OnPropertyChanged("Parameters");
            }
        }

        private List<string> _buttonSelection;
        public List<string> ButtonSelection
        {
            get => _buttonSelection;
            set { _buttonSelection = value; OnPropertyChanged(); }
        }

        public override void Execute()
        {
            _mouseClick.Execute();
        }
    }

    public class MouseMoveViewModel : CommandViewModel
    {
        MouseMove _mouseMove = new MouseMove();

        public MouseMoveViewModel()
        {
            View = "MouseMoveView";
        }

        public override string Parameters => string.Format("{0}, {1}", XCoordinates, YCoordinates);

        public int XCoordinates
        {
            get => _mouseMove.XCoords;
            set
            {
                if (int.TryParse(value.ToString(), out int xCoord))
                {
                    _mouseMove.XCoords = xCoord;
                    ValidXCoordinates = true;
                    
                }
                else
                    ValidXCoordinates = false;

                OnPropertyChanged();
                OnPropertyChanged("Parameters");
            }
        }

        public int YCoordinates
        {
            get => _mouseMove.YCoords;
            set
            {
                if (int.TryParse(value.ToString(), out int yCoord))
                {
                    _mouseMove.YCoords = yCoord;
                    ValidYCoordinates = true;
                }
                else
                    ValidYCoordinates = false;

                OnPropertyChanged();
                OnPropertyChanged("Parameters");
            }
        }

        private bool _validXCoordinates;
        public bool ValidXCoordinates
        {
            get => _validXCoordinates;
            set { _validXCoordinates = value; OnPropertyChanged(); }
        }

        private bool _validYCoordinates;
        public bool ValidYCoordinates
        {
            get => _validYCoordinates;
            set { _validYCoordinates = value; OnPropertyChanged(); }
        }

        private bool _moveRelative;
        public bool MoveRelative
        {
            get => _moveRelative;
            set { _moveRelative = value; OnPropertyChanged(); }
        }

        public override void Execute()
        {
            _mouseMove.Execute();
        }

    }

    public class KeyboardCharacterInputViewModel : CommandViewModel
    {
        readonly KeyboardCharacterInput _keyboardCharacterInput = new KeyboardCharacterInput();
        public KeyboardCharacterInputViewModel()
        {
            View = "KeyboardCharacterInputView";
            ButtonActionList = new List<string>()
            {
                "Press",
                "Down",
                "Up"
            };
        }

        public override string Parameters => string.Format("Key {0} - {1}", ButtonAction, Character);

        private List<string> _buttonActionList;
        public List<string> ButtonActionList
        {
            get => _buttonActionList;
            set { _buttonActionList = value; OnPropertyChanged(); }
        }


        public string ButtonAction 
        { 
            get => _keyboardCharacterInput.ButtonAction; 
            set 
            { 
                _keyboardCharacterInput.ButtonAction = value; 
                OnPropertyChanged();
                OnPropertyChanged("Parameters");
            } 
        }

        public string Character 
        { 
            get => _keyboardCharacterInput.Character; 
            set 
            {
                if(ClickyController.Keyboard.VirtualCodeKeyExists(value.ToString()))
                {
                    _keyboardCharacterInput.Character = value;
                    ValidCharacter = true;
                }
                else
                    ValidCharacter = false;

                OnPropertyChanged();
                OnPropertyChanged("Parameters");
                
            } 
        }

        private bool _validCharacter;
        public bool ValidCharacter
        {
            get => _validCharacter;
            set { _validCharacter = value; OnPropertyChanged(); }
        }

        public override void Execute()
        {
            _keyboardCharacterInput.Execute();
        }
    }

    public class KeyboardTextInputViewModel : CommandViewModel
    {
        readonly KeyboardTextInput _keyboardTextInput = new KeyboardTextInput();
        public KeyboardTextInputViewModel()
        {
            View = "KeyboardTextInputView";
        }

        public override string Parameters => Text;

        public string Text 
        { 
            get => _keyboardTextInput.Text;
            set 
            { 
                _keyboardTextInput.Text = value; 
                OnPropertyChanged();
                OnPropertyChanged("Parameters");
            } 
        }


        public override void Execute()
        {
            _keyboardTextInput.Execute();
        }
    }

    public class WaitViewModel : CommandViewModel
    {
        readonly Wait _wait = new Wait();
        public WaitViewModel()
        {
            View = "WaitView";
        }

        public override string Parameters => string.Format("{0} seconds", Seconds);

        public int Seconds 
        { 
            get => _wait.Seconds; 
            set 
            {
                if (int.TryParse(value.ToString(), out int seconds))
                {
                    _wait.Seconds = seconds;
                    ValidSeconds = true;
                    OnPropertyChanged("Parameters");
                }
                else
                    ValidSeconds = false;

                OnPropertyChanged();
                

            } 
        }

        private bool _validSeconds;
        public bool ValidSeconds
        {
            get => _validSeconds;
            set { _validSeconds = value; OnPropertyChanged(); }
        }

        public override void Execute()
        {
            _wait.Execute();
        }

    }
}
