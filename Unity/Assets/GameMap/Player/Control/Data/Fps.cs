using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fps : MonoBehaviour
{
    private int avgFrameRate;
    private TextMeshProUGUI fps_display_Text;
    private bool active=false;

    public enum FpsTypes
    {
        low=64, normal=94, high=124, higher=148,ultra=168, extra=184, ultrafast=244
    }

    public FpsTypes FpsTarget;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        FpsTarget = FpsTypes.ultra;
        Application.targetFrameRate = (int)FpsTarget;
        fps_display_Text =transform.Find("FPS").GetComponent<TextMeshProUGUI>();
        fps_display_Text.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            active=!active;
            fps_display_Text.gameObject.SetActive(active);
        }

        if (Application.targetFrameRate != (int)FpsTarget)
            Application.targetFrameRate = (int)FpsTarget;

        if (active)
        {
            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            fps_display_Text.text = avgFrameRate.ToString() + " FPS";
        }

    }
}
