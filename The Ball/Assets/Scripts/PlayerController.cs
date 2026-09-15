using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("ジャンプと移動のパラメーター")]
    // ジャンプの初速度
    public float jumpInitialVelocity;
    // 横移動の加速度
    public float rollAcceleration;
    // 横移動の最大速度
    public float maxWalkSpeed;

    [SerializeField, Header("接地判定用レイヤー")]
    // 地面の判定
    LayerMask groundMask;

    // 今ジャンプ中かどうか
    bool jumping;
    // 実寸取得用
    Collider2D col2D;
    // Rigidbody2Dの取得
    Rigidbody2D rigid2D;
    // ステージごとの初期位置を取得
    Vector3 initialposition;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        this.col2D = GetComponent<Collider2D>();
        this.jumping = false;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.initialposition = transform.position;
    }

    void Update()
    {
        HitFloor();

        Jump();

        Roll();

        // 画面下に落ちた場合はステージの最初から
        if (transform.position.y < -10)
        {
            this.rigid2D.bodyType = RigidbodyType2D.Static;
            this.rigid2D.bodyType = RigidbodyType2D.Dynamic;
            transform.position = initialposition;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            GameObject.Find("GameDirector").GetComponent<GameDirector>().Clear();
        }
    }

    // 今床に触れているか、空中状態か判定する
    void HitFloor()
    {
        // もしプレイヤーのColliderが存在しなければ処理しない
        if (col2D == null) return;

        // 接地判定のためのBoxCast（四角形の当たり判定を下に飛ばす）を準備する

        // プレイヤーのコライダーの範囲を取得
        Bounds b = col2D.bounds;
        // 判定を出す位置（プレイヤーの足元より少し下に設定）
        Vector2 center = new Vector2(b.center.x, b.min.y - 0.05f);
        // 判定の大きさ（横幅はキャラより少し小さめ、高さはごく薄い矩形）
        Vector2 size = new Vector2(b.size.x * 0.6f, 0.1f);

        // BoxCastを実行
        // ・center：判定の中心位置
        // ・size  ：判定の大きさ
        // ・0f    ：角度は回転なし
        // ・Vector2.down：真下に向けて判定
        // ・0.05f ：判定を飛ばす長さ（わずかに下へ伸ばす）
        // ・_groundMask：Floorなど「地面」とするレイヤーにだけ反応させる
        RaycastHit2D hit = Physics2D.BoxCast(center, size, 0f, Vector2.down, 0.05f, groundMask);

        // 当たり判定の情報取得
        bool grounded = (hit.collider != null);

        // 地面と触れたか確認
        if (grounded)
        {
            // 地面にいる → ジャンプ可能
            this.jumping = false;
        }
        else
        {
            // 空中にいる → ジャンプ不可
            this.jumping = true;
        }
    }

    // ジャンプ
    void Jump()
    {
        // 空中なら無視
        if (this.jumping)
        {
            return;
        }

        // 上にジャンプ
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.isPressed)
        {
            this.rigid2D.linearVelocityY = this.jumpInitialVelocity;
        }

        this.jumping = true;
    }

    // 移動
    void Roll()
    {
        // 左に移動
        if (Keyboard.current != null &&
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            this.rigid2D.linearVelocityX += -this.rollAcceleration;

            // 横方向速度制限
            if (this.rigid2D.linearVelocityX <= -this.maxWalkSpeed)
            {
                this.rigid2D.linearVelocityX = -this.maxWalkSpeed;
            }
        }

        // 右に移動
        if (Keyboard.current != null &&
            Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            this.rigid2D.linearVelocityX += this.rollAcceleration;

            // 横方向速度制限
            if (this.rigid2D.linearVelocityX >= this.maxWalkSpeed)
            {
                this.rigid2D.linearVelocityX = this.maxWalkSpeed;
            }
        }
    }
}