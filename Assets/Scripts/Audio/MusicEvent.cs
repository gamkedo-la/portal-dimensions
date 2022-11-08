using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    public enum LayerType
    {
        Additive,
        Single
    }

    [CreateAssetMenu(menuName = "SoundSystem/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        [SerializeField] AudioClip[] musicLayers;
        [SerializeField] LayerType layerType = LayerType.Additive;
        [SerializeField] AudioMixerGroup[] mixer;

        public AudioClip[] MusicLayers => musicLayers;
        public LayerType LayerType => layerType;
        public AudioMixerGroup Mixer => Mixer;

        public void Play(float fadeTime)
        {
            MusicManager.Instance.PlayMusic(this, fadeTime);
        }
    }
}

