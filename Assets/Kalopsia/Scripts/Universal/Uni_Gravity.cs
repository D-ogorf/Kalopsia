using UnityEngine;

public class Uni_Gravity : MonoBehaviour
{
    Set_Uni universal;
    Rigidbody2D rb;

    void Start()
    {
        this.universal = GameObject.Find("Scn_Man").GetComponent<Set_Uni>();
        this.rb = this.GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        GravityPull();
    }

    private void GravityPull()
    {
        this.rb.AddForce(this.universal.gravityForce * -this.transform.up);
    }
}
