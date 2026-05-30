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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) ApplyOnHitFX(collision.gameObject);
        DestroySelf();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DestroySelf();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroySelf();
    }

    private void ApplyOnHitFX(GameObject e)
    {
        Uni_Health h = e.GetComponent<Uni_Health>();
        h.curHP -= this.dmg * (1 - h.defense[this.dmgType]);
        h.CheckIfDead();
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
