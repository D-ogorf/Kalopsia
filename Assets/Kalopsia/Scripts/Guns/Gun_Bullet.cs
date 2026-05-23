using Mono.Cecil.Cil;
using UnityEngine;

public class Gun_Bullet : MonoBehaviour
{
    public float dmg;
    public int dmgType;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) ApplyOnHitFX(collision.gameObject);
        DestroySelf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) ApplyOnHitFX(collision.gameObject);
        DestroySelf();
    }

    private void ApplyOnHitFX(GameObject e)
    {
        Uni_Health h = e.GetComponent<Uni_Health>();
        h.curHP -= dmg * (1 - h.defense[dmgType]);
        h.CheckIfDead();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
