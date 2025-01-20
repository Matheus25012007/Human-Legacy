using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentação : MonoBehaviour
{
    [Header("Horizontal Movement Settings:")]
    [SerializeField] public float walkSpeed = 1;

    [Header("Ground Check Settings:")]
    [SerializeField] public float jumpForce;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rampa_check_distancia;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D Friction;
    [SerializeField] private dash dash;

    // Novas variáveis públicas para definir a posição de spawn
    [Header("Spawn Settings:")]
    [SerializeField] public float spawnX = 0f; // Posição X do spawn
    [SerializeField] public float spawnY = 1f; // Posição Y do spawn

    private float angulo_rampa;
    private bool ta_na_rampa;
    private Vector2 perpendicularSpeed;

    private Rigidbody2D rb;
    private float direcaoX;
    public Animator anim;
    private float InputHorizontal = 0;

    public static Movimentação Instancia;

    private direcao dir;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        this.dir = direcao.direita;

        // Defina a posição de spawn do player usando as variáveis spawnX e spawnY
        transform.position = new Vector3(spawnX, spawnY, transform.position.z);
    }

    void Update()
    {
        if (!this.dash.usado)
        {
            if (!Application.isMobilePlatform)
            {
                GetInputs();
            }

            if (InputHorizontal != 0)
            {
                Debug.Log("MobileMove called with InputHorizontal: " + InputHorizontal);
                Move(InputHorizontal);
            }
            else
            {
                Move(direcaoX);
            }
            Jump();
            Flip(InputHorizontal);
            Flip(direcaoX);
            Detecta_rampa();
            AplicarDash();
        }
    }

    void GetInputs()
    {
        direcaoX = Input.GetAxisRaw("Horizontal");
        Debug.Log("GetInputs called, direcaoX: " + direcaoX);
    }

    public void TouchHorizontal(float direcao)
    {
        InputHorizontal = direcao;
        Debug.Log("TouchHorizontal called, InputHorizontal: " + InputHorizontal);
    }

    public void SetDirectionLeft()
    {
        TouchHorizontal(-1);
        Debug.Log("SetDirectionLeft called");
    }

    public void SetDirectionRight()
    {
        TouchHorizontal(1);
        Debug.Log("SetDirectionRight called");
    }

    public void StopMovement()
    {
        TouchHorizontal(0);
        Debug.Log("StopMovement called");
    }

    void Flip(float direction)
    {
        if (direction < 0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            this.dir = direcao.esquerda;
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            this.dir = direcao.direita;
        }
        Debug.Log("Flip called, transform.localScale: " + transform.localScale);
    }

    public void Move(float direction)
    {
        rb.velocity = new Vector2(walkSpeed * direction, rb.velocity.y);
        anim.SetBool("Correndo", rb.velocity.x != 0 && Grounded());
        Debug.Log("Move called, direction: " + direction + ", rb.velocity: " + rb.velocity);
    }

    public bool Grounded()
    {
        bool grounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround)
                        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
                        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround);
        Debug.Log("Grounded called, grounded: " + grounded);
        return grounded;
    }

    private void Detecta_rampa()
    {
        RaycastHit2D hitSlope = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, rampa_check_distancia, whatIsGround);
        if (hitSlope)
        {
            angulo_rampa = Vector2.Angle(hitSlope.normal, Vector2.up);
            ta_na_rampa = angulo_rampa != 0;
            Debug.Log("Detecta_rampa called, angulo_rampa: " + angulo_rampa);
        }
    }

    void Jump()
    {
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        anim.SetBool("Pulando", !Grounded());
        Debug.Log("Jump called, rb.velocity: " + rb.velocity);
    }

    public void PerformJump()
    {
        if (Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            anim.SetBool("Pulando", true);
            Debug.Log("PerformJump called, rb.velocity: " + rb.velocity);
        }
    }

    public void AplicarDash()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            dash.Aplicar(this.dir);
            Debug.Log("AplicarDash called, dir: " + this.dir);
        }
    }

    public void PerformDash()
    {
        dash.Aplicar(this.dir);
        Debug.Log("PerformDash called, dir: " + this.dir);
    }
}