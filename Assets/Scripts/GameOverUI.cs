using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    VariableKeeper variableKeeper;

    void Awake() 
    {
        variableKeeper = FindObjectOfType<VariableKeeper>();
    }    

    void Start() 
    {
        scoreText.text = variableKeeper.persistentScore.ToString("0000000");    
    }
}
