using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton:Game

    public static Game Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] Text[] allCoinsUIText;
    [SerializeField] GameObject nextSceneButton; // Reference to the button
    [SerializeField] Button resetButton; // Reference to the reset button

    public int Coins = 10; // Public access for Coins variable

    void Start()
    {
        UpdateAllCoinsUIText();
        CheckCoins(); // Initial check for coins
        
        // Add listener to the reset button
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetCoins);
        }
        else
        {
            Debug.LogError("ResetButton is not assigned!");
        }
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
        UpdateAllCoinsUIText(); // Ensure UI is updated after using coins
        CheckCoins(); // Check coins after usage
    }

    public bool HasEnoughCoins(int amount)
    {
        return Coins >= amount; // Ensure the return type is bool
    }

    void UpdateAllCoinsUIText()
    {
        foreach (Text coinText in allCoinsUIText)
        {
            coinText.text = Coins.ToString();
        }
    }

    void CheckCoins()
    {
        if (Coins <= 0)
        {
            if (nextSceneButton != null)
            {
                nextSceneButton.SetActive(true); // Show the button
                Button buttonComponent = nextSceneButton.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.onClick.AddListener(OnNextSceneButtonClicked); // Add click listener
                }
                else
                {
                    Debug.LogError("Button component is missing on nextSceneButton!");
                }
            }
            else
            {
                Debug.LogError("NextSceneButton is not assigned!");
            }
        }
    }

    void OnNextSceneButtonClicked()
    {
        // Load the next scene here. Ensure the scene is added to your Build Settings.
        Coins = 10; // Reset coins when loading the next scene
        SceneManager.LoadScene("BackHomeFromSupermarket"); // Replace with the actual scene name
    }

    void ResetCoins()
    {
        Coins = 25; // Reset coins to initial value
        UpdateAllCoinsUIText(); // Update UI to reflect reset coins
    }
}
