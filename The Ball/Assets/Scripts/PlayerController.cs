using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 600.0f;
    float walkForce = 30;
    float maxWalkSpeed = 2.0f;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        // ジャンプ
        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            this.rigid2D.linearVelocityY == 0)
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        // 左に移動
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            //if (this.rigid2D.linearVelocityX < this.maxWalkSpeed)
            {
                this.rigid2D.AddForce(-transform.right * this.walkForce);
            }
        }

        // 右に移動
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            //if (this.rigid2D.linearVelocityX < this.maxWalkSpeed)
            {
                this.rigid2D.AddForce(transform.right * this.walkForce);
            }
        }
    }
}
