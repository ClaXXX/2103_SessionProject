using UnityEngine;
using UnityEngine.UI;

namespace Sounds
{
    public class SoundSettingsMenu : MonoBehaviour {

        private SoundManager soundManager;
        [SerializeField] private Slider ambientSoundVolumeSlider;
        [SerializeField] private Slider songsVolumeSlider;

        private void Awake() {
            soundManager = FindObjectOfType<SoundManager>();
            ambientSoundVolumeSlider.value = soundManager.ambientSoundVolume;
            songsVolumeSlider.value = soundManager.songsVolume;
        }

        public void onChangeSoundEffectsVolume() {
            soundManager.modifyAmbientSoundVolume(ambientSoundVolumeSlider.value);
        }
    
        public void onChangeSongsVolume() {
            soundManager.modifySongsVolume(songsVolumeSlider.value);
        }

    }
}
