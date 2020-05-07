using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        gameManScript.score = 0;
        SceneManager.LoadScene("menuScene", LoadSceneMode.Single);
    }
}
