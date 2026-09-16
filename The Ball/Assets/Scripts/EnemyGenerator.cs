using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour
{
    public LayerMask StageLayer;
    private enum MOVE_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }
    MOVE_TYPE move = MOVE_TYPE.LEFT;
    Rigidbody2D rbody2D;
    float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rbody2D= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   private void FixedUpdate()
    {
        Vector3 scale = transform.localScale;
        if (move == MOVE_TYPE.STOP)
        {
            speed = 0;
        }
        else if (move == MOVE_TYPE.RIGHT)
        {
            scale.x = 1;
            speed = 3;
        }
        else if (move == MOVE_TYPE.LEFT)
        {
            scale.x = -1;
            speed = -3;
        }
        transform.localScale = scale;
        rbody2D.linearVelocity = new Vector2(speed, rbody2D.linearVelocity.y);
    }
}