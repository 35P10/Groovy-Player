﻿using System;
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

        event Action OnAudioStateChanged;

        void Play();
        void Pause();
        void Stop();
        void SeekTo(double newPosition);
    }
}