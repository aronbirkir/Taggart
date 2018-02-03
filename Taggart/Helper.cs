using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Taggart
{
    public static class Helper
    {

        public static string Hex2String(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            string hexString = sb.ToString();
            return hexString;
        }
        public static MixedInKeyInfo ExtractMixedInKeyInfo(string fileName)
        {
            MixedInKeyInfo info = new MixedInKeyInfo();
            var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            var tagFile = TagLib.File.Create(fileName);
            var Id3V2Tag = tagFile.GetTag(TagLib.TagTypes.Id3v2) as TagLib.Id3v2.Tag;
            if (Id3V2Tag != null)
            {
                var GEOBFrames = Id3V2Tag.GetFrames("GEOB");
                foreach (var frame in GEOBFrames)
                {
                    var geobFrame = frame as TagLib.Id3v2.GeneralEncapsulatedObjectFrame;
                    if (geobFrame != null)
                    {

                        var data = encoding.GetString(geobFrame.Object.Data);
                        if (data.StartsWith("ey"))
                        {
                            var base64Decoded = System.Convert.FromBase64String(data);
                            var data2 = encoding.GetString(base64Decoded);
                            if (data2.StartsWith("{"))
                            {
                                var json = JsonConvert.DeserializeObject(data2) as JObject;
                                switch (geobFrame.Description)
                                {
                                    case "CuePoints":
                                        var cuePoints = new List<CuePoint>();
                                        var cues = json["cues"];
                                        int count = 0;
                                        foreach (JObject cue in cues)
                                        {
                                            cuePoints.Add(new CuePoint { Number = count++, Name = cue["name"].Value<string>(), Time = cue["time"].Value<int>() });
                                        }
                                        info.CuePoints = cuePoints.Take(8).ToArray();
                                        break;
                                    case "Key":
                                        info.Key = json["key"].Value<string>();
                                        break;
                                    case "Energy":
                                        info.Energy = json["energyLevel"].Value<int>();
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return info;
        }

        public class MixedInKeyInfo
        {
            public string Key { get; set; }

            public int Energy { get; set; }

            public CuePoint[] CuePoints { get; set; }
        }

        internal static void SaveLibrary(ITrackLibrary trackLibrary, string libraryFileName, string trackContainerElementName)
        {
            // Get dirty tracks and 
            var directory = Path.GetDirectoryName(libraryFileName);
            var tempFile = Path.Combine(directory, Path.GetFileNameWithoutExtension(libraryFileName) + "_tmp" + Path.GetExtension(libraryFileName));
            using (XmlReader reader = XmlReader.Create(libraryFileName))
            using (XmlTextWriter writer = new XmlTextWriter(tempFile, Encoding.UTF8))
            {
                bool insideTracks = false;
                writer.Formatting = System.Xml.Formatting.Indented; // optional, if you want it to look nice
                while (reader.Read())
                {
                    bool hasReachedTracks = false;
                    bool skipPositionMarks = false;
                    if (reader.NodeType == XmlNodeType.XmlDeclaration)
                        writer.WriteStartDocument();
                    if (reader.IsStartElement() && reader.NodeType == XmlNodeType.Element && reader.Name == trackContainerElementName)
                    {
                        hasReachedTracks = true;
                    }
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == trackContainerElementName)
                    {
                        insideTracks = false;
                    }

                    if (!insideTracks)
                    {
                        //writer.WriteNode(reader, false);
                        // write element and all attributes
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            writer.WriteStartElement(reader.Name);
                            writer.WriteAttributes(reader, false);
                            if (reader.IsEmptyElement)
                                writer.WriteEndElement();
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            writer.WriteEndElement();
                        }
                        else if (reader.NodeType == XmlNodeType.Attribute)
                        {
                            writer.WriteAttributes(reader, false);
                        }

                    }
                    else if (reader.Name == "TRACK")
                    {
                        if (reader.IsStartElement())
                        {
                            writer.WriteStartElement(reader.Name);
                            writer.WriteAttributes(reader, false);
                            var trackIdString = reader.GetAttribute("TrackID");
                            int trackId;
                            if (int.TryParse(trackIdString, out trackId))
                            {
                                // Find track from library and write cue points ( if dirty )
                                var track = trackLibrary.Tracks.FirstOrDefault(x => x.TrackId == trackId);
                                if (track != null && track.IsDirty() && track.CuePoints != null)
                                {
                                    skipPositionMarks = true;
                                    foreach (var cuePoint in track.CuePoints)
                                    {
                                        // <POSITION_MARK Name="" Type="0" Start="230.936" Num="2" Red="40" Green="226" Blue="20" />
                                        writer.WriteStartElement("POSITION_MARK");
                                        writer.AddAttribute("Name", cuePoint.Name);
                                        writer.AddAttribute("Type", "0");
                                        writer.AddAttribute("Start", ((float)cuePoint.Time / 1000).ToString(CultureInfo.InvariantCulture));
                                        writer.AddAttribute("Num", cuePoint.Number.ToString());
                                        writer.AddAttribute("Red", "40");
                                        writer.AddAttribute("Green", "226");
                                        writer.AddAttribute("Blue", "20");
                                        writer.WriteEndElement();
                                    }
                                }
                            }

                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            writer.WriteEndElement();
                        }
                    }
                    else if (reader.Name == "POSITION_MARK" && skipPositionMarks)
                    {
                        // do nothing just skipping
                    }
                    else
                    {
                        writer.WriteNode(reader, false);
                    }

                    if (hasReachedTracks)
                    {
                        insideTracks = true;
                    }
                }
            }
            var bakFile = libraryFileName + ".bak";
            if (File.Exists(bakFile))
                File.Delete(bakFile);
            File.Move(libraryFileName, bakFile);
            File.Move(tempFile, libraryFileName);
        }

        public static void AddAttribute(this XmlWriter writer, string name, string value)
        {
            writer.WriteStartAttribute(name);
            writer.WriteString(value);
            writer.WriteEndAttribute();
        }
    }
}
