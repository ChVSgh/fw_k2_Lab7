using System;
using System.Windows.Forms;

namespace Lab7.WinForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Lab7.WinForm.Form1()); 
        }
    }
}