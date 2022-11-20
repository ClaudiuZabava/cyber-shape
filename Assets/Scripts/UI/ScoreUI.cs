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
        _scoreText.text = "Score: 0";
    }

    public void UpdateScore(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
