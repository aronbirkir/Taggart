using System.Windows.Forms;

namespace Taggart
{
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
        }

        public void Add(Control c)
        {
            flpListBox.Controls.Add(c);
            SetupAnchors();
        }

        public void Remove(string name)
        {
            var c = flpListBox.Controls[name];
            flpListBox.Controls.Remove(c);
            c.Dispose();
            SetupAnchors();
        }

        public void Clear()
        {

            while (flpListBox.Controls.Count > 0)
            {
                var c = flpListBox.Controls[0];
                flpListBox.Controls.Remove(c);
                c.Dispose();
            }
        }

        public void SetupAnchors()
        {
            if (flpListBox.Controls.Count > 0)
            {
                for (int i = 0; i < flpListBox.Controls.Count; i++)
                {
                    var ctrl = flpListBox.Controls[i];
                    if (i == 0)
                    {
                        ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                        ctrl.Width = flpListBox.Width - SystemInformation.VerticalScrollBarWidth;
                    }
                    else
                    {
                        ctrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    }
                }
            }
        }

        private void FlpListBox_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (flpListBox.Controls.Count > 0)
            {
                flpListBox.Controls[0].Width = flpListBox.Size.Width - SystemInformation.VerticalScrollBarWidth;
            }
        }

        public int Count
        {
            get { return flpListBox.Controls.Count; }
        }
    }
}
