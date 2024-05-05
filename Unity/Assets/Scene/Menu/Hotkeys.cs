using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotkeys : MonoBehaviour
{
    private Main Main;

    public Main.Hotkeys selectedHotkey;

    private Button button;
    private TextMeshProUGUI text;

    [Header("Keyboard")]
    private bool added;
    private bool activated;
    private int keycodecount;
    private string keyboardstring;

    private void Start()
    {
        Main = GameObject.Find("Main").GetComponent<Main>();
        button = transform.GetChild(1).GetComponent<Button>();
        text=button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        keycodecount = (int)selectedHotkey;
        if (Main != null && Main.keyMappings != null)
        {
            for (int i = 0; i < Main.keyMappings.Count; i++)
            {
                var key = (Main.Hotkeys)i;
                //Debug.Log(key.ToString() + ": " + Main.keyMappings[key].ToString());
                if (i == keycodecount)
                {
                    text.text = Main.keyMappings[key].ToString();
                    keyboardstring = text.text;
                    break; // Exit the loop once the desired index is found
                }
            }
        }

        if (button != null)
        {
            added=true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void Update()
    {
        if (!added)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }

        if (activated)
        {
            button.OnSelect(null);
            text.text = "...";
            if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.Mouse1))
            {
                text.text = keyboardstring;
                DeselectCurrentButton();
            }
            else if (Input.anyKeyDown)
            {
                // Get the key that was pressed
                KeyCode keyPressed = GetPressedKey();

                // Assign the pressed key to the hotkey
                if (keyPressed != KeyCode.None && NotAllowedButtons(keyPressed) && keyPressed != KeyCode.Escape)
                {
                    AssignKeyToHotkey(keyPressed);
                }
            }
        }
    }

    private void DeselectCurrentButton()
    {
        button.enabled = false;
        button.enabled = true;
        Main.Lock(true);
        activated = false;
        button.OnDeselect(null);
        button.onClick.RemoveAllListeners();
        added = false;
    }

    private void OnButtonClick()
    {
        Main.Lock(false);
        activated = true;
    }

    private KeyCode GetPressedKey()
    {
        foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(keyCode))
            {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    private void AssignKeyToHotkey(KeyCode newKey)
    {
        // Update the assigned key for the hotkey
        Main.keyMappings[selectedHotkey] = newKey;
        text.text = newKey.ToString();
        DeselectCurrentButton();
    }

    private bool NotAllowedButtons(KeyCode keyCode)
    {
        foreach (var keyMapping in Main.keyMappings)
        {
            if (keyMapping.Value == keyCode)
            {
                return false;
            }
        }

        bool isMovementKey = keyCode == KeyCode.W || keyCode == KeyCode.A || keyCode == KeyCode.S || keyCode == KeyCode.D;
        bool isMouseButton = keyCode == KeyCode.Mouse0 || keyCode == KeyCode.Mouse1 || keyCode == KeyCode.Mouse2;

        return (!isMouseButton && !isMovementKey);

    }


}
