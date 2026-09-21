using System;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [System.Serializable]
    public class PlatformSettings
    {
        [Header("移動させる？")]
        public bool move = true;
        [Header("移動方向と速度")]
        public Vector2 vector = new Vector2(1, 0);
        [Header("移動距離")]
        public float distance = 1;
        [Header("回す？")]
        public bool rotate;
        [Header("時計回り？")]
        public bool clockwise;
        [Header("回転速度")]
        public float rotateSpeed;
    }

    [SerializeField, Header("足場の設定")]
    PlatformSettings platformSettings;

    // 現在位置をVector2型に変換
    Vector2 Pos;
    // 初期位置の取得
    Vector2 startPos;
    // Rigid2Dの取得
    Rigidbody2D rigid2D;
    // 順方向
    bool forward;

    void Start()
    {
        if (this.platformSettings.move)
        {
            this.Pos = transform.position;
            this.startPos = this.Pos;
            this.rigid2D = gameObject.AddComponent<Rigidbody2D>();
            this.rigid2D.bodyType = RigidbodyType2D.Kinematic;
            this.forward = true;
        }
    }

    void Update()
    {
        // 移動の処理
        if (this.platformSettings.move)
        {
            this.Pos = transform.position;
            
            // 設定した距離だけ移動したら向きを反転
            if ((this.Pos - this.startPos).sqrMagnitude >= this.platformSettings.distance * this.platformSettings.distance)
            {
                this.forward = false;
            }

            // 初期位置まで戻ってきたらまた向きを反転
            if (this.Pos == this.startPos)
            {
                this.forward = true;
            }

            // 移動
            if (this.forward)
            {
                this.rigid2D.linearVelocity = this.platformSettings.vector;
            }
            else
            {
                this.rigid2D.linearVelocity = -this.platformSettings.vector;
            }
        }

        // 回転の処理
        if (this.platformSettings.rotate)
        {
            // 時計回りなら
            if (this.platformSettings.clockwise)
            {
                transform.Rotate(0, 0, -this.platformSettings.rotateSpeed);
            }
            // 反時計回りなら
            else
            {
                transform.Rotate(0, 0, this.platformSettings.rotateSpeed);
            }
        }
    }
}
