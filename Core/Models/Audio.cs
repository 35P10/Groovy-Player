using System;
using System.IO;
using TagLib;
using TagLib.Matroska;

namespace Core.Models
{
    public class Audio
    {
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

        public string Path { get; }
        public string Title { get; }
        public TimeSpan Duration {  get; }
        public string Album { get; }
        public List<string> Artists { get; }
        public string Genres { get; }
        public uint Year { get; }
        public uint Track { get; }
    }
}
