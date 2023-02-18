using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    public enum AimState
    {
        aim,
        fire,
        end
    }
    [Header("State Machine")]
    public AimState currentState;

    [Header("Aim")]
    private Vector2 mouseStartPos;
    private Vector2 mouseEndPos;
    private float velocityX, velocityY;

    [Header("Game Objs")]
    public GameObject arrow;


    private BallsControl ballsControl;
    // Start is called before the first frame update
    void Start()
    {
        arrow.SetActive(false);
        currentState = AimState.aim;
        ballsControl = FindObjectOfType<BallsControl>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AimState.aim:
                aimArrow();
                break;
            case AimState.fire:
                break;
            case AimState.end:
                break;
        }
    }
    private void aimArrow()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            Mousehold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseRelease();
        }
    }

    private void MouseDown()
    {
        mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Mousehold()
    {
        //aim with the arrow;
        arrow.SetActive(true);
        mouseEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        velocityX = (mouseStartPos.x - mouseEndPos.x);
        velocityY = (mouseStartPos.y - mouseEndPos.y);
        if (velocityY <= 0)
            velocityY = 0.01f;
        float theta = Mathf.Rad2Deg * Mathf.Atan(velocityX / velocityY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

    private void MouseRelease()
    {
        arrow.SetActive(false);
        Vector2 tempVelocity = new Vector2(velocityX, velocityY).normalized;
        if (tempVelocity == Vector2.zero)
            return;
        ballsControl.MoveBall(tempVelocity);
        currentState = AimState.fire;
    }

    public void AimStateToAim(){
        currentState = AimState.aim;
    }

    public void AimStateToEnd(){
        currentState = AimState.end;
    }
}