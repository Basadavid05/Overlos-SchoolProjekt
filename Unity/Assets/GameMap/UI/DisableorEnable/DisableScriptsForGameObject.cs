using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScriptsForGameObject : MonoBehaviour
{
    private void Start()
    {
        // Disable all scripts attached to the GameObject
        DisableAllScripts();
    }

    public void DisableAllScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            // Skip the DisableScriptsForGameObject script itself
            if (script.GetType() == typeof(DisableScriptsForGameObject))
                continue;

            script.enabled = false;
        }
    }
}