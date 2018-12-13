using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxController.Console
{
	[Flags]
	public enum XboxControllerButton: byte
	{
		A           = 1,
		B           = 1<<1,
		X           = 1<<2,
		Y           = 1<<3,
		LeftBumper  = 1<<4,
		RightBumper = 1<<5,
		View        = 1<<6,
		Menu        = 1<<7
	}

	[Flags]
	public enum XboxControllerDPad: byte
	{
		Up        = 1<<2,//4,  // 000100
		RightUp   = 2<<2,//8,  // 001000
		Right     = 3<<2,//12, // 001100
		RightDown = 4<<2,//16, // 010000
		Down      = 5<<2,//20, // 010100
		LeftDown  = 6<<2,//24, // 011000
		Left      = 7<<2,//28, // 011100
		LeftUp    = 8<<2,//32, // 100000
	}

	public class XboxControllerState
	{
		public readonly XboxControllerStateButtons Buttons;
		public XboxControllerState(byte[] rawInput)
		{
			Buttons = new XboxControllerStateButtons((XboxControllerButton)rawInput[11]);
		}
	}

	public class XboxControllerStateButtons
	{
		private XboxControllerButton _buttons;
		public XboxControllerStateButtons(XboxControllerButton buttons)
		{
			_buttons = buttons;
		}

		public bool A           => (_buttons & XboxControllerButton.A) > 0;
		public bool B           => (_buttons & XboxControllerButton.B) > 0;
		public bool X           => (_buttons & XboxControllerButton.X) > 0;
		public bool Y           => (_buttons & XboxControllerButton.Y) > 0;
		public bool LeftBumper  => (_buttons & XboxControllerButton.LeftBumper) > 0;
		public bool RightBumper => (_buttons & XboxControllerButton.RightBumper) > 0;
		public bool View        => (_buttons & XboxControllerButton.View) > 0;
		public bool Menu        => (_buttons & XboxControllerButton.Menu) > 0;
	}
}
