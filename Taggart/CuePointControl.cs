using System;
using System.Windows.Forms;

namespace Taggart
{
    public partial class CuePointControl : UserControl
    {

        int? seconds;

        public CuePointControl()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get
            {
                return lblLetter.Text;
            }
            set
            {
                lblLetter.Text = value;
            }
        }

        public int? Time
        {
            get { return seconds; }
            set
            {
                seconds = value;
                if (value.HasValue)
                {
                    var time = TimeSpan.FromMilliseconds(value.Value);
                    lblTime.Text = $"{time.Minutes:D2}:{time.Seconds:D2}:{time.Milliseconds:D3}";
                    lblLetter.BackColor = System.Drawing.Color.GreenYellow;
                }
                else
                {
                    lblTime.Text = string.Empty;
                    lblLetter.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
        private void lblLetter_Click(object sender, EventArgs e)
        {

        }
    }
}
