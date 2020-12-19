﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class AmbiantManager : MonoBehaviour
    {
        private SoundManager _soundManager;
        [Serializable]
        public struct NamedAudioClip {
            public string name;
            public AudioClip audio;
            public AudioSource source;
        }
        public List<NamedAudioClip> sfx;

        void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
        }

        public void Play(string name)
        {

            NamedAudioClip audioClip = sfx.Find(x => x.name == name);
            
            audioClip.source.PlayOneShot(audioClip.audio, _soundManager.ambientSoundVolume);
        }

        public void ActualiseForVolumeChange()
        {
            foreach (NamedAudioClip clip in sfx)
            {
                clip.source.volume = _soundManager.ambientSoundVolume;
            }
        }
    }
}
