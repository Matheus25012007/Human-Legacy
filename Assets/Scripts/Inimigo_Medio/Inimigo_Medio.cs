using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Medio : MonoBehaviour
{
    private Transform alvo;

    [SerializeField]
    private float raioVisao;

    [SerializeField]
    public float velocidade_inimigo;

    [SerializeField]
    public float backup_velocidade_inimigo;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer olhar_inimigo;

    [SerializeField]
    private float distanciaMin;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LayerMask layerAreaVisao;

    public BoxCollider2D colliderAtk;
    public BoxCollider2D colliderCheckAtk;

    private Vector3 originalScale;

    // Start is called before the first frame update
    private void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        ProcurarJogador();

        if (this.alvo != null)
        {
            Inimigo_Seguindo();
        }
        else
        {
            PararMovimentacao();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.raioVisao);
    }

    private void ProcurarJogador()
    {
        Collider2D colisor = Physics2D.OverlapCircle(this.transform.position, this.raioVisao, this.layerAreaVisao);
        if (colisor != null)
        {
            this.alvo = colisor.transform;
        }
        else
        {
            this.alvo = null;
        }
    }

    void Inimigo_Seguindo()
    {
        Vector2 posicaoAlvo = this.alvo.position;
        Vector2 posicaoAtual = this.transform.position;

        float distancia = Vector2.Distance(posicaoAtual, posicaoAlvo);
        if (distancia >= this.distanciaMin)
        {
            Vector2 direcao = new Vector2(posicaoAlvo.x - posicaoAtual.x, 0).normalized; // Apenas no eixo X

            this.rb.velocity = new Vector2(this.velocidade_inimigo * direcao.x, 0); // Apenas no eixo X

            if (direcao.x > 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Vira o inimigo para a esquerda
            }
            else if (direcao.x < 0)
            {
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Vira o inimigo para a direita
            }
            animator.SetBool("movendo", true);
        }
        else
        {
            PararMovimentacao();
        }
    }

    private void PararMovimentacao()
    {
        this.rb.velocity = Vector2.zero;
        this.animator.SetBool("movendo", false);
    }
}
