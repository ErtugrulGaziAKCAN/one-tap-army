using Obvious.Soap;
using QuickTools.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
namespace QuickTools.Scripts.Audio
{
    public class AudioManager : QuickSingletonPersistent<AudioManager>
    {
//-------Public Variables-------//


//------Serialized Fields-------//
        [SerializeField] private int AudioSourcesPoolSize = 8;
        [FormerlySerializedAs("IsMute")] [SerializeField] private BoolReference IsOn;

//------Private Variables-------//
        private AudioSource[] _audioSources;


#region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            CreateSources();
        }

#endregion


#region PUBLIC_METHODS

        public static void PlayClip(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            if (clip == null)
            {
                Debug.LogError("Audio clip is null");
                return;
            }

            var source = Instance.GetAvailableSource();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            if (loop)
            {
                source.Play();
                return;
            }
            source.PlayOneShot(clip);
        }

        public void UpdateMute()
        {
            foreach (var source in _audioSources)
            {
                source.mute = !IsOn.Value;
            }
        }

        public void StopAudio(AudioClip clip)
        {
            foreach (var audioSource in _audioSources)
            {
                if (audioSource.clip != clip)
                    continue;
                audioSource.Stop();
            }
        }

#endregion


#region PRIVATE_METHODS

        private AudioSource GetAvailableSource()
        {
            foreach (var a in _audioSources)
            {
                if (!a.isPlaying && !a.loop)
                {
                    return a;
                }
            }

            return _audioSources[1];
        }

        private void CreateSources()
        {
            _audioSources = new AudioSource[AudioSourcesPoolSize];
            for (var i = 0; i < AudioSourcesPoolSize; i++)
            {
                _audioSources[i] = gameObject.AddComponent<AudioSource>();
            }
        }

#endregion
    }
}