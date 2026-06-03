using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;

    public int Duration;
    public string nextSceneName;
    public Image failedImage;
    public Button nextButton;
    public Image wellDoneImage; // Reference to the "well done" image

    private int remainingDuration;
    private Coroutine timerCoroutine; // Reference to the coroutine

    private void Start()
    {
        Begin(Duration);
    }

    private void Begin(int seconds)
    {
        remainingDuration = seconds;
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            if (wellDoneImage != null && wellDoneImage.gameObject.activeSelf)
            {
                StopTimer();
                Debug.Log("Timer stopped because 'well done' image is active");
                yield break;
            }

            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("Timer has ended");

        // Manage game state
        GameManager.Instance.ReduceLife();

        if (GameManager.Instance.playerLives > 0)
        {
            if (nextButton != null)
            {
                nextButton.gameObject.SetActive(true);
                nextButton.onClick.AddListener(LoadNextScene);
            }
            else
            {
                Debug.LogWarning("Next button not found!");
            }
        }
        else
        {
            GameManager.Instance.ShowRestartButton();
            Debug.Log("Restart button should appear now.");
        }

        if (failedImage != null)
        {
            failedImage.gameObject.SetActive(true);
        }
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
