using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    private float time;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        // Create instance of GameManager
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        time = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            UpdateTime();
        }
        
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void UpdateTime()
    {
        time -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(time);
    }
}
