using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XboxController
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
			var numberOfActiveDevices = RawInput.Net.RawInput.InitialiseGamepads(mainWindowHandle);
			ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;

			//var devices = new IntPtr[numberOfActiveDevices];
			//for (var deviceIndex = 0; deviceIndex < numberOfActiveDevices; deviceIndex++)
			//{
			//	devices[deviceIndex] = RawInput.Net.RawInput.GetDevicePath(deviceIndex);
			//}
		}

		private void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
		{
			if (msg.message != 0x00ff) return;

		// 00ff - 255 is WM_INPUT
			//if (!IsLoaded || msg != 0x00ff) return IntPtr.Zero;
			// Handle messages...

			RawInput.Net.RawInput.ProcessInput(msg.wParam, msg.lParam,
				out ushort button, out int x, out int y);

			System.Diagnostics.Debug.Write(button + "|");
			//System.Diagnostics.Debug.Write(x+"|");
			//System.Diagnostics.Debug.WriteLine(y);


			handled = true;

			//return IntPtr.Zero;
		}
	}
}
