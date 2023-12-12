using System;

namespace Groovy.Services
{
    public class AudioPlayerService
    {
        private string _audioUrl;

        public event Action<string> OnAudioUrlChanged;

        public string AudioUrl
        {
            get { return _audioUrl; }
            set
            {
                if (_audioUrl != value)
                {
                    _audioUrl = value;
                    OnAudioUrlChanged?.Invoke(value);
                }
            }
        }
    }
}
