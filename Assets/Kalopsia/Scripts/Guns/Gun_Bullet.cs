using Mono.Cecil.Cil;
using UnityEngine;

public class Gun_Bullet : MonoBehaviour
{
    public float dmg;
    public float dmgType;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy") ApplyOnHitFX();
        DestroySelf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy") ApplyOnHitFX();
        DestroySelf();
    }

    private void ApplyOnHitFX()
    {
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
