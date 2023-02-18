using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallsControl : MonoBehaviour
{
    public List<GameObject> ballsList = new List<GameObject>();
    [SerializeField] private TMP_Text show_Ball_Number;
    private int ballsNumber;
    public GameObject ball;
    public Transform ballsHolder;
    public Transform startPoints;
    private bool isBallsMove;
    private int ballsListIndex,maxBallsThisTurn;
    private Vector2 moveVelocity;
    private Vector2 thisTurnPos;
    public float ballCDTime;
    private float ballTimer;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        AddBall();
        ballTimer = 0f;
        ballsNumber = ballsList.Count;
        show_Ball_Number.text = ballsNumber.ToString();
    }

    public void AddBall()
    {
        GameObject go = Instantiate(ball, startPoints.position, Quaternion.identity);
        ballsList.Add(go);
        go.transform.SetParent(ballsHolder);
        if (ballsList.Count > 1) go.SetActive(false);
    }

    private void Update()
    {
        if (isBallsMove)
        {
            if (ballTimer <= 0f && ballsListIndex < maxBallsThisTurn)
            {
                ballsList[ballsListIndex].SetActive(true);
                ballsList[ballsListIndex].transform.position = thisTurnPos;
                ballsList[ballsListIndex].GetComponent<Rigidbody2D>().velocity=moveVelocity*5f;
                ballsListIndex++;
                ballTimer = ballCDTime;
                ballsNumber--;
                show_Ball_Number.text = ballsNumber.ToString();
                Debug.Log(ballsListIndex);
            }
            else
            {
                ballTimer -= Time.deltaTime;
            }
            if(ballsListIndex >= maxBallsThisTurn)
                isBallsMove=false;
        }
    }
    public void MoveBall(Vector2 velocity)
    {
        moveVelocity = velocity;
        ballsListIndex = 0;
        isBallsMove = true;
        maxBallsThisTurn = ballsList.Count;
        thisTurnPos = startPoints.position;
    }

    public void checkEndTurn()
    {
        for (int i = 0; i < ballsList.Count; i++)
        {
            if (ballsList[i].activeInHierarchy)
            {
                return;
            }
        }
        ballsList[0].SetActive(true);
        ballTimer = 0f;
        ballsNumber = ballsList.Count;
        show_Ball_Number.text = ballsNumber.ToString();
        StartCoroutine(gm.EndTurn());
    }
}
