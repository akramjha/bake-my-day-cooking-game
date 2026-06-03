using UnityEngine;
using UnityEngine.UI;

public class MusicButtonManager : MonoBehaviour
{
    public Button musicToggleButton;
    public Button musicOffButton;

    void Start()
    {
        // Find the BackgroundMusicManager in the scene
        BackgroundMusicManager backgroundMusicManager = FindObjectOfType<BackgroundMusicManager>();

        if (backgroundMusicManager != null && musicToggleButton != null && musicOffButton != null)
        {
            musicToggleButton.onClick.AddListener(ToggleButton);
            musicOffButton.onClick.AddListener(ToggleButton);
            UpdateButtons(backgroundMusicManager.IsMusicMuted());
        }
        else
        {
            Debug.LogWarning("BackgroundMusicManager or Button is missing.");
        }
    }

    void UpdateButtons(bool isMusicMuted)
    {
        if (isMusicMuted)
        {
            musicToggleButton.gameObject.SetActive(true);
            musicOffButton.gameObject.SetActive(false);
        }
        else
        {
            musicToggleButton.gameObject.SetActive(false);
            musicOffButton.gameObject.SetActive(true);
        }
    }

    void ToggleButton()
    {
        BackgroundMusicManager backgroundMusicManager = FindObjectOfType<BackgroundMusicManager>();

        if (backgroundMusicManager != null)
        {
            backgroundMusicManager.ToggleMusic();
            UpdateButtons(backgroundMusicManager.IsMusicMuted());
        }
    }
}
