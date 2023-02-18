using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBorder : MonoBehaviour
{
    public bool startPointPosHasSet;
    public GameObject startPoint;
    public BallsControl ballsControl;
    private void Start() {
        ballsControl = FindObjectOfType<BallsControl>();
        startPointPosHasSet = false;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ball")){
            other.gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            other.gameObject.SetActive(false);
            ballsControl.checkEndTurn();
            if(!startPointPosHasSet)
            {
                startPointPosHasSet=true;
                startPoint.transform.position = new Vector2(other.gameObject.transform.position.x,-3.4f);
            }
            other.gameObject.transform.position=startPoint.transform.position;
        }
    }
}
