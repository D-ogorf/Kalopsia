using UnityEngine;

public class Gun_Shotgun : MonoBehaviour
{
    public int pelletNum = 5;
    public float spread = 10;
    public GameObject pellet;

    [HideInInspector] public Set_mCh settings;
    public void Update()
    {
        ShootHandler();
    }

    private void ShootHandler()
    {
        
    }
}
