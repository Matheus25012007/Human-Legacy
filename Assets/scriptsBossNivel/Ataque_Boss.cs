using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Boss : MonoBehaviour
{
    private bool ataque;
    public Animator animacao;
    public Transform posicao;
    public float range_ataque = 0.6f;
    public LayerMask inimigo_layer;
    public int dano_heroi;


    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ataque = Input.GetButtonDown("Ataque_1");

        if (ataque == true)
        {
            Ataque();
        }


    }
    public void BotaoAtaque2()
    {
        Ataque();
    }
    void Ataque()
    {
        //Animação


        //Range de Ataque
        Collider2D[] dano_inimigo = Physics2D.OverlapCircleAll(posicao.position, range_ataque, inimigo_layer);

        foreach (Collider2D inimigo in dano_inimigo)
        {
            inimigo.GetComponent<Morte_Boss>().Dano_boss(dano_heroi);
        }
        Efeitos.instance.SomAtaque.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(posicao.position, range_ataque);
    }
}

