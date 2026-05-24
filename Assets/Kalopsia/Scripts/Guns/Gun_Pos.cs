using UnityEngine;

public class Gun_Pos : MonoBehaviour
{
    public sbyte leftRightInt;
    public sbyte upDownInt;
    [HideInInspector] public Set_mCh settings;

    void Start()
    {
        
    }

    void Update()
    {
        StateChecker();
    }

    private void StateChecker()
    {
        if(Input.GetKey(this.settings.left)) this.leftRightInt = -1;

        if(Input.GetKey(this.settings.right)) this.leftRightInt = 1;

        if(!Input.GetKey(this.settings.up) && !Input.GetKey(this.settings.down))
        {
            this.upDownInt = 0;
            return;
        }

        if(Input.GetKey(this.settings.up)) this.upDownInt = 1;

        if(Input.GetKey(this.settings.down)) this.upDownInt = -1;
    }
}
