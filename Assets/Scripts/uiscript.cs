using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class uiscript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Health health;
    [SerializeField] List<GameObject> hearts;
    [SerializeField] Vector2 heartInstantiateDis;
    [SerializeField] GameObject heart;
    VariableKeeper variableKeeper;
    bool once = true;
    
    void Awake()
    {
        variableKeeper = FindObjectOfType<VariableKeeper>();    
    }

    void Update()
    {
        showHealthAndScore();
    }

    void showHealthAndScore()
    {
        scoreText.text = variableKeeper.persistentScore.ToString("0000000");
    }


    public void AddScore(int scoreValue)
    {
        variableKeeper.persistentScore += scoreValue;
    }

    public void DeleteHeart(int value)
    {
        if (hearts != null)
        {
            for (int i = 0; i < value; i++)
            {
                int lastIndex = hearts.Count - 1;
                Image lastHeartImage = hearts[lastIndex].GetComponent<Image>();
                lastHeartImage.enabled = !enabled;
                hearts.RemoveAt(lastIndex);
            }
        }
    }
    
    public void AddHeartOnce(int value)
    {
        if (once)
        {
            for (int i = 0; i < value; i++)
            {
                int lastIndex = hearts.Count-1;
                Transform lastGameObjectTransform = hearts[lastIndex].GetComponent<Transform>();
                GameObject newHeart = Instantiate(heart,lastGameObjectTransform.position + (Vector3)heartInstantiateDis,Quaternion.identity,lastGameObjectTransform.GetComponentInParent<Transform>());
                hearts.Add(newHeart);
            }
            health.health += value;
            once = false;
        }
    }

}
