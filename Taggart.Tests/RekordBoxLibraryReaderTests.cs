using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taggart.RekordBox;

namespace Taggart.Tests
{
    [TestClass]
    public class RekordBoxLibraryReaderTests
    {
        string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<DJ_PLAYLISTS Version=""1.0.0"">
  <PRODUCT Name=""rekordbox"" Version=""5.0.3"" Company=""Pioneer DJ"" />
  <COLLECTION Entries=""2"">
    <TRACK TrackID=""1"" Name=""Horny (Radio Slave &amp; Thomas Gandey Just 17 Mix)"" Artist=""Mousse T"" Composer="""" Album=""Beatport T0P 100 March 2012"" Grouping="""" Genre=""Tech House"" Kind=""MP3 File"" Size=""19010917"" TotalTime=""475"" DiscNumber=""0"" TrackNumber=""78"" Year=""2012"" AverageBpm=""124.00"" DateAdded=""2018-01-07"" BitRate=""320"" SampleRate=""44100"" Comments=""1A - 6"" PlayCount=""0"" Rating=""0"" Location=""file://localhost/X://01A%20124%20Horny%20(Radio%20Slave%20%26%20Thomas%20Gandey%20Just%2017%20Mix)%20-%20Mousse%20T.mp3"" Remixer="""" Tonality=""1A"" Label="""" Mix="""">
      <TEMPO Inizio=""0.312"" Bpm=""124.00"" Metro=""4/4"" Battito=""1"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""0.295"" Num=""0"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""93.198"" Num=""1"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""155.134"" Num=""2"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""186.102"" Num=""3"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""217.07"" Num=""4"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""317.716"" Num=""5"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""348.684"" Num=""6"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""395.136"" Num=""7"" Red=""40"" Green=""226"" Blue=""20"" />
    </TRACK>
    <TRACK TrackID=""2"" Name=""Dibiza (Solee Remix)"" Artist=""Danny Tenaglia"" Composer="""" Album=""High Pro-File - One"" Grouping="""" Genre=""Tech House"" Kind=""MP3 File"" Size=""16666774"" TotalTime=""416"" DiscNumber=""0"" TrackNumber=""13"" Year=""2012"" AverageBpm=""125.00"" DateAdded=""2018-01-07"" BitRate=""320"" SampleRate=""44100"" Comments=""1A - 6"" PlayCount=""0"" Rating=""0"" Location=""file://localhost/X://01A%20125%20Dibiza%20(Solee%20Remix)%20-%20Danny%20Tenaglia.mp3"" Remixer=""Solee"" Tonality=""1A"" Label=""High Pro-File Recordings"" Mix="""">
      <POSITION_MARK Name="""" Type=""0"" Start=""0.536"" Num=""0"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""46.612"" Num=""1"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""230.936"" Num=""2"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""138.773"" Num=""3"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""200.213"" Num=""4"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""230.933"" Num=""5"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""323.093"" Num=""6"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""338.453"" Num=""7"" Red=""40"" Green=""226"" Blue=""20"" />
      <TEMPO Inizio=""0.056"" Bpm=""125.00"" Metro=""4/4"" Battito=""4"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""0.536"" Num=""0"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""46.613"" Num=""1"" Red=""40"" Green=""226"" Blue=""20"" />
      <POSITION_MARK Name="""" Type=""0"" Start=""230.936"" Num=""2"" Red=""40"" Green=""226"" Blue=""20"" />
    </TRACK>
  </COLLECTION>
  <PLAYLISTS>
    <NODE Type=""0"" Name=""ROOT"" Count=""2"">
      <NODE Name=""SmartList"" Type=""1"" KeyType=""0"" Entries=""2"">
        <TRACK Key=""1"" />
        <TRACK Key=""2"" />
      </NODE>
      <NODE Name=""Playlist1"" Type=""1"" KeyType=""0"" Entries=""1"">
        <TRACK Key=""2"" />
      </NODE>    
      <NODE Name=""Mainstream"" Type=""0"" Count=""12"">
        <NODE Name=""01M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""02M"" Type=""1"" KeyType=""0"" Entries=""1"">
            <TRACK Key=""2399""/>
        </NODE>
        <NODE Name=""03M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""04M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""05M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""06M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""07M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""08M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""09M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        <NODE Name=""10M"" Type=""1"" KeyType=""0"" Entries=""2"">
            <TRACK Key=""2735""/>
            <TRACK Key=""2510""/>
        </NODE>
        <NODE Name=""11M"" Type=""1"" KeyType=""0"" Entries=""2"">
            <TRACK Key=""2313""/>
            <TRACK Key=""2734""/>
        </NODE>
        <NODE Name=""12M"" Type=""1"" KeyType=""0"" Entries=""0""/>
        </NODE>
    </NODE>
  </PLAYLISTS>
</DJ_PLAYLISTS>";



        [TestMethod]
        public void ReadLibrary()
        {
            var lib = new RekordBoxLibrary(new StringReader(xml));
            Assert.AreEqual(2, lib.Tracks.Count);
            Assert.AreEqual(1, lib.Playlists.Count);
            Assert.AreEqual(3, ((PlaylistFolder)lib.Playlists[0]).Playlists.Count);
        }

        [TestMethod]
        public void WriteLibrary()
        {
            var lib = new RekordBoxLibrary(new StringReader(xml));
            var s = new StringBuilder();
            RekordBoxLibrarySerializer.Serialize(lib, new StringWriter(s));
            System.Console.WriteLine(s.ToString());
        }
    }
}
