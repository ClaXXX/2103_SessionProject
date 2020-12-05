using DefaultNamespace.Sounds;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsMenu : MonoBehaviour {

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Slider ambientSoundVolumeSlider;
    [SerializeField] private Slider songsVolumeSlider;
    
    public void onChangeSoundEffectsVolume() {
        soundManager.modifyAmbientSoundVolume(ambientSoundVolumeSlider.value);
    }
    
    public void onChangeSongsVolume() {
        soundManager.modifySongsVolume(songsVolumeSlider.value);
    }

}
