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
        var velocidadeAtual = Movimentação.Instancia.walkSpeed;
        Movimentação.Instancia.walkSpeed = 0;
        bossGO.SetActive (true);
        yield return new WaitForSeconds(3);
        Movimentação.Instancia.walkSpeed = velocidadeAtual;
        Destroy(gameObject);
    }
}
