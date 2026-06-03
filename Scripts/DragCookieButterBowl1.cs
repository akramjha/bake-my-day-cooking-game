
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragCookieButterBowl1 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public RectTransform bowlTransform; // Reference to the bowl's RectTransform
    public RectTransform bowl2Transform; // Reference to the second bowl's RectTransform
    public Image sugarInBowlImage; // Reference to the sugar-in-bowl image
    public GameObject[] pillars; // Reference to the pillars
    public Image ohNoImage; // Reference to the "Oh No" image
    public Image youDidItImage; // Reference to the "You did it" image
    public Button nextButton; // Reference to the Next button

    private Vector2 originalPosition; // Store the original position of the sugar
    private bool isOverBowl = false;
    private bool isOverBowl2 = false;
    private bool isCollidingWithPillar = false;
    private bool isOverObstacle = false;
    private bool success = false; // Flag to track successful drop

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        if (sugarInBowlImage != null)
        {
            sugarInBowlImage.enabled = false; // Ensure the sugar-in-bowl image is hidden initially
        }
        else
        {
            Debug.LogError("Sugar-in-Bowl Image reference is not set in the Inspector.");
        }

        if (ohNoImage != null)
        {
            ohNoImage.gameObject.SetActive(false); // Ensure the "Oh No" image is hidden initially
        }
        else
        {
            Debug.LogError("Oh No Image reference is not set in the Inspector.");
        }

        if (youDidItImage != null)
        {
            youDidItImage.gameObject.SetActive(false); // Ensure the "You did it" image is hidden initially
        }
        else
        {
            Debug.LogError("You did it Image reference is not set in the Inspector.");
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(false); // Hide the Next button initially
        }
        else
        {
            Debug.LogError("Next Button reference is not set in the Inspector.");
        }

        originalPosition = rectTransform.anchoredPosition; // Store the original position of the sugar
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f; // Optional: make the image semi-transparent while dragging
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out position);
        rectTransform.anchoredPosition = position;

        // Check for collisions with pillars
        isCollidingWithPillar = false;
        foreach (GameObject pillar in pillars)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(pillar.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera))
            {
                isCollidingWithPillar = true;
                break;
            }
        }

        if (isCollidingWithPillar)
        {
            Debug.Log("Sugar hit a pillar");
            ShowOhNoImage(); // Show the "Oh No" image
            ResetSugarPosition(); // Reset the sugar to its original position
        }

        // Check if sugar is over the bowl
        isOverBowl = RectTransformUtility.RectangleContainsScreenPoint(bowlTransform, eventData.position, canvas.worldCamera);

        // Check if sugar is over the second bowl
        isOverBowl2 = RectTransformUtility.RectangleContainsScreenPoint(bowl2Transform, eventData.position, canvas.worldCamera);
        
        // Check if sugar is over any obstacle
        isOverObstacle = IsOverObstacle(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f; // Reset the image transparency
        canvasGroup.blocksRaycasts = true;

        if (isOverBowl && !isCollidingWithPillar)
        {
            Debug.Log("Sugar successfully dragged over the bowl");
            sugarInBowlImage.enabled = true; // Show the sugar-in-bowl image
            gameObject.SetActive(false); // Hide the dragged sugar image
            ShowYouDidItImage(); // Show the "You did it" image
            success = true; // Set success flag
        }

        // Check if sugar is over the second bowl
        if (isOverBowl2 && !isCollidingWithPillar)
        {
            Debug.Log("Sugar successfully dragged over the second bowl");
            sugarInBowlImage.enabled = true; // Show the sugar-in-bowl image
            gameObject.SetActive(false); // Hide the dragged sugar image
            ShowYouDidItImage(); // Show the "You did it" image
            success = true; // Set success flag
        }

        // Check if a pillar is dropped onto an obstacle
        if (isCollidingWithPillar && isOverObstacle)
        {
            Debug.Log("Pillar dropped onto an obstacle");
            RemoveObstacle(); // Remove the obstacle
        }

        // Enable next button if success
        if (success)
        {
            nextButton.gameObject.SetActive(true); // Show the Next button
        }
    }

    private void ShowOhNoImage()
    {
        if (ohNoImage != null)
        {
            ohNoImage.gameObject.SetActive(true); // Show the "Oh No" image
            Invoke("HideOhNoImage", 3f); // Hide the "Oh No" image after 3 seconds
        }
    }

    private void HideOhNoImage()
    {
        if (ohNoImage != null)
        {
            ohNoImage.gameObject.SetActive(false); // Hide the "Oh No" image
        }
    }

    private void ResetSugarPosition()
    {
        rectTransform.anchoredPosition = originalPosition; // Reset the sugar to its original position
    }

    private void ShowYouDidItImage()
    {
        if (youDidItImage != null)
        {
            youDidItImage.gameObject.SetActive(true); // Show the "You did it" image
        }
    }

    private bool IsOverObstacle(Vector2 position)
    {
        // Implement logic to check if the sugar is over any obstacle
        return false; // Placeholder, replace with your actual logic
    }

    private void RemoveObstacle()
    {
        // Implement logic to remove the obstacle
    }
}
   
