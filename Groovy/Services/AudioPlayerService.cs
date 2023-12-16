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

            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await SetDefaultAudioUrlAsync();
        }

        private async Task SetDefaultAudioUrlAsync()
        {
            // string filename = "track1.flac";
            // Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);

            // string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filename);

            // await Task.Run(async () =>
            // {
            //     using (FileStream outputStream = File.Create(targetFile))
            //     {
            //         await inputStream.CopyToAsync(outputStream);
            //     }
            // });

            //AudioUrl = targetFile;
            AudioUrl = "https://samplelib.com/lib/preview/mp3/sample-12s.mp3";
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
