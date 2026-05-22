using UnityEngine;

[System.Serializable]
    public class Weapon
    {
        public string weaponName;
        public float minimumFireRate;
        public float maximumFireRate;
        public float shotsToBuildUp;

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
        public float innacuracy;

        public GameObject bulletFX;
        public ParticleSystem blastFX;
        public GameObject spawnPoint;
    }
public class Gun_Array : MonoBehaviour
{
    public Weapon[] weapons;
    public int curWeapon;
}
