using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services.Contracts
{
    public interface IAudioPlayerService
    {
        string Url { get; set; }
        double CurrentPosition { get; }
        double TotalDuration { get; }

        event Action OnAudioStateChanged;

        void Play();
        void Pause();
        void Stop();
        void SeekTo(TimeSpan newPosition);
    }
}
