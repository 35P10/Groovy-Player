using Groovy.Services.Contracts;
using NAudio.Wave;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services.Repository
{
    public class MauiAudioPlayerService : IAudioPlayerService
    {
        public IAudioManager _audioManager;
        private IAudioPlayer audioPlayer;
        bool isPositionChangeSystemDriven;
        public event Action OnAudioStateChanged;

        public string Url
        {
            get { return Url; }
            set
            {
                audioPlayer = _audioManager.CreatePlayer(value);
            }
        }

        public double TotalDuration => audioPlayer?.Duration ?? 1;

        public double CurrentPosition
        {
            get => audioPlayer?.CurrentPosition ?? 0;
            set
            {
                if (audioPlayer is not null &&
                    audioPlayer.CanSeek &&
                    isPositionChangeSystemDriven is false)
                {
                    audioPlayer.Seek(value);
                }
            }
        }

        public MauiAudioPlayerService()
        {
            MauiAudioPlayerServiceAsync();
        }

        private async void MauiAudioPlayerServiceAsync()
        {
            _audioManager = new AudioManager();
            audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("track1.flac"));
        }

        public void Play()
        {
            audioPlayer.Play();
        }
        public void Pause()
        {
            if (audioPlayer.IsPlaying)
            {
                audioPlayer.Pause();
            }
            else
            {
                audioPlayer.Play();
            }
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void SeekTo(TimeSpan newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
