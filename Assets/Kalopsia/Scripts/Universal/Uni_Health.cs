using UnityEngine;

public class Uni_Health : MonoBehaviour
{
    public float maxHP;
    public float curHP;
    public float[] defense;

    public bool _isDead;

    void Start()
    {
        defense = new float[10];
    }

    public void CheckIfDead()
    {
        if(curHP <= 0)
        {
            _isDead = true;
        }
    }
}
