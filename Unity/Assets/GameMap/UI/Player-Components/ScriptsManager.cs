using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    [Header("Gameobjects")]
    public Pointers UIPointer;
    private GameObject Pointer;

    [Header("Necessary Things")]
    private Rigidbody rb;

    private void Start()
    {
        Pointer=UIPointer.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (PauseMenu.GameIsPause || InventoryController.InventoryOpen || MapControl.MapIsEnabled || SoulShop.SoulShopActive || PlayerDatas.Death)
        {
            Pointer.gameObject.SetActive(false);
            rb.drag = 2f;
            rb.AddForce(Vector3.down * 2f);
            MonoBehaviour[] scripts = transform.GetComponents<MonoBehaviour>();
            Disable(scripts);
            
        }
        else if(!PauseMenu.GameIsPause && !InventoryController.InventoryOpen && !MapControl.MapIsEnabled && !SoulShop.SoulShopActive && !PlayerDatas.Death)
        {
                MonoBehaviour[] scripts = transform.GetComponents<MonoBehaviour>();

            Enable(scripts);
            Pointer.gameObject.SetActive(true);
        }
    }

    public static void Disable(MonoBehaviour[] scripts)
    {
        // Disable each script
        foreach (var script in scripts)
        {
            // Skip the DisableScriptsForGameObject script itself
            if (script.GetType() == typeof(ScriptsManager))
                continue;
            if (script.GetType() == typeof(MoveControl))
                continue;
            if (script.GetType() == typeof(PlayerDatas))
                continue;
            script.enabled = false;
        }
    }

    public static void Enable(MonoBehaviour[] scripts)
    {
        // Enable each script
        foreach (var script in scripts)
        {
            // Skip the EnableScriptsForGameObject script itself
            if (script.GetType() == typeof(ScriptsManager))
                continue;
            script.enabled = true;
        }
    }
}
