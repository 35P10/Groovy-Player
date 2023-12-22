using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class Library
    {
        public SortedDictionary<int, Audio> AllTracks { get; } = new SortedDictionary<int, Audio>();
        public SortedSet<AudioMinRepresentation> SortedTracks = new SortedSet<AudioMinRepresentation>();

        public Library() { }

        public void AddTrack(Audio track)
        {
            var temp = new AudioMinRepresentation(track.Title, AllTracks.Count + 1);
            track.Id = temp.Id;
            SortedTracks.Add(temp);
            AllTracks.Add(temp.Id, track);
        }
    }
}
