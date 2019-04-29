using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taggart.Data;
using Taggart.Data.Models;

namespace Taggart
{
    public partial class MainForm : Form
    {
        private List<CuePointControl> cuePoints;
        private List<CuePointControl> mikCuePoints;
        private ITrackLibrary trackLibrary;
        private TrackLibraryContext db;

        public MainForm()
        {
            InitializeComponent();
            cuePoints = new List<CuePointControl> { cuepointA, cuePointB, cuePointC, cuePointD, cuePointE, cuePointF, cuePointG, cuePointH };
            mikCuePoints = new List<CuePointControl> { cuePointMik1, cuePointMik2, cuePointMik3, cuePointMik4, cuePointMik5, cuePointMik6, cuePointMik7, cuePointMik8 };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load libraries from the db and populate the library tree
            db = new TrackLibraryContext();
            LoadLibraryTree();


        }

        private void LoadLibraryTree()
        {
            treeLibrary.Nodes.Clear();
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "librariesNode";
            mainNode.Text = "Libaries";
            treeLibrary.Nodes.Add(mainNode);
            var libraries = DataAccess.GetLibrariesInfo(db);
            foreach (var libInfo in libraries)
            {
                mainNode.Nodes.Add(new TreeNode
                {
                    Name = libInfo.File,
                    Text = $"{libInfo.Name} ({libInfo.TrackCount})",
                    Tag = libInfo
                });
            }
            mainNode.ExpandAll();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                libraryFileNameInput.Text = openFileDialog1.FileName;
                //var info = Helper.ExtractMixedInKeyInfo(openFileDialog1.FileName);
            }
        }

        private void ScanLibrary_Click(object sender, EventArgs e)
        {
            if (File.Exists(libraryFileNameInput.Text))
            {
                trackLibrary = TrackLibraryFactory.CreateFromFile(LibraryType.RekordBox, libraryFileNameInput.Text);
                TrackLibraryDbSaver.Save(libraryFileNameInput.Text, trackLibrary);
                var bindingList = new BindingList<TrackInfo>(trackLibrary.Tracks);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
                dataGridView1.AutoResizeColumns();
            }
        }

        private void btnSaveLibrary_Click(object sender, EventArgs e)
        {
            //Helper.SaveLibrary(trackLibrary, textBox1.Text, "COLLECTION");
            var libraryFileName = libraryFileNameInput.Text;
            var directory = Path.GetDirectoryName(libraryFileName);
            var tempFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(libraryFileName) + "_tmp" + Path.GetExtension(libraryFileName));
            using (var writer = new StreamWriter(libraryFileName))
            {
                RekordBoxLibrarySerializer.Serialize(trackLibrary, writer);
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
            var track = dataGridView1.Rows[e.RowIndex].DataBoundItem as TrackInfo;
            if (track != null && track.IsDirty())
            {
                e.CellStyle.BackColor = System.Drawing.Color.Pink;
            }
        }


        private void btn_checkFiles_Click(object sender, EventArgs e)
        {
            IProgress<int> progress = new Progress<int>(value => { lblProgress.Text = value.ToString(); });
            Task task = Task.Run(() => CheckFiles(progress));
        }

        private void CheckFiles(IProgress<int> progress)
        {
            using (var db = new TrackLibraryContext())
            {
                // Loop library tracks and check their existence.
                // For tracks missing file existence, search the library to see if same artist and song exist
                int count = 0;
                foreach (var track in trackLibrary.Tracks)
                {
                    progress.Report(count++);
                    if (!File.Exists(track.FileName))
                    {
                        var dbTrack = db.Tracks.Where(x => x.Name == track.Name && x.Artist == track.Artist).FirstOrDefault();
                        if (dbTrack != null)
                        {
                            track.SetFileExists(false, dbTrack.TrackId);
                        }
                        else
                        {
                            track.SetFileExists(false, null);
                        }
                    }
                    else
                    {
                        // Check database to see if we can match the song with another library track
                        track.SetFileExists(true, null);
                    }
                }
            }
        }

        private void btnDeleteLibrary_Click(object sender, EventArgs e)
        {
            if (treeLibrary.SelectedNode != null)
            {
                var libInfo = treeLibrary.SelectedNode.Tag as LibraryInfo;
                if (libInfo != null)
                {
                    DataAccess.DeleteLibrary(db, libInfo.LibraryId);
                    var libNode = treeLibrary.SelectedNode;
                    treeLibrary.SelectedNode = libNode.Parent;
                    treeLibrary.Nodes.Remove(libNode);

                }
            }
        }

        private void btnAddLibrary_Click(object sender, EventArgs e)
        {
            // Open dialog
        }

        private void treeLibrary_Click(object sender, EventArgs e)
        {
            // Show tracks from the selected library
        }

        private void treeLibrary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Show tracks from the selected library
            var libInfo = e.Node.Tag as LibraryInfo;
            if (libInfo != null)
            {
                ShowLibraryTracks(libInfo.LibraryId);
                libraryFileNameInput.Text = libInfo.File;
            }

        }

        private void ShowLibraryTracks(int libraryId)
        {
            //var tracks = DataAccess.GetLibraryTracks(db, libraryId);
            db.Tracks.Where(x => x.LibraryId == libraryId).Load();
            trackInfoBindingSource.DataSource = DataAccess.GetLibraryTracks(db, libraryId);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
