using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource buttonSoundEffect;
    private bool isSoundEffectMuted;

    private void Start()
    {
        isSoundEffectMuted = PlayerPrefs.GetInt("SoundEffectMuted", 0) == 1;
        buttonSoundEffect.mute = isSoundEffectMuted;
    }

    public void ToggleSoundEffect()
    {
        isSoundEffectMuted = !isSoundEffectMuted;
        buttonSoundEffect.mute = isSoundEffectMuted;
        PlayerPrefs.SetInt("SoundEffectMuted", isSoundEffectMuted ? 1 : 0);
    }
}
