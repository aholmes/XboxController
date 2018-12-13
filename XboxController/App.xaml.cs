using System;
using System.Windows;
using System.Diagnostics;
using System.Text;
using System.Windows.Interop;
//using SharpDX.XInput;
using SharpDX.RawInput;

namespace XboxController
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		App()
		{
			InitializeComponent();
		}

		[STAThread]
		static void Main()
		{

			var app = new App();
			var window = new MainWindow();
			Device.RegisterDevice(SharpDX.Multimedia.UsagePage.Generic, SharpDX.Multimedia.UsageId.GenericMouse, DeviceFlags.None);
			//Device.RegisterDevice(SharpDX.Multimedia.UsagePage.Generic, SharpDX.Multimedia.UsageId.GenericGamepad, DeviceFlags.None);
			//Device.RawInput += Device_RawInput;
			Device.MouseInput += Device_MouseInput;
			app.Run(window);
		}

		private static void Device_MouseInput(object sender, MouseInputEventArgs e)
		{
			Debug.WriteLine("mouse");
		}

		private static void Device_RawInput(object sender, RawInputEventArgs e)
		{
			Debug.WriteLine("input");
		}

		private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
		{
			if (msg.message == 15
			|| msg.message == 275
			|| msg.message == 512
			|| msg.message == 49371) return;
			/// 00ff - 255 is WM_INPUT
			if (msg.message != 0x00ff) return;// || !controller.IsConnected) return;
			var sb = new StringBuilder();
			sb.AppendLine(msg.message.ToString());

			//var gamepad = controller.GetState().Gamepad;


			//sb.Append((Math.Abs((float)gamepad.LeftThumbX) < deadband ? 0 : (float)gamepad.LeftThumbX / short.MinValue * -100)+"|");
			//sb.Append((Math.Abs((float)gamepad.LeftThumbY) < deadband ? 0 : (float)gamepad.LeftThumbY / short.MaxValue * 100)+"|");
			//sb.Append((Math.Abs((float)gamepad.RightThumbX) < deadband ? 0 : (float)gamepad.RightThumbX / short.MaxValue * 100)+"|");
			//sb.Append((Math.Abs((float)gamepad.RightThumbY) < deadband ? 0 : (float)gamepad.RightThumbY / short.MaxValue * 100)+"|");

			//sb.Append(gamepad.LeftTrigger+"|");
			//sb.Append(gamepad.RightTrigger);
			Debug.WriteLine(sb);

			//	//if (!IsLoaded || msg != 0x00ff) return IntPtr.Zero;
			//	// Handle messages...

			//	RawInput.Net.RawInput.ProcessInput(msg.wParam, msg.lParam,
			//		out ushort button, out int x, out int y);

			//	System.Diagnostics.Debug.Write(button + "|");
			//	//System.Diagnostics.Debug.Write(x+"|");
			//	//System.Diagnostics.Debug.WriteLine(y);


			handled = true;

		//	//return IntPtr.Zero;
		}
		/*
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
			var numberOfActiveDevices = RawInput.Net.RawInput.InitialiseGamepads(mainWindowHandle);

			//var devices = new IntPtr[numberOfActiveDevices];
			//for (var deviceIndex = 0; deviceIndex < numberOfActiveDevices; deviceIndex++)
			//{
			//	devices[deviceIndex] = RawInput.Net.RawInput.GetDevicePath(deviceIndex);
			//}

			IsLoaded = true;
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;
			//base.OnSourceInitialized(e);
			//var source = PresentationSource.FromVisual(this) as HwndSource;
			//source.AddHook(WndProc);
		}

		private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
		{
			if (!IsLoaded || msg.message != 0x00ff) return;

		// 00ff - 255 is WM_INPUT
			//if (!IsLoaded || msg != 0x00ff) return IntPtr.Zero;
			// Handle messages...

			RawInput.Net.RawInput.ProcessInput(msg.wParam, msg.lParam,
				out byte buttons, out int x, out int y);

			//System.Diagnostics.Debug.Write(buttons + "|");
			//System.Diagnostics.Debug.Write(x+"|");
			//System.Diagnostics.Debug.WriteLine(y);


			handled = true;

			//return IntPtr.Zero;
		}

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
		// 00ff - 255 is WM_INPUT
			if (!IsLoaded || msg != 0x00ff) return IntPtr.Zero;
			// Handle messages...

			RawInput.Net.RawInput.ProcessInput(wParam, lParam,
				out byte buttons, out int x, out int y);

			//System.Diagnostics.Debug.Write(buttons + "|");
			//System.Diagnostics.Debug.Write(x+"|");
			//System.Diagnostics.Debug.WriteLine(y);


			handled = true;

			return IntPtr.Zero;
		}
		*/
	}
}
