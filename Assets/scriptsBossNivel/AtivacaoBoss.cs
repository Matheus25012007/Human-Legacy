using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivacaoBoss : MonoBehaviour
{
    public GameObject bossGO;

    private void Start()
    {
        bossGO.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossUI.instance.AtivacaoBoss();
            StartCoroutine(esperarBoss());
           
        }
    }

    IEnumerator esperarBoss()
    {
        var velocidadeAtual = Movimenta��o.Instancia.walkSpeed;
        Movimenta��o.Instancia.walkSpeed = 0;
        bossGO.SetActive (true);
        yield return new WaitForSeconds(3);
        Movimenta��o.Instancia.walkSpeed = velocidadeAtual;
        Destroy(gameObject);
    }
}
