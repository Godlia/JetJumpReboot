using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    private int currentScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score:\n" + currentScore;
    }



    public void setScore(int score)
    {
        currentScore = score;
    }

    public int getScore() {
        return currentScore;
    }
}
