using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private BallsControl ballsControl;
    // Start is called before the first frame update
    void Start()
    {
        ballsControl = FindObjectOfType<BallsControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball"))
        {
            ballsControl.AddBall();
            this.gameObject.SetActive(false);
        }
    }
}
