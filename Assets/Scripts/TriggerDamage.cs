using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public barraDeVida heart;
    public Movimentação player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            heart.vida = heart.vida - 50;
            player.anim.SetTrigger("TakeDamage");
        }
    }
}
