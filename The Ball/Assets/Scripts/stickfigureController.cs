using UnityEngine;

public class stickfigureController : MonoBehaviour
{
    // ボールのオブジェクトを取得
    GameObject ball;
    
    void Start()
    {
        this.ball = GameObject.Find("BallPrefab");
    }

    void Update()
    {
        // 常にボールの上に立たせる
        transform.position = this.ball.transform.position + new Vector3(0, 1.5f, 0);

        // ボールの進行方向から棒人間の向きを変える
        if (this.ball.GetComponent<PlayerController>().rigid2D.linearVelocityX < 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0); 
        }
    }
}
