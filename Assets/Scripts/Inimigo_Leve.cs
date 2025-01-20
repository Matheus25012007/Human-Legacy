using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inimigo_Leve : MonoBehaviour
{
    public float velocidade;
    public float distancia;

    bool isLeft = true;

    public Transform verificador_de_chao;

   


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        RaycastHit2D chao = Physics2D.Raycast(verificador_de_chao.position, Vector2.down, distancia);

        if (chao.collider == false)
        {
            if (isLeft == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }

       

    }
}
