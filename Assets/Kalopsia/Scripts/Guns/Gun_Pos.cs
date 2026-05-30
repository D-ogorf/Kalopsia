using UnityEngine;

public class Gun_Pos : MonoBehaviour
{
    public sbyte leftRightInt;
    public sbyte upDownInt;
    public Vector2 distFromCenter;
    public bool _isLocked;
    [HideInInspector] public Set_mCh settings;
    [HideInInspector] public mCh_Mov player;

    void Start()
    {
        this.player = GetComponentInParent<mCh_Mov>();
    }

    void Update()
    {
        StateChecker();
        PosChange();
        RotChange();
    }

    private void StateChecker()
    {
        if(!this.player._isDashing)
        {
            ChangeUpDown();
            ChangeLeftRight();
        }

        this._isLocked = Input.GetKey(this.settings.lockAim);
    }

    private void ChangeLeftRight()
    {
        if(this.upDownInt != 0 && this.player.leftRightInt == 0 && !Input.GetKey(this.settings.left) && !Input.GetKey(this.settings.right))
        {
            this.leftRightInt = 0;
            return;
        }

        this.leftRightInt = this.player.lastLeftRight;
    }

    private void ChangeUpDown()
    {
        if(!Input.GetKey(this.settings.up) && !Input.GetKey(this.settings.down))
        {
            this.upDownInt = 0;
            return;
        }

        if(Input.GetKey(this.settings.up)) this.upDownInt = 1;

        if(Input.GetKey(this.settings.down)) this.upDownInt = -1;
    }

    private void PosChange()
    {
        if(Mathf.Abs(this.leftRightInt) + Mathf.Abs(this.upDownInt) < 2)
        {
            this.transform.localPosition = new Vector2(this.distFromCenter.x * this.leftRightInt, this.distFromCenter.y * this.upDownInt);
            return;
        }

        float aux = 1/((this.distFromCenter.x + this.distFromCenter.y) / 2);
        this.transform.localPosition = new Vector2(this.distFromCenter.x * this.leftRightInt * aux, this.distFromCenter.y * this.upDownInt * aux);
    }

    private void RotChange()
    {
        Vector2 Posdiff = this.player.transform.position - this.transform.position;
        Posdiff.Normalize();  
        float rotZ = Mathf.Atan2(Posdiff.y, Posdiff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
