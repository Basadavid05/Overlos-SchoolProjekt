using TMPro;
using UnityEngine;
using System.Collections;

public class TextMove : MonoBehaviour
{
    private TextMeshProUGUI text;
    public float moveSpeed = 1f;
    public float threshold = 7; 
    private bool isMoving = false;
    private bool moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        text=gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (text.text.Length > threshold && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveTextCoroutine());
        }
    }

    IEnumerator MoveTextCoroutine()
    {
        while (true)
        {
            Vector3 currentPosition = text.rectTransform.localPosition;

            // Move the text to the right if moveRight is true, otherwise move left
            currentPosition.x += moveRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
            text.rectTransform.localPosition = currentPosition;

            // Check if the text has reached the right or left edge
            if (currentPosition.x >= text.rectTransform.rect.width / 2 || currentPosition.x <= -text.rectTransform.rect.width / 2)
            {
                // Reverse the direction of movement
                moveRight = !moveRight;
            }

            yield return null;
        }
    }
}
