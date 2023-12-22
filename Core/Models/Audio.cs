using System;
using System.IO;
using TagLib;
using TagLib.Flac;
using TagLib.Matroska;

namespace Core.Models
{
    public class AudioMinRepresentation : IComparable<AudioMinRepresentation>
    {
        public string Title { get; } = "Unknown";
        public int Id { set; get; } = 0;

        public AudioMinRepresentation (string title, int id)
        {
            Title = title;
            Id = id;
        }

        public int CompareTo(AudioMinRepresentation other)
        {
            return string.Compare(this.Title, other.Title, StringComparison.Ordinal);
        }
    }
        public class Audio : IComparable<Audio>
    {
        public Audio()
        {
            ;
        }

        public Audio(string filePath)
        {
            try
            {
                var tfile = TagLib.File.Create(filePath);
                Title = tfile.Tag.Title;
                Duration = tfile.Properties.Duration;
                if(tfile.Tag.Album != null)
                    Album = tfile.Tag.Album;
                if(tfile.Tag.Performers.Count() > 0)
                    Artists = tfile.Tag.Performers.ToList();
                Genres = tfile.Tag.Genres.ToList().FirstOrDefault();
                Year = tfile.Tag.Year;
                Track = tfile.Tag.Track;
                IPicture Picture = tfile.Tag.Pictures.ToList().FirstOrDefault();
                if (Picture != null)
                {
                    string base64Image = Convert.ToBase64String(Picture.Data.Data);
                    Imagen =  $"data:{Picture.MimeType};base64,{base64Image}";
                }
                Path = filePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo cargar el audio.", ex);
            }
        }

        public string Path { get; } = "None";
        public string Title { get; } = "Unknown";
        public TimeSpan Duration {  get; } = TimeSpan.Zero;
        public string Album { get; } = "Unknown";
        public List<string> Artists { get; } = new List<string> { "Unknown" };
        public string Genres { get; } = "Unknown";
        public uint Year { get; } = uint.MinValue;
        public uint Track { get; } = uint.MinValue;
        public string Imagen { get; } = "Unknown";
        public int Id { set; get; } = 0;

        public int CompareTo(Audio other)
        {
            return string.Compare(this.Title, other.Title, StringComparison.Ordinal);
        }
    }
}
