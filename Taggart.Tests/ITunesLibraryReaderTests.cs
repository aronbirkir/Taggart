using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taggart.ITunes;

namespace Taggart.Tests
{
    [TestClass]
    public class ITunesLibraryReaderTests
    {
        string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<!DOCTYPE plist PUBLIC ""-//Apple Computer//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">
<plist version=""1.0"">
<dict>
    <key>Major Version</key><integer>1</integer>
    <key>Minor Version</key><integer>1</integer>
    <key>Date</key><date>2018-02-12T23:01:23Z</date>
    <key>Application Version</key><string>12.7.3.46</string>
    <key>Features</key><integer>5</integer>
    <key>Show Content Ratings</key><true/>
    <key>Music Folder</key><string>file://localhost/E:/iTunes2/iTunes/iTunes%20Media/</string>
    <key>Library Persistent ID</key><string>60BB7CE963EE5E93</string>
    <key>Tracks</key>
    <dict>
        <key>9997</key>
        <dict>
            <key>Track ID</key><integer>9997</integer>
            <key>Name</key><string>Private Dancer</string>
            <key>Artist</key><string>Tina Turner</string>
            <key>Album</key><string>80s Collection (Disc 6)</string>
            <key>Genre</key><string>80s</string>
            <key>Kind</key><string>MPEG audio file</string>
            <key>Size</key><integer>6878123</integer>
            <key>Total Time</key><integer>430785</integer>
            <key>Disc Number</key><integer>6</integer>
            <key>Disc Count</key><integer>14</integer>
            <key>Track Number</key><integer>4</integer>
            <key>Track Count</key><integer>12</integer>
            <key>BPM</key><integer>117</integer>
            <key>Date Modified</key><date>2016-03-03T22:30:10Z</date>
            <key>Date Added</key><date>2017-12-14T23:55:42Z</date>
            <key>Bit Rate</key><integer>128</integer>
            <key>Sample Rate</key><integer>44100</integer>
            <key>Comments</key><string>9B</string>
            <key>Normalization</key><integer>928</integer>
            <key>Compilation</key><true/>
            <key>Persistent ID</key><string>877BFCF14608DBBB</string>
            <key>Track Type</key><string>File</string>
            <key>Location</key><string>file://localhost/E:/music/80s/09B%20117%20Private%20Dancer%20-%20Tina%20Turner.mp3</string>
            <key>File Folder Count</key><integer>-1</integer>
            <key>Library Folder Count</key><integer>-1</integer>
        </dict>
        <key>9999</key>
        <dict>
            <key>Track ID</key><integer>9999</integer>
            <key>Name</key><string>Boys Say Go! (live)</string>
            <key>Artist</key><string>Depeche Mode</string>
            <key>Album</key><string>Everything Counts</string>
            <key>Genre</key><string>80s</string>
            <key>Kind</key><string>MPEG audio file</string>
            <key>Size</key><integer>2503673</integer>
            <key>Total Time</key><integer>156368</integer>
            <key>BPM</key><integer>142</integer>
            <key>Date Modified</key><date>2017-12-07T18:01:16Z</date>
            <key>Date Added</key><date>2017-12-14T23:55:42Z</date>
            <key>Bit Rate</key><integer>128</integer>
            <key>Sample Rate</key><integer>44100</integer>
            <key>Comments</key><string>6A - Cannapower</string>
            <key>Persistent ID</key><string>A36BF51D811502EE</string>
            <key>Track Type</key><string>File</string>
            <key>Location</key><string>file://localhost/E:/music/80s/06A%20142%20Boys%20Say%20Go!%20(live)%20-%20Depeche%20Mode.mp3</string>
            <key>File Folder Count</key><integer>-1</integer>
            <key>Library Folder Count</key><integer>-1</integer>
        </dict>
        <key>10001</key>
        <dict>
            <key>Track ID</key><integer>10001</integer>
            <key>Name</key><string>Everything Counts (Original 12</string>
            <key>Artist</key><string>Depeche Mode</string>
            <key>Album</key><string>Everything Counts</string>
            <key>Genre</key><string>80s</string>
            <key>Kind</key><string>MPEG audio file</string>
            <key>Size</key><integer>7041481</integer>
            <key>Total Time</key><integer>439980</integer>
            <key>BPM</key><integer>114</integer>
            <key>Date Modified</key><date>2017-12-07T18:01:18Z</date>
            <key>Date Added</key><date>2017-12-14T23:55:42Z</date>
            <key>Bit Rate</key><integer>128</integer>
            <key>Sample Rate</key><integer>44100</integer>
            <key>Comments</key><string>8B - Cannapower</string>
            <key>Persistent ID</key><string>1E0B5027EAB3A708</string>
            <key>Track Type</key><string>File</string>
            <key>Location</key><string>file://localhost/E:/music/80s/08B%20114%20Everything%20Counts%20(Original%2012%20-%20Depeche%20Mode.mp3</string>
            <key>File Folder Count</key><integer>-1</integer>
            <key>Library Folder Count</key><integer>-1</integer>
        </dict>
      </dict>
      <key>Playlists</key>
      <array>
        <dict>
            <key>Name</key><string>Library</string>
            <key>Description</key><string></string>
            <key>Master</key><true/>
            <key>Playlist ID</key><integer>31883</integer>
            <key>Playlist Persistent ID</key><string>533C20F03A4F4A54</string>
            <key>Visible</key><false/>
            <key>All Items</key><true/>
            <key>Playlist Items</key>
            <array>
                <dict>
                    <key>Track ID</key><integer>9997</integer>
                </dict>
                <dict>
                    <key>Track ID</key><integer>9999</integer>
                </dict>
                <dict>
                    <key>Track ID</key><integer>10001</integer>
                </dict>
            </array>
        </dict>
    </array>
</dict>
</plist>";

        [TestMethod]
        public void ReadLibrary()
        {
            var lib = new ITunesLibrary(new StringReader(xml));
            Assert.AreEqual(2, lib.Tracks.Count);
            //Assert.AreEqual(0, lib.Playlists.Count);
            //Assert.AreEqual(3, ((PlaylistFolder)lib.Playlists[0]).Playlists.Count);
        }
    }
}
