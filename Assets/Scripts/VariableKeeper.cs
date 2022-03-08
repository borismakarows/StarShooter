using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableKeeper : MonoBehaviour
{
    static VariableKeeper instance;
    uiscript uiScript;
    public int persistentScore;
    void Awake() 
    {
        uiScript = FindObjectOfType<uiscript>();   
        ManageSingleton(); 
    }

    void ManageSingleton()
    {
        
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScore()
    {
        persistentScore = 0;
    }

}
