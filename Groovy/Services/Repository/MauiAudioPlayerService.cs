using Groovy.Services.Contracts;
using Plugin.Maui.Audio;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using System.ComponentModel;

namespace Groovy.Services.Repository
{
    public class MauiAudioPlayerService : IAudioPlayerService
    {
        private IAudioManager _audioManager;
        private IAudioPlayer _audioPlayer;
        private TimeSpan _animationProgress;
        private Timer _updateTimer;
        private readonly IDispatcher _dispatcher;
        public bool IsPlaying {
            get {
                return _audioPlayer.IsPlaying;
            }
        }
        public Audio Audio {
            get
            {
                return _audio;
            }
        }

        private Audio _audio;

        public void ChangeTrack(Audio newTrack)
        {
            Stop();
            _audio = newTrack;
            _audioManager = new AudioManager();
            FileStream fileStream = new FileStream(_audio.Path, FileMode.Open, FileAccess.Read);
            _audioPlayer = _audioManager.CreatePlayer(fileStream);

            UpdateAudioPosition();
        }

        public event Action OnAudioStateChanged;

        public double TotalDuration => _audioPlayer?.Duration ?? 1;

        public double CurrentPosition
        {
            get => _audioPlayer?.CurrentPosition ?? 0;
            set
            {
                SeekTo(value);
            }
        }

        public MauiAudioPlayerService(IDispatcher dispatcher)
        {
            _audio = new Audio();
            _dispatcher = dispatcher;
            MauiAudioPlayerServiceAsync();
        }

        private async Task MauiAudioPlayerServiceAsync()
        {
            try
            {
                await Task.Run(async () =>
                {
                    _audioManager = new AudioManager();
                    _audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("track1.flac"));
                    UpdateAudioPosition();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Archivo no encontrado: {ex}");
            }
        }

        private void UpdateAudioPosition()
        {
            _updateTimer = new Timer(UpdateAudioPositionCallback, null, 0, 100);
        }

        private void UpdateAudioPositionCallback(object state)
        {
            if (_audioPlayer?.IsPlaying == true)
            {
                _dispatcher.Dispatch(() => OnAudioStateChanged?.Invoke());
            }
        }

        public void Play()
        {
            _audioPlayer.Play();
        }

        public void Pause()
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Pause();
            }
            else
            {
                _audioPlayer.Play();
            }
        }

        public void Stop()
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Stop();
            }
        }

        public void SeekTo(double newPosition)
        {
            if (_audioPlayer is not null &&
                _audioPlayer.CanSeek)
            {
                _audioPlayer.Seek(newPosition);
                OnAudioStateChanged?.Invoke();
            }
        }

        public void Dispose()
        {
            _updateTimer?.Dispose();
        }
    }
}
