using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerLives = 5;
    public Image[] lifeImages;
    public string endSceneName;
    public Button restartButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HideRestartButton(); // Hide the restart button when the game starts
    }

    public void ReduceLife()
    {
        playerLives--;
        Debug.Log("Life reduced. Remaining lives: " + playerLives);
        UpdateLivesUI();
        if (playerLives <= 0)
        {
            ShowRestartButton(); // Call ShowRestartButton() when player's life reaches 0
        }
    }

    public void UpdateLivesUI()
    {
        if (lifeImages == null || lifeImages.Length == 0)
        {
            Debug.LogWarning("Life images not assigned or found!");
            return;
        }

        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (lifeImages[i] != null)
            {
                // Disable the heart image if the corresponding life is depleted
                lifeImages[i].enabled = i < playerLives;
            }
        }
    }

    public void ShowRestartButton()
    {
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true); // Activate the restart button
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("Restart button shown.");
        }
        else
        {
            Debug.LogWarning("Restart button not found!");
        }
    }

    public void HideRestartButton()
    {
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false); // Hide the restart button
            Debug.Log("Restart button hidden.");
        }
        else
        {
            Debug.LogWarning("Restart button not found!");
        }
    }

    public void RestartGame()
    {
        playerLives = 5;
        SceneManager.LoadScene("1 STEP"); // Change "1 STEP" to your actual starting scene
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign UI elements in the new scene
        lifeImages = GameObject.FindGameObjectsWithTag("HeartImage")
                            .Select(go => go.GetComponent<Image>())
                            .ToArray();
        restartButton = GameObject.FindGameObjectWithTag("RestartButton")?.GetComponent<Button>();

        UpdateLivesUI();
        HideRestartButton(); // Hide the restart button when a new scene is loaded
    }
}