using System;
using System.IO;
using TagLib;
using TagLib.Matroska;

namespace Core.Models
{
    public class Audio
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
                Album = tfile.Tag.Album;
                Artists = tfile.Tag.Artists.ToList();
                Genres = tfile.Tag.Genres.ToList().FirstOrDefault();
                Year = tfile.Tag.Year;
                Track = tfile.Tag.Track;
                Path = filePath;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("No se pudo cargar el audio.", ex);
            }
        }

        public string Path { get; } = "None";
        public string Title { get; } = "None";
        public TimeSpan Duration {  get; } = TimeSpan.Zero;
        public string Album { get; } = "None";
        public List<string> Artists { get; } = new List<string>();
        public string Genres { get; } = "None";
        public uint Year { get; } = uint.MaxValue;
        public uint Track { get; } = uint.MaxValue;
    }
}
