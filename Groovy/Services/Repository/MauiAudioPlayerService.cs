using Groovy.Services.Contracts;
using Plugin.Maui.Audio;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services.Repository
{
    public class MauiAudioPlayerService : IAudioPlayerService
    {
        private IAudioManager _audioManager;
        private IAudioPlayer _audioPlayer;
        private TimeSpan _animationProgress;
        private Timer _updateTimer;
        private readonly IDispatcher _dispatcher;
        public bool IsPlaying { get; set; }

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
            IsPlaying = true;
        }

        public void Pause()
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Pause();
                IsPlaying = false;
            }
            else
            {
                _audioPlayer.Play();
                IsPlaying = true;
            }
        }

        public void Stop()
        {
            if (_audioPlayer.IsPlaying)
            {
                _audioPlayer.Stop();
                IsPlaying = false;
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
