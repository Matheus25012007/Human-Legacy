using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_Behaviour : MonoBehaviour
{
    public Transform[] transforms;
    public float vidaBoss, vidaAtual;
    public Image vidaImg;
    public GameObject flame;
    public float tempo_ataque, countdown;

    public float delayPraTP, contagemPraTP;
    // Start is called before the first frame update
    private void Start()
    {
        var posicaoInicial = Random.Range(0, transforms.Length);
        transform.position = transforms[posicaoInicial].position;
        contagemPraTP = delayPraTP;
        countdown = tempo_ataque;
    }

    // Update is called once per frame
    private void Update()
    {
        countdown -= Time.deltaTime;

        contagemPraTP -= Time.deltaTime;
        DamageBoss();
        BossScale();
        
        if(countdown<0)
        {
            Ataque_Player();
            countdown = tempo_ataque;
        }
        if(contagemPraTP <= 0 )
        {
            contagemPraTP = delayPraTP;
            TP();

        }
    }

    public void Ataque_Player()
    {
            GameObject spell = Instantiate(flame, transform.position, Quaternion.identity);
    }

    public void DamageBoss()
    {
        vidaAtual = GetComponent<Enemy>().vida;
        vidaImg.fillAmount = vidaAtual / vidaBoss;
    }

    public void TP()
    {
        var posicaoInicial = Random.Range(0, transforms.Length);
        transform.position = transforms[posicaoInicial].position;
    }

    public void BossScale()
    {
        if(transform.position.x > Movimentação.Instancia.transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}
