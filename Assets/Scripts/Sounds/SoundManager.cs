﻿using System;
using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace Sounds {
    public class SoundManager : MonoBehaviour {
        // TODO : Tous les sons et musiques vont ici
        public GameObject hitSound;
        public GameObject ballInHoleSound;

        private List<GameObject> ambientSounds = new List<GameObject>();
        private List<GameObject> songs = new List<GameObject>();

        public float ambientSoundVolume = 1f;
        public float songsVolume = 0.2f;

        public void Start() {
            DontDestroyOnLoad(this.gameObject); // TODO : Vérifier que ça marche
            ambientSounds.Add(hitSound);
            ambientSounds.Add(ballInHoleSound);
        }

        public void playHitSound(Transform source) {
            AudioSource audioClip = hitSound.GetComponent<AudioSource>();
            audioClip.volume = ambientSoundVolume;
//            Debug.Log(audioClip.volume);
            GameObject go = Instantiate(hitSound, source);
            Destroy(go, 1f);
        }

        public void playBallInHoleSound(Transform source) {
            AudioSource audioClip = ballInHoleSound.GetComponent<AudioSource>();
            audioClip.volume = ambientSoundVolume;
            GameObject go = Instantiate(ballInHoleSound, source);
            Destroy(go, 1.4f);
        }

        public void modifyAmbientSoundVolume(float newValue) {
            ambientSoundVolume = newValue;
        }

        public void modifySongsVolume(float newValue) {
            songsVolume = newValue;
        }
    }
}