using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapControl : MonoBehaviour
{

    [Header("Public-s")]
    public GameObject player;

    [Header("Functions")]
    public float treshold;

    [Header("Spawn")]
    public Transform spawnmanager;
    public LayerMask GroundLayermask;

    [Header("SpawningPoint")]
    public List<Transform> SpawnLocations = new List<Transform>();

    [Header("Map-components")]
    public GameObject maps;
    public static bool MapIsEnabled=false;

    [Header("Map+")]
    public static Transform PickUpMap;

    public Rigidbody rb;
    private int spawncount;

    // Start is called before the first frame update
    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        mapactive();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Treshold();
        if(!PauseMenu.GameIsPause && !InventoryController.InventoryOpen)
        {
            if (MapIsEnabled)
            {
                mapactive();
            }
        }
        
    }

    public void mapactive()
    {
        maps.SetActive(true);
        MapIsEnabled = true;
        player.transform.position = spawnmanager.position;
        rb.isKinematic= true;
        Time.timeScale = 0f;
        Main.main.Lock(true);
    }
    public void mapdisable()
    {
        maps.SetActive(false);
        MapIsEnabled = false;
        rb.isKinematic = false;
        Time.timeScale = 1f;
        Main.main.Lock(false);
    }
    private void Treshold()
    {
        if (player.transform.position.y < treshold)
        {
            PlayerDatas.Death=true;
        }
    }


    public void Spawner(int numb)
    {
        Vector3 spawnPosition = SpawnLocations[numb - 1].position; // Elmentjük a spawn pozíció pozícióját egy változóba
        player.transform.position = spawnPosition+new Vector3(0,5,0); // Beállítjuk a játékos pozícióját a spawn pozícióra
        spawncount = numb;
        mapdisable();

    }
}