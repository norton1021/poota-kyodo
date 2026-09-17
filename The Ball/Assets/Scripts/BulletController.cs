using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("ここは設定しなくてもいいです")]
    public float bulletVerocity;

    void Update()
    {
        // フレームごとに等速で移動させる
        transform.Translate(-this.bulletVerocity, 0, 0);

        // 画面外に出たらオブジェクトを破棄する
        if (transform.position.x < -50 ||
            transform.position.x > 50 ||
            transform.position.y < -50 ||
            transform.position.y > 50)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.Find("stickfigure_0"))
        {
            GameObject.Find("GameDirectorPrefab").GetComponent<GameDirector>().Miss();
        }
        else if (collision.gameObject == GameObject.Find("BallPrefab"))
        {
            Destroy(gameObject);
        }
    }
}
