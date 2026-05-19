using System;
using System.Collections;
using UnityEngine;

public class mCh_Mov : MonoBehaviour
{
    [Header("Move")]
    public float maxMovSpeed = 60; // Velocidade de movimento máxima
    public float curMovSpeed; // Velocidade de movimento atual

    public float maxVelocityX = 7.5f; // Velocidade máxima X
    [Range(0, 1)] public float acceleration = .575f; // Tempo que demora para acelerar
    [Range(0, 1)] public float deceleration = .885f; // Tempo que demora para desacelerar

    public bool _isGrounded; // Está no chão?
    public bool _isWalking; // Está andando?
    public bool _canWalk; // Pode andar

    [Header("Jump")]
    public float jumpForce = 400; // Força do pulo
    public float maxVelocityY = 10; // Velocidade máxima Y

    public float maxJumpTime = .4f; // Tempo máximo que pode pular
    public float airMultiplier = .5f; // Multiplicador no ar
    public float jumpBuffer = .125f; // Buffer de pulo
    public float coyoteTime = .125f; // Coyote time

    public bool _isJumping; // Está pulando?
    public bool _wantJump; // Quer pular?
    public bool _canJump; // Pode pular?

    [Header("Dash")]
    public float dashForce = 230; // Força do dash

    public float maxDashTime = .2f; // Tempo máximo que pode dar dash
    public float timeToUseDash = .45f; // Tempo que demora para poder usar dash
    public float dashBuffer = .125f; // Buffer de dash

    public bool _isDashing; // Está dando dash?
    public bool _wantDash; // Quer dar dash?
    public bool _isDashInCD; // Dash está em cooldown?
    public bool _canDash = true; // Pode dar dash?
    
    [Header("Stamina")]
    public int maxStamina = 3; // Stamina máxima
    public int curStamina; // Stamina atual
    public float timeToRegenStamina = 1.5f; // Tempo que demora para regenerar uma barra de stamina
    public float curStaminaRegenTime; // Tempo de regeneração do dash atual

    [Header("Misc")]
    public bool _clampVelocityX = true; // Limitar velocidade X?
    public bool _clampVelocityY = true; // Limitar velocidade Y?

    [HideInInspector] public float timeSinceNotGrounded; // Tempo desde que não está no chão

    [HideInInspector] public sbyte leftRightInt; // Direção atual (Esquerda/Direita)
    [HideInInspector] public sbyte lastLeftRight = 1; // Última direção (Esquerda/Direita)
    [HideInInspector] public sbyte upDownInt; // Direção atual (Cima/Baixo)
    [HideInInspector] public sbyte lastUpDown; // Última direção (Cima/Baixo)
    [HideInInspector] public sbyte jumpInt = 0; // Int do pulo
    [HideInInspector] public sbyte dashInt = 0; // Int do dash

    [HideInInspector] public float gravityForce = 150; // Força com que a gravidade puxa para baixo

    [HideInInspector] public Set_mCh settings;
    [HideInInspector] public Set_Uni universal;
    [HideInInspector] public Rigidbody2D rb;

    void Start()
    {
        Initiallizer();
    }

    private void Initiallizer()
    {
        this.universal = GameObject.Find("Scn_Man").GetComponent<Set_Uni>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        StateChecker();
        InputHandler();
        JumpHandler();
        CoyoteHandler();
        StaminaHandler();
        DashHandler();
    }

    private void StateChecker()
    {
        this._isGrounded = Physics2D.BoxCast(this.transform.position, this.transform.localScale, this.transform.localEulerAngles.z, -this.transform.up, this.transform.localScale.y / 2 + .05f, LayerMask.GetMask("Platform"));
        this._canWalk = !this._isDashing;
        this._isWalking = this.rb.linearVelocityX != 0;
        this._isJumping = this.jumpInt != 0;
        this._canJump = !this._isJumping && this.timeSinceNotGrounded <= this.coyoteTime;
        this._isDashing = this.dashInt != 0;
        this._canDash = this.curStamina > 0 && !this._isDashing && !this._isDashInCD;
    }

    private void InputHandler()
    {
        if(!this._canWalk)
        {
            this.leftRightInt = 0;
            return;
        }

        if(Input.GetKey(this.settings.left)) 
        {
            this.leftRightInt = -1;
            this.lastLeftRight = this.leftRightInt;
        }

        if(!Input.GetKey(this.settings.left) && !Input.GetKey(this.settings.right)) this.leftRightInt = 0;

        if(Input.GetKey(this.settings.right)) 
        {
            this.leftRightInt = 1;
            this.lastLeftRight = this.leftRightInt;
        }
    }

    private void JumpHandler()
    {
        if(Input.GetKeyDown(this.settings.jump)) StartCoroutine(JumpBuffer());

        if(this._wantJump && this._canJump)
        {
            StartCoroutine(Jumping());
        }
    }

    private IEnumerator JumpBuffer()
    {
        float t = 0;

        while(t <= this.jumpBuffer)
        {
            t += Time.deltaTime;
            this._wantJump = true;
            yield return null;
        }
        this._wantJump = false;
    }

    private IEnumerator Jumping()
    {
        float t = 0;

        while(t <= this.maxJumpTime && Input.GetKey(this.settings.jump))
        {
            t += Time.deltaTime;
            this.jumpInt = 1;
            if(t > .1f && this.rb.linearVelocityY == 0) break;
            yield return null;
        }
        if(t < this.maxJumpTime) this.rb.linearVelocityY /= 5;
        this.jumpInt = 0;
    }

    private void CoyoteHandler()
    {
        if(!this._isGrounded) this.timeSinceNotGrounded += Time.deltaTime;
        else this.timeSinceNotGrounded = 0;
    }

    private void StaminaHandler()
    {
        if(this.curStamina < this.maxStamina && !this._isDashing) this.curStaminaRegenTime += Time.deltaTime;
        else return;

        if (this.curStaminaRegenTime >= this.timeToRegenStamina)
        {
            this.curStaminaRegenTime = 0;

            if(this.curStamina < this.maxStamina) this.curStamina += 1;
        }
    }

    private void DashHandler()
    {
        if(Input.GetKeyDown(this.settings.dash)) StartCoroutine(DashBuffer());

        if(this._wantDash && this._canDash) StartCoroutine(Dashing());
    }

    private IEnumerator DashBuffer()
    {
        float t = 0;

        while(t <= this.dashBuffer)
        {
            t += Time.deltaTime;
            this._wantDash = true;
            yield return null;
        }
        this._wantDash = false;
    }

    private IEnumerator Dashing()
    {
        float t = 0;

        this.curStamina -= 1;
        while(t <= this.maxDashTime)
        {
            t += Time.deltaTime;

            this.rb.linearVelocityY = 0;
            this._clampVelocityX = false;
            this.dashInt = lastLeftRight;
            if(t > .1f && this.rb.linearVelocityX == 0) break;
            yield return null;
        }
        this._clampVelocityX = true;
        this.dashInt = 0;

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        float t = 0;

        while(t < this.timeToUseDash)
        {
            t += Time.deltaTime;
            this._isDashInCD = true;
            yield return null;
        }
        this._isDashInCD = false;
    }

    void FixedUpdate()
    {
        GravityPull();
        VelocityCalc();
        LinearVelocity();
    } 

    private void GravityPull()
    {
        if(!this._canJump) this.rb.AddForce(universal.gravityForce * -this.transform.up);
    }

    private void VelocityCalc()
    {
        float auxAirMtp;

        if(this._isGrounded) auxAirMtp = 0;
        else auxAirMtp = 1;

        if(this.leftRightInt == 0) this.curMovSpeed = Mathf.Lerp(this.curMovSpeed, 0, this.deceleration);
        else this.curMovSpeed = Mathf.Lerp(this.curMovSpeed, this.maxMovSpeed * this.leftRightInt, this.acceleration * (1 + auxAirMtp * this.airMultiplier));
    }

    private void LinearVelocity()
    {
        this.rb.AddForce
        (
            this.curMovSpeed * this.transform.right * Mathf.Abs(this.leftRightInt) +
            this.jumpForce * this.jumpInt * this.transform.up +
            this.dashForce * this.dashInt * this.transform.right
        );

        if(this.leftRightInt == 0) this.rb.linearVelocityX /= 1/this.deceleration;

        if(this._clampVelocityX) this.rb.linearVelocityX = Mathf.Clamp(this.rb.linearVelocityX, -this.maxVelocityX, this.maxVelocityX);
        if(this._clampVelocityY) this.rb.linearVelocityY = Mathf.Clamp(this.rb.linearVelocityY, -this.maxVelocityY, this.maxVelocityY); 
    }
}