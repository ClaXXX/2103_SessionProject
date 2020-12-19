﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


namespace Sounds
{
    public class MusicManager : MonoBehaviour
    {
        private SoundManager _soundManager;
        private AudioClip _originalMusic;

        [Serializable]
        public struct NamedMusicClip {
            public string name;
            public AudioClip audio;
        }
        
        [Header("Musics")]
        public List<AudioClip> musics;
        public List<NamedMusicClip> specialMusics;
        public List<AmbiantManager.NamedAudioClip> placedAudioClips;

        [Header("Sources")]
        public DoubleAudioSource source; // MainCamera most of the time
        public float fadingTime = 1f;
        
        void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            System.Random rand = new System.Random();

            _originalMusic = musics.ElementAt(rand.Next(musics.Count)); // Get a random music
            source.loop = true; // Init a loop system
            ActualiseForVolumeChange();
            source.CrossFade(_originalMusic, fadingTime);
            
            foreach (AmbiantManager.NamedAudioClip placedAudioClip in placedAudioClips)
            {
                placedAudioClip.source.loop = true; // Init a loop system
                placedAudioClip.source.clip = placedAudioClip.audio;
                placedAudioClip.source.Play();
            }
        }

        public void ActualiseForVolumeChange()
        {
            source.volume = _soundManager.songsVolume;
            foreach (AmbiantManager.NamedAudioClip placedAudioClip in placedAudioClips)
            {
                placedAudioClip.source.volume = _soundManager.songsVolume;
            }
        }

        public void ChangeMusic(string name)
        {
            source.CrossFade(specialMusics.Find(x => x.name == name).audio,
                fadingTime);
        }

        public void PlayBack()
        {
            source.CrossFadeBack(fadingTime);
        }

        public void ResetMusic()
        {
            source.CrossFade(_originalMusic,
                fadingTime);
        }
    }
}
