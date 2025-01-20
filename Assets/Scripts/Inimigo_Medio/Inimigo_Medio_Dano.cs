using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo_Medio_Dano : MonoBehaviour
{
    public barraDeVida heart;
    public Movimentação player;
    public Animator anim;

    private Inimigo_Medio enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Inimigo_Medio>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("Atacando", true);
        enemy.velocidade_inimigo = 0f;

        yield return new WaitForSeconds(0.50f);
        heart.vida = heart.vida - 50;
        player.anim.SetTrigger("TakeDamage");
        anim.SetBool("Atacando", false);
        enemy.velocidade_inimigo = enemy.backup_velocidade_inimigo;


    }
}