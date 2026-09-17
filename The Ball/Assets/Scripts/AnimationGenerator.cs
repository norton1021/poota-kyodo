using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    [Header("画像")]
    public Sprite normalSprite;
    public Sprite squashedSprite;

    [Header("ジャンプ")]
    public float jumpPower = 18f;

    [Header("縮んでいる時間")]
    public float squashedTime = 0.15f;

    private SpriteRenderer spriteRenderer;
    private bool isActive = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 最初は通常画像
        spriteRenderer.sprite = normalSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb =
                collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 高くジャンプ
                playerRb.linearVelocity = new Vector2(
                    playerRb.linearVelocity.x,
                    jumpPower
                );

                // 縮む
                StartCoroutine(Squash());
            }
        }
    }

    IEnumerator Squash()
    {
        isActive = false;

        // 縮んだ画像に変更
        spriteRenderer.sprite = squashedSprite;

        yield return new WaitForSeconds(squashedTime);

        // 通常画像に戻す
        spriteRenderer.sprite = normalSprite;

        isActive = true;
    }
}
