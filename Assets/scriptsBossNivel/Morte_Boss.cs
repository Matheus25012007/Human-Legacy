using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;

public class Morte_Boss : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Animator anim;

    private Enemy boss;

   
    

   



    public float tempo_morte;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<Enemy>();

}

    public void Dano_boss(int dano)
    {
        boss.vida -= dano;
        if (boss.vida <= 0)
        {
            boss_Morrendo();
        }
        else
        {
            StartCoroutine(boss_Tomando_Dano());
            //anim.SetTrigger("Tomando_Dano");
        }
    }

    IEnumerator boss_Tomando_Dano()
    {
        boss.velocidade = 0f;
        GetComponent<SpriteRenderer>().material = GetComponent<Blink>().blink;
        yield return new WaitForSeconds(0.5f);
        boss.velocidade = boss.backup_velocidade_inimigo;
        GetComponent<SpriteRenderer>().material = GetComponent<Blink>().original;

    }

    void boss_Morrendo()
    {
        boss.velocidade = 0f;
        boss.backup_velocidade_inimigo = 0f;
        anim.SetBool("morrendo", true);
        Destroy(this.gameObject, tempo_morte);
    }
}
