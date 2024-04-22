using TMPro;
using UnityEngine;

public class ItemTextMover : MonoBehaviour
{
    private float scrollSpeed = 1.4f;
    private TextMeshProUGUI textComponent;
    private float scrollDistance = 2f;
    private RectTransform rectTransform;
    private bool moveRight = false;
    private Vector2 originalPosition;
    private float siblingWidth;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent= GetComponent<TextMeshProUGUI>();
        originalPosition = rectTransform.anchoredPosition;
        // Set the text component to not break
        textComponent.enableWordWrapping = false;
        textComponent.overflowMode = TextOverflowModes.Overflow;

        // Get the width of the sibling container
        RectTransform siblingRectTransform = transform.parent.Find("Background").GetComponent<RectTransform>();
        siblingWidth = siblingRectTransform.rect.width;
    }

    void Update()
    {
        // Only scroll if the text length exceeds a certain threshold
        if (textComponent.text.Length > 6)
        {
            // Move the text horizontally
            if (!moveRight)
            {
                rectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
            }
            else
            {
                rectTransform.anchoredPosition += Vector2.right * scrollSpeed * Time.deltaTime;
            }

            // Check if the text has moved the desired distance to the left
            if (!moveRight && (rectTransform.anchoredPosition.x - originalPosition.x) < -scrollDistance)
            {
                moveRight = true;
            }
            // Check if the text has moved the desired distance to the right
            else if (moveRight && (rectTransform.anchoredPosition.x - originalPosition.x) > scrollDistance)
            {
                moveRight = false;
            }
            /*
            // Clamp the text position within the sibling's bounds
            float textWidth = textComponent.preferredWidth;
            float minX = -siblingWidth / 2 + textWidth / 2;
            float maxX = siblingWidth / 2 - textWidth / 2;
            Vector2 clampedPosition = rectTransform.anchoredPosition;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            rectTransform.anchoredPosition = clampedPosition;
            */
        }
    }
}
