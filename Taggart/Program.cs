using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using Taggart.Data;

namespace Taggart
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            using (var ctx = new TrackLibraryContext())
            {
                var libraries = ctx.Libraries.ToList();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
