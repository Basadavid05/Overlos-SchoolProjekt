using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWithWeapon : MonoBehaviour
{

    [Header("References")]
    [SerializeField] GunData GunDatas;

    [Header("Bullets")]
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    public float BulletVelocity = 30f;
    public float bulletPrefabLifeTime = 20f;

    [Header("Reloading")]
    float CurrentMax;
    float UsedAmmo;
    float ReloadAmmo;

    [Header("Spread")]
    public float SpreadIntensity;
    float timeSinceLastShot;

    private void Start()
    {
        GunDatas.currentAmmo = GunDatas.magSize;
        GunDatas.reloading = false;
        CurrentMax = GunDatas.maxAmmo;
    }

    private void Update()
    {
        bool CanShoot() => !GunDatas.reloading && timeSinceLastShot > 1f / (GunDatas.firerate / 60f);

        if (Input.GetMouseButton(0) && GunDatas.currentAmmo > 0 && CanShoot())
        {
            Shoot();
            GunDatas.currentAmmo--;
            timeSinceLastShot = 0;
            UsedAmmo++;
        }
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R)&& CurrentMax> 0)
        {
            StartReloding();
        }
    }


    private void Shoot()
    {
        //Vector3 ShootingDirection = CalculateDirection().normalized;
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity);
        //bullet.transform.position = ShootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward.normalized * BulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
    }

    public Vector3 CalculateDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - BulletSpawn.position;

        float x = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        float y = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);

        return direction + new Vector3(x, y, 0);
        
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void StartReloding()
    {
        if (!GunDatas.reloading)
        {
            StartCoroutine(ReloadGun());
        }
    }

    private IEnumerator ReloadGun()
    {
        GunDatas.reloading = true;

        yield return new WaitForSeconds(GunDatas.reloadTime);

        CurrentMax -= UsedAmmo;

        UsedAmmo = 0;

        if(CurrentMax>GunDatas.magSize)
        {
            ReloadAmmo = GunDatas.magSize;
        }
        else
        {
            ReloadAmmo = CurrentMax;
        }

        GunDatas.currentAmmo = ReloadAmmo;

        GunDatas.reloading = false;
    }
}
