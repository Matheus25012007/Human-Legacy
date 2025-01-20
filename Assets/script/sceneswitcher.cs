using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneswitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGameScene()
    {
        SceneManager.LoadScene("jogoo");
    }

    // Update is called once per frame
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
