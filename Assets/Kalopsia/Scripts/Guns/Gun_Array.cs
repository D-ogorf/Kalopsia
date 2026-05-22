using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public float minimumFireRate;
    public float maximumFireRate;
    public float shotsToBuildUp;
    public bool _isAuto;

    public bool _hasWeapon;
    public float timeSinceLastAtack;
    public float curFireRate;

    public Bullets[] bullets;
}
[System.Serializable]
public class Bullets
{
    public float dmg;
    public int dmgType;
    public int ammoType;
    public int ammoCost;
    public float speed;
    public float delay;
    public float rotation;
    public float innacuracy;

    public GameObject bulletFX;
    public ParticleSystem blastFX;
    public GameObject spawnPoint;
}
public class Gun_Array : MonoBehaviour
{
    public Weapon[] weapons;
    public int curWeapon;
    public bool _wantShoot;
    public bool _canShoot;
    public float shootBuffer;

    public Set_mCh settings;

    private void Start()
    {
        StartCoroutine(IncreaseTimeSinceAtk());
    }

    private IEnumerator IncreaseTimeSinceAtk()
    {
        while(true)
        {
            IncreaseDeltaTime();
            yield return null;
        }
    }

    private void IncreaseDeltaTime()
    {
        for(int i = 0; i < weapons.Count(); i++)
        {
            this.weapons[i].timeSinceLastAtack += Time.deltaTime;
        }
    }

    private void Update()
    {
        StateChecker();
        ShootHandler();
    }

    private void StateChecker()
    {
        this._canShoot = this.weapons[curWeapon].timeSinceLastAtack >= 1 / this.weapons[curWeapon].curFireRate;
    }

    private void ShootHandler()
    {
        if(this.weapons[curWeapon]._isAuto)
        {
            if(Input.GetKey(this.settings.shoot)) StartCoroutine(ShootBuffer());
        }
        else
        {
            if(Input.GetKeyDown(this.settings.shoot)) StartCoroutine(ShootBuffer());
        }

        if(this._wantShoot && this._canShoot) Shoot();
    }

    private IEnumerator ShootBuffer()
    {
        float t = 0;

        while(t <= shootBuffer)
        {
            t += Time.deltaTime;
            _wantShoot = true;
            yield return null;
        }
        _wantShoot = false;
    }

    private void Shoot()
    {
        this.weapons[curWeapon].timeSinceLastAtack = 0;
        for(int i = 0; i < this.weapons[curWeapon].bullets.Count(); i++)
        {
            StartCoroutine(SetBullet(this.weapons[curWeapon].bullets, i));
        }
    }

    private IEnumerator SetBullet(Bullets[] b, int i)
    {
        float t = 0;

        while(t <= b[i].delay)
        {
            t += Time.deltaTime;
            yield return null;
        }

        GameObject bullet = Instantiate(b[i].bulletFX, b[i].spawnPoint.transform.position, Quaternion.identity);
        bullet.transform.localEulerAngles = new Vector3(0, 0, b[i].rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = b[i].speed * bullet.transform.right;
    }
}
