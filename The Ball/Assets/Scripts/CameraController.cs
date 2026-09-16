using UnityEngine;

public class CameraController : MonoBehaviour
{
    // カメラモード
    // 固定: fixed
    // 横移動: horizontal
    // 縦移動: vertical
    // 自由: free
    public string cameraMode;

    // プレイヤーのオブジェクトを取得
    GameObject player;
    // 前フレームでのプレイヤーの座標位置
    public Vector3 prePlayerPos;

    void Start()
    {
        this.cameraMode = "fixed";
        this.prePlayerPos = transform.position;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // カメラを固定
        if (this.cameraMode == "fixed")
        {
            transform.position = this.prePlayerPos;
        }
        // 横移動でプレイヤーに追従
        else if (this.cameraMode == "horizontal")
        {
            if (this.player != null &&
                this.player.transform.position != this.prePlayerPos)
            {
                transform.position = new Vector3(this.player.transform.position.x, 0, -10);
                this.prePlayerPos = this.player.transform.position;
            }
        }
        // 縦移動でプレイヤーに追従
        else if (this.cameraMode == "vertical")
        {
            if (this.player != null &&
                this.player.transform.position != this.prePlayerPos)
            {
                transform.position = new Vector3(0, this.player.transform.position.y, -10);
                this.prePlayerPos = this.player.transform.position;
            }
        }
        // 自由にプレイヤーに追従
        else if (this.cameraMode == "free")
        {
            if (this.player != null &&
                this.player.transform.position != this.prePlayerPos)
            {
                transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, -10);
                this.prePlayerPos = this.player.transform.position;
            }
        }
    }
}