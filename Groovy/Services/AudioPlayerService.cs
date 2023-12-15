using System;
using Groovy.Services;

namespace Groovy.Services
{
    public class AudioPlayerService
    {
        private AudioPlayer audioPlayer;

        public event Action<string> OnAudioUrlChanged;
        public event Action OnAudioStateChanged;

        public AudioPlayerService()
        {
            audioPlayer = new AudioPlayer();
            audioPlayer.OnAudioStateChanged += () => OnAudioStateChanged?.Invoke();

            SetDefaultAudioUrl();
        }

        private void SetDefaultAudioUrl()
        {
            string defaultAudioUrl = "D:\\Projects\\Groovy\\Groovy\\Resources\\Tracks\\track1.flac";
            string absolutePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, defaultAudioUrl);
            AudioUrl = absolutePath;
        }

        public string AudioUrl
        {
            get { return audioPlayer?.Url; }
            set
            {
                if (audioPlayer != null)
                {
                    audioPlayer.Stop();
                    audioPlayer = null;
                }

                audioPlayer = new AudioPlayer();
                audioPlayer.OnAudioStateChanged += () => OnAudioStateChanged?.Invoke();

                audioPlayer.Url = value;
                OnAudioUrlChanged?.Invoke(value);
            }
        }

        public TimeSpan CurrentPosition
        {
            get { return audioPlayer?.CurrentPosition ?? TimeSpan.Zero; }
        }

        public TimeSpan TotalDuration
        {
            get { return audioPlayer?.TotalDuration ?? TimeSpan.Zero; }
        }

        public void Play()
        {
            audioPlayer?.Play();
        }

        public void Pause()
        {
            audioPlayer?.Pause();
        }

        public void Stop()
        {
            audioPlayer?.Stop();
        }

        public void SeekTo(TimeSpan newPosition)
        {
            audioPlayer?.SeekTo(newPosition);
        }
    }
}
