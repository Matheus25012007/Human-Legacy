using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    private bool ataque;
    public Animator animacao;
    public Transform posicao_ataque;
    public float range_ataque = 0.6f;
    public LayerMask inimigo_layer;

    void Start()
    {

    }

    void Update()
    {
        ataque = Input.GetButtonDown("Ataque_1");

        if (ataque)
        {
            ExecutarAtaque();
        }
    }

    public void BotaoAtaque()
    {
        ExecutarAtaque();
    }

    void ExecutarAtaque()
    {
        // Animação
        animacao.SetTrigger("atacando");

        // Range de Ataque
        Collider2D[] dano_inimigo = Physics2D.OverlapCircleAll(posicao_ataque.position, range_ataque, inimigo_layer);

        foreach (Collider2D inimigo in dano_inimigo)
        {
            inimigo.GetComponent<Morte_Inimigo_Leve>().Dano_Inimigo_Leve(100);   
        }
        Efeitos.instance.SomAtaque.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(posicao_ataque.position, range_ataque);
    }

   
}
