using UnityEngine;

public class mCh_States : MonoBehaviour
{
    public bool _isGrounded;
    public bool _isWalking;
    [HideInInspector] public mCh_Mov movement;
    [HideInInspector] public Gun_Array guns;
    [HideInInspector] public Gun_Pos aim;
    void Start()
    {
        Initiallizer();
    }

    private void Initiallizer()
    {
        GameObject player = GameObject.Find("player");
        movement = player.GetComponent<mCh_Mov>();
        guns = player.GetComponentInChildren<Gun_Array>();
        aim = player.GetComponentInChildren<Gun_Pos>();
    }

    void Update()
    {
        
    }
}
