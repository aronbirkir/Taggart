using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Taggart
{
    public partial class MainForm : Form
    {
        private List<CuePointControl> cuePoints;
        private List<CuePointControl> mikCuePoints;
        private ITrackLibrary trackLibrary;

        public MainForm()
        {
            InitializeComponent();
            cuePoints = new List<CuePointControl> { cuepointA, cuePointB, cuePointC, cuePointD, cuePointE, cuePointF, cuePointG, cuePointH };
            mikCuePoints = new List<CuePointControl> { cuePointMik1, cuePointMik2, cuePointMik3, cuePointMik4, cuePointMik5, cuePointMik6, cuePointMik7, cuePointMik8 };
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                //var info = Helper.ExtractMixedInKeyInfo(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                trackLibrary = TrackLibraryFactory.CreateFromFile(textBox1.Text);
                TrackLibraryDbSaver.Save(textBox1.Text, trackLibrary);
                var bindingList = new BindingList<TrackInfo>(trackLibrary.Tracks);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
                dataGridView1.AutoResizeColumns();
            }
        }

        private void SetCuePoints(List<CuePointControl> cuePointControls, ICollection<CuePoint> cuePoints)
        {
            var mapOfCuePoints = cuePoints?.ToDictionary(x => x.Number) ?? new Dictionary<int, CuePoint>();
            for (int i = 0; i < cuePointControls.Count; i++)
            {
                cuePointControls[i].Time = null;
                cuePointControls[i].Tag = null;
                if (mapOfCuePoints.ContainsKey(i))
                {
                    cuePointControls[i].Time = mapOfCuePoints[i].Time;
                    cuePointControls[i].Tag = mapOfCuePoints[i];
                }
            }
        }

        private ICollection<CuePoint> CopyCuePointValues(List<CuePointControl> cuePointControls, ICollection<CuePoint> cuePoints = null, bool leaveExisting = false)
        {
            var newCuePoints = new List<CuePoint>();
            var mapOfExistingCuePoints = cuePoints?.ToDictionary(x => x.Number) ?? new Dictionary<int, CuePoint>();
            for (int i = 0; i < cuePointControls.Count; i++)
            {
                var cuePoint = cuePointControls[i].Tag as CuePoint;
                if (cuePoint != null)
                {
                    if (leaveExisting && mapOfExistingCuePoints.ContainsKey(cuePoint.Number))
                    {
                        newCuePoints.Add(mapOfExistingCuePoints[cuePoint.Number]);
                    }
                    else
                    {
                        newCuePoints.Add(new CuePoint { Number = cuePoint.Number, Name = cuePoint.Name, Time = cuePoint.Time });
                    }
                }
            }
            return newCuePoints;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selectedTrack = dataGridView1.CurrentRow.DataBoundItem as TrackInfo;
            if (selectedTrack != null)
            {
                SetCuePoints(cuePoints, selectedTrack.CuePoints);
                CuePoint[] trackMikCuePoints = null;
                if (cbAutoMik.Checked)
                {
                    var info = Helper.ExtractMixedInKeyInfo(selectedTrack.FileName);
                    trackMikCuePoints = info.CuePoints;
                }
                SetCuePoints(mikCuePoints, trackMikCuePoints);
            }
        }

        private void btnReadMIK_Click(object sender, EventArgs e)
        {
            var selectedTrack = dataGridView1.CurrentRow.DataBoundItem as TrackInfo;
            if (selectedTrack != null)
            {
                var info = Helper.ExtractMixedInKeyInfo(selectedTrack.FileName);
                SetCuePoints(mikCuePoints, info.CuePoints);
            }
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            var selectedTrack = dataGridView1.CurrentRow.DataBoundItem as TrackInfo;
            if (selectedTrack != null)
            {
                selectedTrack.SetCuePoints(CopyCuePointValues(mikCuePoints).ToArray());
                SetCuePoints(cuePoints, selectedTrack.CuePoints);
                dataGridView1.InvalidateRow(dataGridView1.CurrentRow.Index);
            }
        }

        private void btnCopyMissing_Click(object sender, EventArgs e)
        {
            var selectedTrack = dataGridView1.CurrentRow.DataBoundItem as TrackInfo;
            if (selectedTrack != null)
            {
                selectedTrack.SetCuePoints(CopyCuePointValues(mikCuePoints, selectedTrack.CuePoints, true));
                SetCuePoints(cuePoints, selectedTrack.CuePoints);
                dataGridView1.InvalidateRow(dataGridView1.CurrentRow.Index);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var track = this.dataGridView1.Rows[e.RowIndex].DataBoundItem as TrackInfo;
            if (track != null && track.IsDirty())
            {
                e.CellStyle.BackColor = System.Drawing.Color.Pink;
            }
        }

        private void btnSaveLibrary_Click(object sender, EventArgs e)
        {
            Helper.SaveLibrary(trackLibrary, textBox1.Text, "COLLECTION");
        }
    }
}
