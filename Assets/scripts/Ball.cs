using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ballRig;
    private float timer;
    [SerializeField]private float cd;

    private void Start()
    {
        ballRig = gameObject.GetComponent<Rigidbody2D>();
        timer = cd;
    }
    private void OnEnable() {
        timer = cd;
    }

    private void Update()
    {
        if (ballRig.velocity.y > -0.1f && ballRig.velocity.y < 0.1f)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            timer=cd;
            ballRig.velocity = new Vector2( ballRig.velocity.x , ballRig.velocity.y-0.2f);
        }
    }
}
