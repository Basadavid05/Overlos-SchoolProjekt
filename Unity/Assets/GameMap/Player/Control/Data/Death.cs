using UnityEngine;

public class Death : MonoBehaviour
{
    public PlayerDatas data;
    public GameObject areas;

    public void Resurrect()
    {
        Time.timeScale = 1f;
        Main.main.Lock(false);
        PlayerDatas.Death = false;
        data.sliders.value = PlayerDatas.PlayerMaxHealth;
        data.PlayerCurrentHealth = PlayerDatas.PlayerMaxHealth;
        PlayerDatas.PlayerHP= PlayerDatas.PlayerMaxHealth;
        ItemPlacement.instance.Resurect();
        MapControl.MapIsEnabled = true;
        SpawnReferences.Respawnenemy = true;
        this.gameObject.SetActive(false);
    }
}
