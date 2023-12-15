using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Groovy.Services
{
    public class AudioPlayer
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;

        public string Url
        {
            get { return audioFile?.FileName; }
            set
            {
                Stop();
                audioFile = new AudioFileReader(value);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);
            }
        }

        public TimeSpan CurrentPosition
        {
            get { return audioFile?.CurrentTime ?? TimeSpan.Zero; }
        }

        public TimeSpan TotalDuration
        {
            get { return audioFile?.TotalTime ?? TimeSpan.Zero; }
        }

        public event Action OnAudioStateChanged;

        private void NotifyAudioStateChanged()
        {
            OnAudioStateChanged?.Invoke();
        }

        public void Play()
        {
            if (waveOut != null && audioFile != null)
            {
                waveOut.Play();
                NotifyAudioStateChanged();
            }
        }

        public void Pause()
        {
            if (waveOut != null)
            {
                waveOut.Pause();
                NotifyAudioStateChanged();
            }
        }

        public void Stop()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                NotifyAudioStateChanged();
            }
        }

        public void SeekTo(TimeSpan newPosition)
        {
            if (audioFile != null)
            {
                audioFile.CurrentTime = newPosition;
                NotifyAudioStateChanged();
            }
        }
    }
}
