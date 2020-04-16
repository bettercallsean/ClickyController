﻿using System;
using System.Collections.Generic;
using System.Text;
using ClickyController;
using ClickyControllerGUI.Models;

namespace ClickyControllerGUI.ViewModels
{
    public abstract class CommandViewModel : BaseViewModel
    {
        private string _type;
        public string Type 
        { 
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public abstract void Execute();
    }

    public class MouseClickViewModel : CommandViewModel
    {
        MouseClick _mouseClick = new MouseClick();


        public MouseClickViewModel()
        {
            ButtonSelection = new Dictionary<string, char>()
            {
                { "Left Click", 'L' },
                { "Middle Click", 'M'},
                { "Right Click", 'R'}
            };
        }

        public char Button
        {
            get => _mouseClick.Button;
            set { _mouseClick.Button = value; OnPropertyChanged(); }
        }

        private Dictionary<string, char> _buttonSelection;
        public Dictionary<string, char> ButtonSelection
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

        }

        public int XCoordinates
        {
            get => _mouseMove.XCoords;
            set
            {
                if (int.TryParse(value.ToString(), out int xCoord))
                {
                    _mouseMove.XCoords = xCoord;
                    OnPropertyChanged();
                }
                else
                    ValidXCoordinates = false;
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

        public override void Execute()
        {
            _mouseMove.Execute();
        }

    }

    public class KeyboardCharacterInputViewModel : CommandViewModel
    {
        KeyboardCharacterInput _keyboardCharacterInput = new KeyboardCharacterInput();
        public KeyboardCharacterInputViewModel()
        {
            ButtonActionDictionary = new Dictionary<string, char>()
            {
                { "Key Press", 'P' },
                { "Key Down", 'D' },
                { "Key Up", 'U' }
            };
        }


        private Dictionary<string, char> _buttonActionDictionary;
        public Dictionary<string, char> ButtonActionDictionary
        {
            get => _buttonActionDictionary;
            set { _buttonActionDictionary = value; OnPropertyChanged(); }
        }


        public char ButtonAction 
        { 
            get => _keyboardCharacterInput.ButtonAction; 
            set { _keyboardCharacterInput.ButtonAction = value; OnPropertyChanged(); } 
        }

        public string Character 
        { 
            get => _keyboardCharacterInput.Character; 
            set 
            {
                if(Keyboard.VirtualCodeKeyExists(value.ToString()))
                {
                    _keyboardCharacterInput.Character = value;
                    ValidCharacter = true;
                }
                else
                    ValidCharacter = false;

                OnPropertyChanged();
                
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

        KeyboardTextInput _keyboardTextInput = new KeyboardTextInput();
        public KeyboardTextInputViewModel()
        {

        }

        public string Text 
        { 
            get => _keyboardTextInput.Text;
            set { _keyboardTextInput.Text = value; OnPropertyChanged(); } 
        }

        public override void Execute()
        {
            _keyboardTextInput.Execute();
        }
    }

    public class WaitViewModel : CommandViewModel
    {

        Wait _wait = new Wait();
        public WaitViewModel()
        {

        }

        public int Seconds 
        { 
            get => _wait.Seconds; 
            set 
            {
                if (int.TryParse(value.ToString(), out int seconds))
                {
                    _wait.Seconds = seconds;
                    ValidSeconds = true;
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
