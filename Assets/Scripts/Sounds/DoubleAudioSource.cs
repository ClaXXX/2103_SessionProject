using System;
using System.Collections;
using UnityEngine;

namespace Sounds
{
    public class DoubleAudioSource : MonoBehaviour
    {
        private AudioSource _source0;
        private AudioSource _source1;

        private bool _isFirst = true;

        private Coroutine _zerothSourceFadeRoutine;
        private Coroutine _firstSourceFadeRoutine;

        private float _volume = 0;
        public float volume
        {
            get => _volume;
            set
            {
                _volume = value;
                _source0.volume = _isFirst ? value : 0;
                _source1.volume = _isFirst ? 0 : value;
            }
        }

        private bool _loop = false;
        public bool loop
        {
            get => _loop;
            set
            {
                _loop = value;
                _source0.loop = _loop;
                _source1.loop = _loop;
            }
        }

        void Reset() {
            Update();
        }
     
     
        void Awake() {
            Update();
        }

        void Update() {
            if (_source0 == null || _source1 == null) {
                InitSources(gameObject.GetComponents<AudioSource>());
            }
        }

        void InitSources(AudioSource[] audioSources) {
            switch (audioSources.Length) {
                case 0: {
                    _source0 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                    _source1 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                } break;
                case 1: {
                        _source0 = audioSources[0];
                        _source1 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                    } break;
                default: {
                        _source0 = audioSources[0];
                        _source1 = audioSources[1];
                    } break;
            }

            _source0.loop = true;
            _source1.loop = true;
        }

        public void CrossFade( AudioClip audio,
                               float fadingTime,
                               float delayBeforeCrossFade = 0) {
            StartCoroutine(Fade(audio, fadingTime, delayBeforeCrossFade));
        }
        
        public void CrossFadeBack(float fadingTime,
            float delayBeforeCrossFade = 0) {
            StartCoroutine(FadeBack(fadingTime, delayBeforeCrossFade));
        }

        IEnumerator Fade( AudioClip audio,
                          float fadingTime,
                          float delayBeforeCrossFade = 0)
        {
            var playing = _isFirst ? _source0 : _source1;
            var toPlay = _isFirst ? _source1 : _source0;

            if (delayBeforeCrossFade > 0)
                yield return new WaitForSeconds(delayBeforeCrossFade);
            
            toPlay.loop = loop;
            toPlay.clip = audio;
            toPlay.volume = 0;
            toPlay.Play();
 
            if(_firstSourceFadeRoutine != null)
                StopCoroutine(_firstSourceFadeRoutine);
            _firstSourceFadeRoutine = StartCoroutine(fadeSource(toPlay,
                toPlay.volume,volume,fadingTime));
            
            if (_zerothSourceFadeRoutine != null) {
                StopCoroutine(_zerothSourceFadeRoutine);
            }
            _zerothSourceFadeRoutine = StartCoroutine(fadeSource(playing,
                playing.volume,0,fadingTime));
            _isFirst = !_isFirst;
        }
     
        IEnumerator FadeBack(float fadingTime,
            float delayBeforeCrossFade = 0)
        {
            var playing = _isFirst ? _source0 : _source1;
            var toPlay = _isFirst ? _source1 : _source0;

            if (delayBeforeCrossFade > 0)
                yield return new WaitForSeconds(delayBeforeCrossFade);
            
            toPlay.loop = loop;
            toPlay.volume = 0;
 
            if(_firstSourceFadeRoutine != null)
                StopCoroutine(_firstSourceFadeRoutine);
            _firstSourceFadeRoutine = StartCoroutine(fadeSource(toPlay,
                toPlay.volume,volume,fadingTime));
            
            if (_zerothSourceFadeRoutine != null) {
                StopCoroutine(_zerothSourceFadeRoutine);
            }
            _zerothSourceFadeRoutine = StartCoroutine(fadeSource(playing,
                playing.volume,0,fadingTime));
            _isFirst = !_isFirst;
        }
     
        IEnumerator fadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration)
        {
            float startTime = Time.time;
 
            while (true)
            {
                float elapsed = Time.time - startTime;
 
                sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume, endVolume, elapsed / duration));
 
                if (sourceToFade.volume == endVolume)
                {
                    break;
                }
 
                yield return null;
            }
        }

     
        //returns false if BOTH sources are not playing and there are no sounds are staged to be played.
        //also returns false if one of the sources is not yet initialized
        public bool isPlaying
        {
            get
            {
                if (!_source0 || !_source1)
                    return false;
                return _source0.isPlaying || _source1.isPlaying;
            }
        }
    }
}