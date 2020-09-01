using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : WeaponBase
{
    [Tooltip("Prefab must have bulletController component")]
    public    GameObject prefabBullet;
    public    Transform  bulletSpot;
    public    float      bulletSpeed;
    protected GameObject bullet;

    public    bool  loaded;

    public    float chargeTime = 0.5f;
    protected float charge;
    protected bool  charging;

    public    float reloadTime = 2.0f;
    protected float reload;
    protected bool  reloading;


    private void Start ()
    {
        Load();
    }

    private void Update ()
    {
        if (charging)
        {
            charge = Mathf.Clamp(charge + Time.deltaTime, 0, chargeTime);
        }

        if (reloading)
        {
            reload += Time.deltaTime;
            if (reload >= reloadTime)
            {
                Load();
            }
        }
    }

    public void Load ()
    {
        loaded = true;
        bullet = Instantiate(prefabBullet, bulletSpot.position, bulletSpot.rotation, bulletSpot);
        bullet.Q<Rigidbody>()
            .SetIsKinematic(true);
    }

    public override void Charge ()
    {
        if (!loaded)
            return;

        charging = true;
        charge   = 0f;
    }

    public override void Fire ()
    {
        if (!loaded)
            return;

        loaded    = false;
        reloading = true;
        reload    = 0f;

        // converge with aimsight at 30 units
        Vector3 target = transform.forward * 30f;
        Vector3 direct = (target - bullet.position()).normalized;

        Debug.DrawLine(transform.position, target, Color.yellow, 5f);

        bullet.transform.parent = null;
        bullet.Q<BulletController>().Fire(this);
        bullet.Q<Rigidbody>()
            .SetIsKinematic(false)
            .SetVelocity(direct * bulletSpeed);
    }
}
