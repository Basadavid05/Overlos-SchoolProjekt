using TMPro;
using UnityEngine;

public class Fps : MonoBehaviour
{
    private int avgFrameRate;
    private TextMeshProUGUI fps_display_Text;
    private bool active=false;
    private KeyCode key;

    private void Start()
    {

        fps_display_Text =transform.Find("FPS").GetComponent<TextMeshProUGUI>();
        fps_display_Text.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Main.main.ChangedHotkeys)
        {
            key = Main.main.keyMappings[Main.Hotkeys.ViewFps];
        }

        if (Input.GetKeyDown(key))
        {
            active=!active;
            fps_display_Text.gameObject.SetActive(active);
        }

        if (active)
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            fps_display_Text.text = avgFrameRate.ToString() + " FPS";
        }

    }
}
