using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScriptsForGameObject : MonoBehaviour
{
    private void Start()
    {
        // Enable all scripts attached to the GameObject
        EnableAllScripts();
    }

    public void EnableAllScripts()
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour script in scripts)
        {
            // Skip the EnableScriptsForGameObject script itself
            if (script.GetType() == typeof(EnableScriptsForGameObject))
                continue;

            script.enabled = true;
        }
    }
}