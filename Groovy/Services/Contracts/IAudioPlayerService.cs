using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services.Contracts
{
    public interface IAudioPlayerService
    {
        double CurrentPosition { get; }
        double TotalDuration { get; }
        bool IsPlaying { get; }
        Audio Audio {  get; }

        void ChangeTrack(Audio newTrack);

        event Action OnAudioStateChanged;
        event Action OnAudioChanged;

        void Play();
        void Pause();
        void Stop();
        void SeekTo(double newPosition);
    }
}
