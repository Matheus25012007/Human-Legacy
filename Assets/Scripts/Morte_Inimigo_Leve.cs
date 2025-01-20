using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte_Inimigo_Leve : MonoBehaviour
{
    private int vida_Inimigo = 100;

    public void Dano_Inimigo_Leve(int dano)
    {
        vida_Inimigo -= dano;
        if(vida_Inimigo <=0)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
