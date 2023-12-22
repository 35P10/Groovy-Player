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
        public SortedSet<Audio> allTracks { get; } = new SortedSet<Audio>();

        public Library() { }

        public void AddTrack(Audio track)
        {
            allTracks.Add(track);
        }
    }
}
