using System.Runtime.InteropServices;
using static ClickyController.Controller;

namespace ClickyController;

/// <summary>
/// Contains the functionality that would typically be associated with the mouse 
/// e.g mouse clicks, scrolling and moving the mouse
/// </summary>
public class Mouse
{
	/// <summary>
	/// Windows API that returns the position of the cursor with its X and Y coordinates
	/// </summary>
	/// <param name="mousePosition">POINT struct containing the X and Y coordinates of the mouse</param>
	/// <returns></returns>
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern bool GetCursorPos(out POINT mousePosition);

	/// <summary>
	/// Windows API that moves the mouse to the given X/Y coordinates
	/// </summary>
	/// <param name="x">The X coordinate to move the mouse to</param>
	/// <param name="y">The Y coordinate to move the mouse to</param>
	/// <returns></returns>
	[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	private static extern bool SetCursorPos(int x, int y);

	/// <summary>
	/// Moves mouse to a certain position on the screen
	/// </summary>
	/// <param name="xPosition">The X coordinate to move the mouse to.</param>
	/// <param name="yPosition">The Y coordinate to move the mouse to.</param>
	/// <param name="relativeToMousePosition">Optional parameter: If enabled, will move the mouse relative to its current position on the screen</param>
	public static void MoveMouse(int xPosition, int yPosition, bool relativeToMousePosition = false)
	{
		// If relativeToMousePosition is true, the mouse is moved a given number of pixels along the X/Y axis
		// relative to where the mouse cursor is currently located on screen.
		if (relativeToMousePosition)
		{
			xPosition = XCoordinate + xPosition;
			yPosition = YCoordinate + yPosition;
		}

		SetCursorPos(xPosition, yPosition);
	}

	/// <summary>
	/// Performs a left click
	/// </summary>
	public static void LeftClick()
	{
		MouseAction(0x0002);
		MouseAction(0x0004);
	}

	/// <summary>
	/// Holds to the left button down
	/// </summary>
	public static void LeftDown()
	{
		MouseAction(0x0002);
	}

	/// <summary>
	///  Lifts the left button up
	/// </summary>
	public static void LeftUp()
	{
		MouseAction(0x0004);
	}

	/// <summary>
	/// Performs a right click
	/// </summary>
	public static void RightClick()
	{
		MouseAction(0x0008);
		MouseAction(0x0010);
	}

	/// <summary>
	/// Holds the right button down
	/// </summary>
	public static void RightDown()
	{
		MouseAction(0x0008);
	}

	/// <summary>
	/// Lifts the right button up
	/// </summary>
	public static void RightUp()
	{
		MouseAction(0x0010);
	}

	/// <summary>
	/// Performs a click with the middle button (the scroll wheel)
	/// </summary>
	public static void MiddleClick()
	{
		MouseAction(0x0020);
		MouseAction(0x0040);
	}

	/// <summary>
	/// Holds the middle mouse button down
	/// </summary>
	public static void MiddleDown()
	{
		MouseAction(0x0020);
	}

	/// <summary>
	/// Lifts the middle mouse button up
	/// </summary>
	public static void MiddleUp()
	{
		MouseAction(0x0040);
	}

	/// <summary>
	/// Scrolls the mouse wheel down
	/// </summary>
	/// <param name="clicks">The number of mouse wheel 'clicks' to scroll down by</param>
	public static void ScrollDown(uint clicks)
	{
		/* A single 'wheel click' is represented as the value 120. A positive value represents scrolling up (moving the wheel away from the user)
         * Whereas a negative value represents a scrolling down (moving the wheel towards the user). However, the DWORD equivalent in C# is uint,
         * which can't store negative values (as it's unsigned). To counter this, the unchecked operator is used to suppress overflow checking.
         * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/unchecked
         */

		var wheelClickData = unchecked((uint)-120) * clicks;
		MouseAction(0x0800, wheelClickData);
	}

	/// <summary>
	/// Scrolls the mouse wheel up
	/// </summary>
	/// <param name="clicks">The number of mouse wheel 'clicks' to scroll up by</param>
	public static void ScrollUp(uint clicks)
	{
		var wheelClickData = 120 * clicks;
		MouseAction(0x0800, wheelClickData);
	}

	/// <summary>
	/// Returns the current X coordinate of the mouse
	/// </summary>
	public static int XCoordinate => MousePosition.xPosition;

	/// <summary>
	/// Returns the current Y coordinate of the mouse
	/// </summary>
	public static int YCoordinate => MousePosition.yPosition;

	/// <summary>
	/// Holds the information about the mouse X and Y coordinates
	/// </summary>
	private struct POINT
	{
		internal int xPosition;
		internal int yPosition;
	}

	private static POINT _mousePosition;
	/// <summary>
	/// Holds information about the current position of the mouse
	/// </summary>
	private static POINT MousePosition
	{
		get
		{
			GetCursorPos(out _mousePosition);
			return _mousePosition;
		}
	}

	/// <summary>
	/// Performs a mouse action (that is, either Down or Release). This allows a user to perform an action like dragging the mouse 
	/// or long button presses. 
	/// </summary>
	/// <param name="mouseActionCode">HEX code associated with the mouse button and the state for it to be in (Up or Down)</param>
	/// <param name="mouseData">Used by the Scroll functions and represents how many clicks of the scroll wheel they wish to do.
	/// A negative value scrolls down and a positive value scrolls up.</param>
	private static void MouseAction(uint mouseActionCode, uint mouseData = 0)
	{
		var buttonAction = new INPUT
		{
			type = 0,
			union =
			{
				mouseInput = new MOUSEINPUT
				{
					xPosition = 0,
					yPosition = 0,
					mouseData = mouseData,
					mouseAction = mouseActionCode,
					time = 0,
					extraInfo = GetMessageExtraInfo()
				}
			}
		};


		INPUT[] inputs = { buttonAction };

		SendInput(1, inputs, INPUT.Size);
	}
}
