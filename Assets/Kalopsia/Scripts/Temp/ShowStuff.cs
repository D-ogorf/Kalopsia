using UnityEngine;
using UnityEngine.UI;

public class ShowStuff : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("player");
    }
    void Update()
    {
        GetComponent<Text>().text = "Stamina: " + player.GetComponent<mCh_Mov>().curStamina + " Vel: " + player.GetComponent<Rigidbody2D>().linearVelocity;
    }
}
