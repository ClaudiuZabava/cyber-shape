using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    private Text _scoreText;
    private void Awake()
    {
        _scoreText = GetComponent<Text>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
            UpdateScore(0, PlayerPrefs.GetInt("Highscore"));
        else
            UpdateScore(0, 0);
    }

    public void UpdateScore(int newScore, int highScore)
    {
        _scoreText.text = "Score: " + newScore + "\n" + "Highscore: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
