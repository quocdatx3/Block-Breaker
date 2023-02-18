using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        square,
        hexagonA,
        circle,
        hexagonB,
    }

    public BlockType blockType;
    [SerializeField] private TMP_Text show_life;

    [Header("Block Basic Stats")]
    [SerializeField] private int life;

    [Header("Color Control")]
    [SerializeField] private Gradient gradient;
    private SpriteRenderer blockRenderer;
    private GameManager gameManager; 
    private ScoreManager scoreManager;
    private SoundManager soundManager;
    
    private void OnEnable() {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();
        life = gameManager.level;
        blockRenderer = gameObject.GetComponent<SpriteRenderer>();
        blockRenderer.color = gradient.Evaluate(Random.Range(0f,1f));
        show_life.text = life.ToString();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        life--;
        show_life.text = life.ToString();
        soundManager.BallHit.Play();
        if (life == 0)
        {
            scoreManager.AddScore();
            soundManager.BlockBreak.Play();
            this.gameObject.SetActive(false);
        }
    }

}
