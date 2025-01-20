using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class barraDeVida : MonoBehaviour
{
    public bool isDead;
    public Slider slider;
    public float vida = 100f;
    public Animator anim;
    public Movimentação Player;
    public Ataque Ataq;
    public Ataque_Inimigo_Medio ata;
    public Fogo_Boss flame;
    public CanvasGroup respawnButtonCanvasGroup; // CanvasGroup do botão de respawn

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Movimentação>();
        Ataq = FindObjectOfType<Ataque>();
        ata = FindObjectOfType<Ataque_Inimigo_Medio>();
        anim = Player.GetComponent<Animator>();
        respawnButtonCanvasGroup = GameObject.Find("revive").GetComponent<CanvasGroup>(); //
        HideRespawnButton(); // Esconder o botão no início
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = vida;

        if (vida <= 0)
        {
            if (!isDead)
            {
                isDead = true;
                StartCoroutine(HandleDeath());
            }
        }
        else if (isDead)
        {
            isDead = false;
            HideRespawnButton(); // Esconder o botão se a vida for maior que zero
        }

        if (isDead && !Application.isMobilePlatform && Input.GetKeyDown(KeyCode.Space))
        {
            RevivePlayer();
        }
    }

    private IEnumerator HandleDeath()
    {
        Debug.Log("Player morreu.");
        Player.anim.SetTrigger("Die");
        Player.GetComponent<Movimentação>().enabled = false;
        Player.GetComponent<BoxCollider2D>().enabled = false;
        Player.GetComponent<Rigidbody2D>().simulated = false;
        ata.GetComponent<Ataque_Inimigo_Medio>().enabled = false;
        Ataq.GetComponent<Ataque>().enabled = false;

        if (Application.isMobilePlatform)
        {
            ShowRespawnButton(); // Mostrar o botão de respawn quando o jogador morrer e o jogo estiver em um dispositivo móvel
        }

        yield return new WaitForSeconds(0.8f);

        if (!Application.isMobilePlatform)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RevivePlayer();
            }
        }
    }

    public void DeadState(float dano)
    {
        vida -= dano;
    }

    // Método para reviver o jogador via botão
    public void RevivePlayerViaButton()
    {
        if (isDead)
        {
            RevivePlayer();
        }
    }

    // Método para reviver o jogador
    private void RevivePlayer()
    {
        Debug.Log("RevivePlayer chamado.");
        FindObjectOfType<PlayerPos>().RevivePlayer();
        Player.GetComponent<Movimentação>().enabled = true;
        Player.GetComponent<BoxCollider2D>().enabled = true;
        Player.GetComponent<Rigidbody2D>().simulated = true;
        ata.GetComponent<Ataque_Inimigo_Medio>().enabled=true;
        Ataq.GetComponent<Ataque>().enabled=true;
        isDead = false;
        vida = 100f;
        Player.anim.SetTrigger("Revive");

        HideRespawnButton(); // Esconder o botão de respawn após reviver
    }

    // Mostrar o botão de respawn
    private void ShowRespawnButton()
    {
        if (respawnButtonCanvasGroup != null)
        {
            respawnButtonCanvasGroup.alpha = 1;
            respawnButtonCanvasGroup.interactable = true;
            respawnButtonCanvasGroup.blocksRaycasts = true;
        }
    }

    // Esconder o botão de respawn
    private void HideRespawnButton()
    {
        if (respawnButtonCanvasGroup != null)
        {
            respawnButtonCanvasGroup.alpha = 0;
            respawnButtonCanvasGroup.interactable = false;
            respawnButtonCanvasGroup.blocksRaycasts = false;
        }
    }
}
