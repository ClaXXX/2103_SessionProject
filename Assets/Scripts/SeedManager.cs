using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class SeedManager : MonoBehaviour{
        public static SeedManager instance;

        private String seed;
        private InputsFactory inputsFactory;


        void Awake() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(instance);
            }
        }

        public void setSeed(String seed) {
            this.seed = seed;
        }

        public String getSeed() {
            return seed;
        }
    }


}