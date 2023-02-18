using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    private int score;
    private int highScore;
    public TMP_Text show_score;

    private void Start() {
        highScore=PlayerPrefs.GetInt("hightScore");
        ShowScore();
    }
    public void ShowScore(){
        show_score.text = "Score: "+score
                        +"\nTopScore: "+highScore;
    }
    public void SetHighScore(){
        PlayerPrefs.SetInt("hightScore",score);
    }
    public void AddScore(){
        score++;
        ShowScore();
    }
    public int GetScore(){
        return score;
    }
    public int GetHighScore(){
        return highScore;
    }
}
