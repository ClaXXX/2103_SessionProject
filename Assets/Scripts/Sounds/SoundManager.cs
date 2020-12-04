using System;
using UnityEngine;

namespace DefaultNamespace.Sounds {
    public class SoundManager : MonoBehaviour {
        // TODO : Tous les sons et musiques vont ici
        public GameObject hitSound;
        public GameObject ballInHoleSound;

        public void playHitSound(Transform source) {
            GameObject go = Instantiate(hitSound, source);
            Destroy(go, 1f);
        }

        public void playBallInHoleSound(Transform source) {
            GameObject go = Instantiate(ballInHoleSound, source);
            Destroy(go, 1.4f);
        }
    }
}