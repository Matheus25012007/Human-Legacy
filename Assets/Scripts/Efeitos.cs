using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efeitos : MonoBehaviour
{
    public static Efeitos instance;
       
    public AudioSource SomAtaque;

    private void Awake()
    {
        instance = this;
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
