using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Morte_Inimigo_Medio : MonoBehaviour
{
    public int vida_inimigo_medio = 100;

    private Animator anim;

    private Inimigo_Medio inimigoMedio;

    public float tempo_morte;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        inimigoMedio = GetComponent<Inimigo_Medio>();
    }

    public void Dano_Inimigo_Medio(int dano)
    {
        vida_inimigo_medio -= dano;
        if (vida_inimigo_medio <= 0)
        {
            Inimigo_Medio_Morrendo();
        }
        else
        {
            StartCoroutine(Inimigo_Tomando_Dano());
            anim.SetTrigger("Tomando_Dano");
        }
    }

    IEnumerator Inimigo_Tomando_Dano()
    {
        inimigoMedio.velocidade_inimigo = 0f;
        yield return new WaitForSeconds(0.9f);
        inimigoMedio.velocidade_inimigo = inimigoMedio.backup_velocidade_inimigo;

    }

    void Inimigo_Medio_Morrendo()
    {
        inimigoMedio.velocidade_inimigo = 0f;
        inimigoMedio.backup_velocidade_inimigo = 0f;
        anim.SetBool("morrendo", true);
        Destroy(this.gameObject, tempo_morte);
    }

    // Update is called once per frame
    void Update()
    {

    }
}