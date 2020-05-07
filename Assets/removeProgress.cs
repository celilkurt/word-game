using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        PlayerPrefs.SetInt("curLevel", 1);
    }

}
