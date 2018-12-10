using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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

			var intPtr = IntPtr.Zero;
			var numberOfActiveDevices = RawInput.Net.RawInput.InitialiseGamepads(intPtr);

			var devices = new IntPtr[numberOfActiveDevices];
			for (var deviceIndex = 0; deviceIndex < numberOfActiveDevices; deviceIndex++)
			{
				devices[deviceIndex] = RawInput.Net.RawInput.GetDevicePath(deviceIndex);
			}

			while(true)
			{

				RawInput.Net.RawInput.ProcessInput(IntPtr.Zero, devices[0],
					out byte buttons, out int x, out int y);

				System.Diagnostics.Debug.Write(buttons);
				System.Diagnostics.Debug.Write(x);
				System.Diagnostics.Debug.WriteLine(y);
				System.Threading.Thread.Sleep(100);
			}
		}
	}
}
