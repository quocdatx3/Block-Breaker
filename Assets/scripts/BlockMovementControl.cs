using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovementControl : MonoBehaviour
{
    private enum blockState{
        Stop,
        Move
    }

    private blockState currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = blockState.Stop;
    }

    // Update is called once per frame
    public void Move(){
        currentState = blockState.Move;
    }
    void Update()
    {
        if ( currentState == blockState.Move)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y-1);
            currentState = blockState.Stop;
        }
    }
}
