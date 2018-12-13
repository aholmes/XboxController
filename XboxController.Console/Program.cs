using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Multimedia;
using SharpDX.RawInput;
using Microsoft.VisualBasic.PowerPacks;

namespace XboxController.Console
{
	class Program
	{
		private static TextBox textBox;
		private static Dictionary<XboxControllerButton, OvalShape> buttons;

		[STAThread]
		static void Main()
		{
			var form = new Form { Width = 1300, Height = 600};
			textBox = new TextBox() { Dock = DockStyle.Top, Height = 200, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true, Font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular) };

			var sc = new ShapeContainer();// { Dock = DockStyle.Top, Left = 300, Top = 50, Height = 400, Width = 800 };
			buttons = new Dictionary<XboxControllerButton, OvalShape>(8);
			for (var i = 0; i < 8; i++)
			{
				buttons[(XboxControllerButton)(1<<i)] = new OvalShape(sc) { Width = 10, Height = 10, FillStyle = FillStyle.Solid };
			}

			PositionPrimaryButtons((225, 200), 50);

			form.Controls.Add(textBox);
			form.Controls.Add(sc);
			form.Visible = true;

			Device.RegisterDevice(UsagePage.Generic, UsageId.GenericGamepad, DeviceFlags.None);
			Device.RawInput += (sender, args) =>textBox.Invoke(new UpdateTextCallback(UpdateGamepadText), args);
			Device.RawInput += HandleButtonInput;

			Application.Run(form);
		}

		static void PositionPrimaryButtons((int Top, int Left) startingPosition, int scale = 50)
		{
			buttons[XboxControllerButton.A].Top = startingPosition.Top + scale;
			buttons[XboxControllerButton.A].Left = startingPosition.Left + scale / 2;
			buttons[XboxControllerButton.B].Top = startingPosition.Top + scale / 2;
			buttons[XboxControllerButton.B].Left = startingPosition.Left + scale;
			buttons[XboxControllerButton.X].Top = startingPosition.Top + scale / 2;
			buttons[XboxControllerButton.X].Left = startingPosition.Left;
			buttons[XboxControllerButton.Y].Top = startingPosition.Top;
			buttons[XboxControllerButton.Y].Left = startingPosition.Left + scale / 2;
			buttons[XboxControllerButton.LeftBumper].Top = startingPosition.Top - scale / 2;
			buttons[XboxControllerButton.RightBumper].Top = startingPosition.Top - scale / 2;
			buttons[XboxControllerButton.RightBumper].Left = startingPosition.Left + scale / 2;
			buttons[XboxControllerButton.View].Top = startingPosition.Top + scale / 2;
			buttons[XboxControllerButton.View].Left = startingPosition.Left / 2 - scale / 2;
			buttons[XboxControllerButton.Menu].Top = startingPosition.Top + scale / 2;
			buttons[XboxControllerButton.Menu].Left = startingPosition.Left / 2 + scale;
		}

		static void HandleButtonInput(object sender, RawInputEventArgs rawArgs)
		{
			var args = (HidInputEventArgs)rawArgs;
			var state = new XboxControllerState(args.RawData);

			buttons[XboxControllerButton.A].FillColor = state.Buttons.A ? Color.Red : Color.Black;
			buttons[XboxControllerButton.B].FillColor = state.Buttons.B ? Color.Red : Color.Black;
			buttons[XboxControllerButton.X].FillColor = state.Buttons.X ? Color.Red : Color.Black;
			buttons[XboxControllerButton.Y].FillColor = state.Buttons.Y ? Color.Red : Color.Black;
			buttons[XboxControllerButton.LeftBumper].FillColor = state.Buttons.LeftBumper ? Color.Red : Color.Black;
			buttons[XboxControllerButton.RightBumper].FillColor = state.Buttons.RightBumper ? Color.Red : Color.Black;
			buttons[XboxControllerButton.View].FillColor = state.Buttons.View ? Color.Red : Color.Black;
			buttons[XboxControllerButton.Menu].FillColor = state.Buttons.Menu ? Color.Red : Color.Black;
		}

		static void UpdateGamepadText(RawInputEventArgs rawArgs)
		{
			var sb = new StringBuilder();
			var args = (HidInputEventArgs)rawArgs;
			foreach(var d in args.RawData.Select((v,i) => new { v, i }))
			{
				sb.AppendFormat("{0}:{1,-6} |", d.i, d.v);
			}
			textBox.AppendText(sb.AppendLine().ToString());
		}

		public delegate void UpdateTextCallback(RawInputEventArgs args);
	}
}
