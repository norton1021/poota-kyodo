using UnityEngine;

public class BroomController : MonoBehaviour
{
    public Transform target; // 追尾するターゲット

    public float moveSpeed = 2.5f; // 移動速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ターゲットが未設定ならタグ "Player" を探す
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        // ターゲット方向のベクトルを計算
        Vector2 direction = (target.position - transform.position).normalized;

        // 直線的に移動（回転なし）
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
}
