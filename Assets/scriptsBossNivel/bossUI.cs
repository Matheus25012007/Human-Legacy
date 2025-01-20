using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossUI : MonoBehaviour
{
    public GameObject painelBoss;
    public GameObject Muros;


    public static bossUI instance;

    private void Awake()
    { 
       if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        painelBoss.SetActive(false);
        Muros.SetActive(false);       


    }

    public void AtivacaoBoss()
    {
        painelBoss.SetActive(true);
        Muros.SetActive(true);
    }


   
    // Update is called once per frame
    void Update()
    {
        
    }
}
