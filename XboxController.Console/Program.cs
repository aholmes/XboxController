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

		[STAThread]
		static void Main()
		{
			var form = new Form { Width = 1300, Height = 600};
			textBox = new TextBox() { Dock = DockStyle.Top, Height = 200, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true, Font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Regular) };

			var sc = new ShapeContainer(){ Dock = DockStyle.Top, Height = 400, Width = 800 };
			var buttons = new Dictionary<XboxControllerButton, OvalShape>(8);
			for (var i = 0; i < 8; i++)
			{
				buttons[(XboxControllerButton)(1<<i)] = new OvalShape(sc) { Top = 200, Left = 10 * i, Width = 10, Height = 10, FillStyle = FillStyle.Solid };
			}
			form.Controls.Add(textBox);
			form.Controls.Add(sc);
			form.Visible = true;

			Device.RegisterDevice(UsagePage.Generic, UsageId.GenericGamepad, DeviceFlags.None);
			Device.RawInput += (sender, args) =>textBox.Invoke(new UpdateTextCallback(UpdateGamepadText), args);

			Device.RawInput += (s,e) =>
			{
				var args = (HidInputEventArgs)e;
				var state = new XboxControllerState(args.RawData);

				buttons[XboxControllerButton.A].FillColor = state.Buttons.A ? Color.Red : Color.Black;
				buttons[XboxControllerButton.B].FillColor = state.Buttons.B ? Color.Red : Color.Black;
				buttons[XboxControllerButton.X].FillColor = state.Buttons.X ? Color.Red : Color.Black;
				buttons[XboxControllerButton.Y].FillColor = state.Buttons.Y ? Color.Red : Color.Black;
				buttons[XboxControllerButton.LeftBumper].FillColor = state.Buttons.LeftBumper ? Color.Red : Color.Black;
				buttons[XboxControllerButton.RightBumper].FillColor = state.Buttons.RightBumper ? Color.Red : Color.Black;
				buttons[XboxControllerButton.View].FillColor = state.Buttons.View ? Color.Red : Color.Black;
				buttons[XboxControllerButton.Menu].FillColor = state.Buttons.Menu ? Color.Red : Color.Black;
			};

			Application.Run(form);
		}

		static void UpdateGamepadText(RawInputEventArgs rawArgs)
		{
			var sb = new StringBuilder();
			var args = (HidInputEventArgs)rawArgs;
			foreach(var d in args.RawData.Select((v,i) => new { v, i }))
			{
				sb.AppendFormat("{0}:{1,-6} |", d.i, d.v);
			}
			//Debug.WriteLine(sb);
			textBox.AppendText(sb.AppendLine().ToString());
		}

		static void UpdateMouseText(RawInputEventArgs rawArgs)
		{
			var args = (MouseInputEventArgs)rawArgs;

			textBox.AppendText(string.Format("(x,y):({0},{1}) Buttons: {2} State: {3} Wheel: {4}\r\n", args.X, args.Y, args.ButtonFlags, args.Mode, args.WheelDelta));
		}
		public delegate void UpdateTextCallback(RawInputEventArgs args);
	}
}
