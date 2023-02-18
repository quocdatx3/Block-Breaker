using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    private ScoreManager scoreManager;
    public TMP_Text show_End_Score;
    private void OnEnable() {
        scoreManager = FindObjectOfType<ScoreManager>();
        int score = scoreManager.GetScore();
        int highScore = scoreManager.GetHighScore();
        if(score>highScore){
            scoreManager.SetHighScore();
            show_End_Score.text = "New Highscore: "+score;
        }else{
            show_End_Score.text = "Score: "+score;
        }
    }
}
