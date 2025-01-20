using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fogo_Boss : MonoBehaviour
{
    float velocidade_movimento;
    Rigidbody2D rb2d;
    Vector2 direcao_move;
    Movimentação alvo;
    public float dano = 10f; // Quantidade de dano causado pelo ataque
    barraDeVida heart;

    // Start is called before the first frame update
    void Start()
    {
        velocidade_movimento = GetComponent<Enemy>().velocidade;
        rb2d = GetComponent<Rigidbody2D>();
        alvo = Movimentação.Instancia;

        direcao_move = (alvo.transform.position - transform.position).normalized * velocidade_movimento;
        rb2d.velocity = new Vector2(direcao_move.x, direcao_move.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Supondo que o jogador tenha um script de saúde
            barraDeVida playerHealth = other.GetComponent<barraDeVida>();
            if (playerHealth != null)
            {
                playerHealth.DeadState(dano);
            }
            Destroy(gameObject); // Destrói o projétil após o impacto
        }
    }

}
