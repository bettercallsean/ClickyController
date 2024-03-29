﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using ClickyController.Properties;
using static ClickyController.Controller;

namespace ClickyController;

/// <summary>
/// Contains the functionality that would be associated with using a keyboard
/// </summary>
public class Keyboard
{
	private const int VirtualCodeToScanCodeMode = 0;
	private const int ScanCodeToVirtualCodeMode = 1;
	// Maps the user specified key to its corresponding Windows Virtual Key
	private static readonly Dictionary<string, ushort> _keyToVirtualKeyDictionary = JsonSerializer.Deserialize<Dictionary<string, ushort>>(Resources.VirtualKeyCodes);
	// Contains characters that can only be accessed by holding the 'Shift' key e.g. ! @ £
	private static readonly Dictionary<string, string> _keyToVirtualKeyShiftDictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(Resources.VirtualKeyCodesShift);
	// Maps the user specified key to its corresponding Hardware Scan code
	private static readonly Dictionary<string, ushort> _keyToScanCodeDictionary = JsonSerializer.Deserialize<Dictionary<string, ushort>>(Resources.ScanCodes);

	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern uint MapVirtualKey(uint uCode, uint uMapType);

	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern int GetKeyNameText(uint lParam, [Out] StringBuilder lpString, int cchSize);

	// characters in this list produce rubbish output when passed into GetKeyNameText with extra shifted values
	private static readonly Dictionary<uint, string> _misbehavingKeys = new()
	{
		{ 19, "PAUSE"},
		{ 33, "PAGE UP"},
		{ 34, "PAGE DOWN"},
		{ 35, "END"},
		{ 36, "HOME"},
		{ 37, "LEFT"},
		{ 38, "UP"},
		{ 39, "RIGHT"},
		{ 40, "DOWN"},
		{ 45, "INS"},
		{ 46, "DEL"},
		{ 91, "WIN"},
		{ 93, "Apps" }
	};


	/// <summary>
	/// Performs a simple key press
	/// </summary>
	/// <param name="character">The name of the key or the letter the user wishes to press</param>
	public static void KeyPress(string character)
	{
		var holdShift = false;

		/* If the character entered requires the SHIFT key to be pressed, the keyToVirtualKeyShiftDictionary dictionary will contain the
         normal key that needs to be pressed 
         e.g. "!" is on the "1" key, "~" is on the "#" key 
         */

		// If the character isn't " ", then all the whitespace gets removed
		// e.g. "Caps Lock " will turn to "CapsLock"

		// This is only useful when someone calls this method directly from their program to press a certain key 
		// Otherwise the EnterText method passes each character through one by one anyway, meaning there won't be any whitespace to remove
		if (!string.IsNullOrWhiteSpace(character))
			character = character.Replace(" ", string.Empty);

		// The dictionary keys are all stored in lowercase, so any
		character = character.ToLower();

		if (!VirtualCodeKeyExists(character))
		{
			// If the character entered requires 'Shift' to be held, this tries to find it in a dictionary containing those values
			// e.g "!" or "@"
			try
			{
				character = _keyToVirtualKeyShiftDictionary[character];
				holdShift = true;
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine(e);
			}
		}

		var keyPress = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = _keyToVirtualKeyDictionary[character],
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = 0,
					keystrokeFlags = 0
				}
			}
		};


		var keyRelease = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = _keyToVirtualKeyDictionary[character],
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = 0,
					keystrokeFlags = 2
				}
			}
		};


		INPUT[] inputs = { keyPress, keyRelease };

		if (holdShift)
		{
			KeyDown("SHIFT");
			SendInput(2, inputs, INPUT.Size);
			KeyUp("SHIFT");
		}
		else
			SendInput(2, inputs, INPUT.Size);
	}

	/// <summary>
	/// Will 'type' the user specified string
	/// </summary>
	/// <param name="textEntry">The text that the user wishes to type out on the screen</param>
	public static void EnterText(string textEntry)
	{

		foreach (var letter in textEntry)
		{
			if (char.IsUpper(letter))
			{
				// Simulates holding 'SHIFT' in order to create a capitalised version of a character 
				KeyDown("SHIFT");
				KeyPress(letter.ToString());
				KeyUp("SHIFT");
			}
			else
				KeyPress(letter.ToString());
		}
	}

	/// <summary>
	/// Holds a key down using it's virtual key
	/// </summary>
	/// <param name="character">The key to hold down</param>
	public static void KeyDown(string character)
	{
		// Simulates holding a key down

		character = character.ToLower();

		var keyPress = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = _keyToVirtualKeyDictionary[character],
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = 0,
					keystrokeFlags = 0
				}
			}
		};


		INPUT[] inputs = { keyPress };

		SendInput(1, inputs, INPUT.Size);
	}

	/// <summary>
	/// Releases a key
	/// </summary>
	/// <param name="character">The key to release</param>
	public static void KeyUp(string character)
	{
		// Simulates releasing a key
		character = character.ToLower();

		var keyPress = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = _keyToVirtualKeyDictionary[character],
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = 0,
					keystrokeFlags = 2
				}
			}
		};


		INPUT[] inputs = { keyPress };

		SendInput(1, inputs, INPUT.Size);
	}

	/// <summary>
	/// Performs a keyboard shortcut by pressing all of the keys at the same time
	/// </summary>
	/// <param name="character1">Name of the first key to press down</param>
	/// <param name="character2">Name of the second key to press down</param>
	/// <param name="character3">Optional: Name of the third key to press down</param>
	public static void KeyboardShortcut(string character1, string character2, string character3 = "")
	{
		KeyDown(character1);
		KeyDown(character2);

		if (!string.IsNullOrWhiteSpace(character3))
			KeyPress(character3);


		KeyUp(character1);
		KeyUp(character2);
	}

	/// <summary>
	/// Performs a keyboard shortcut by pressing all of the keys at the same time, using the key's scan codes
	/// </summary>
	/// <param name="character1">Name of the first key to press down</param>
	/// <param name="character2">Name of the second key to press down</param>
	/// <param name="character3">Optional: Name of the third key to press down</param>
	public static void KeyboardShortcutScanCode(string character1, string character2, string character3 = "")
	{
		KeyDownScanCode(character1);
		KeyDownScanCode(character2);

		if (!string.IsNullOrWhiteSpace(character3))
			KeyPressScanCode(character3);

		KeyUpScanCode(character1);
		KeyUpScanCode(character2);
	}

	/// <summary>
	/// Performs a key press using the key's scan code.
	/// ScanCodes are the codes sent directly by your keyboard hardware and can be useful in apps/games that take their input 
	/// directly from the keyboard directly.
	/// </summary>
	/// <param name="character">Name of the key or character the user wishes to press</param>
	public static void KeyPressScanCode(string character)
	{
		character = character.ToLower();

		var scanCode = _keyToScanCodeDictionary[character];

		var keyDown = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = 0,
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = scanCode,
					keystrokeFlags = 0x0008
				}
			}
		};


		var keyRelease = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = 0,
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = scanCode,
					keystrokeFlags = 0x0008 | 0x0002
				}
			}
		};


		INPUT[] inputs = { keyDown, keyRelease };

		SendInput(2, inputs, INPUT.Size);

	}

	/// <summary>
	/// Holds a key down using its scan code
	/// </summary>
	/// <param name="character">Name of the key or character the user wishes to hold down</param>
	public static void KeyDownScanCode(string character)
	{
		character = character.ToLower();

		var keyPress = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = 0,
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = _keyToScanCodeDictionary[character],
					keystrokeFlags = 0x0008
				}
			}
		};


		INPUT[] inputs = { keyPress };

		SendInput(1, inputs, INPUT.Size);
	}

	/// <summary>
	/// Releases a key using its scan code
	/// </summary>
	/// <param name="character">Name of the key or character the user wishes to release</param>
	public static void KeyUpScanCode(string character)
	{
		character = character.ToLower();

		var keyPress = new INPUT
		{
			type = 1,
			union =
			{
				keyboardInput = new KEYBDINPUT
				{
					virtualKeyCode = 0,
					time = 0,
					extraInfo = GetMessageExtraInfo(),
					hardwareScanCode = _keyToScanCodeDictionary[character],
					keystrokeFlags = 0x0008 | 0x0002
				}
			}
		};


		INPUT[] inputs = { keyPress };

		SendInput(1, inputs, INPUT.Size);
	}

	/// <summary>
	/// Checks whether a key or character has a Virtual Code stored in the Virtual Key dictionary
	/// </summary>
	/// <param name="character">Name of the key or character the user wishes to check</param>
	/// <returns></returns>
	public static bool VirtualCodeKeyExists(string character)
	{
		// Since the keys are stored in lowercase, we need to convert the input first
		character = character.ToLower();
		return _keyToVirtualKeyDictionary.ContainsKey(character);
	}

	/// <summary>
	/// Checks whether a key or character has a Scan Code stored in the Scan Key dictionary
	/// </summary>
	/// <param name="character">Name of the key or character the user wishes to check</param>
	/// <returns></returns>
	public static bool ScanCodeKeyExists(string character)
	{
		// Since the keys are stored in lowercase, we need to convert the input first
		character = character.ToLower();
		return _keyToScanCodeDictionary.ContainsKey(character);
	}

	/// <summary>
	/// Returns the name of the pressed key based on the virtual code that is passed in
	/// </summary>
	/// <param name="virtualCode">Virtual code of the keyboard key</param>
	/// <returns></returns>
	public static string GetKeyNameFromVirtualCode(uint virtualCode)
	{
		if (_misbehavingKeys.ContainsKey(virtualCode))
			return _misbehavingKeys[virtualCode];

		var scanCode = MapVirtualKey(virtualCode & 0xffff, VirtualCodeToScanCodeMode);

		var output = new StringBuilder(512);
		scanCode <<= 0x10;

		GetKeyNameText(scanCode, output, output.Capacity);

		return output.ToString();
	}
}
