using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (player != null)
        {
            Destroy(player); // Destroi o Player existente
        }
        SceneManager.LoadScene("SampleScene");
    }
}
