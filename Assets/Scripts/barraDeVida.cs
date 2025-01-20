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
    public Movimenta��o Player;
    public Ataque Ataq;
    public Ataque_Inimigo_Medio ata;
    public Fogo_Boss flame;
    public CanvasGroup respawnButtonCanvasGroup; // CanvasGroup do bot�o de respawn

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Movimenta��o>();
        Ataq = FindObjectOfType<Ataque>();
        ata = FindObjectOfType<Ataque_Inimigo_Medio>();
        anim = Player.GetComponent<Animator>();
        respawnButtonCanvasGroup = GameObject.Find("revive").GetComponent<CanvasGroup>(); //
        HideRespawnButton(); // Esconder o bot�o no in�cio
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
            HideRespawnButton(); // Esconder o bot�o se a vida for maior que zero
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
        Player.GetComponent<Movimenta��o>().enabled = false;
        Player.GetComponent<BoxCollider2D>().enabled = false;
        Player.GetComponent<Rigidbody2D>().simulated = false;
        ata.GetComponent<Ataque_Inimigo_Medio>().enabled = false;
        Ataq.GetComponent<Ataque>().enabled = false;

        if (Application.isMobilePlatform)
        {
            ShowRespawnButton(); // Mostrar o bot�o de respawn quando o jogador morrer e o jogo estiver em um dispositivo m�vel
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

    // M�todo para reviver o jogador via bot�o
    public void RevivePlayerViaButton()
    {
        if (isDead)
        {
            RevivePlayer();
        }
    }

    // M�todo para reviver o jogador
    private void RevivePlayer()
    {
        Debug.Log("RevivePlayer chamado.");
        FindObjectOfType<PlayerPos>().RevivePlayer();
        Player.GetComponent<Movimenta��o>().enabled = true;
        Player.GetComponent<BoxCollider2D>().enabled = true;
        Player.GetComponent<Rigidbody2D>().simulated = true;
        ata.GetComponent<Ataque_Inimigo_Medio>().enabled=true;
        Ataq.GetComponent<Ataque>().enabled=true;
        isDead = false;
        vida = 100f;
        Player.anim.SetTrigger("Revive");

        HideRespawnButton(); // Esconder o bot�o de respawn ap�s reviver
    }

    // Mostrar o bot�o de respawn
    private void ShowRespawnButton()
    {
        if (respawnButtonCanvasGroup != null)
        {
            respawnButtonCanvasGroup.alpha = 1;
            respawnButtonCanvasGroup.interactable = true;
            respawnButtonCanvasGroup.blocksRaycasts = true;
        }
    }

    // Esconder o bot�o de respawn
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
