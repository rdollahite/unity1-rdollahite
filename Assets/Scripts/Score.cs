using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;

    public static int scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (scoreText != null)
        {
            if (scoreValue > 0)
            {
                scoreText.text = "Score: " + scoreValue.ToString();
            }
            else
            {
                scoreText.text = "Score: 0";
            }
        }
        else
        {
            Debug.Log("Text component not assigned.");
        }
    }
}
