
using UnityEngine;
using UnityEngine.UI;

public class DirectionArrow : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public Camera mainCamera;

    public float edgeDistance = 80f;

    void Update()
    {
        if (player == null ||  target == null||mainCamera == null)
            return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

        // 目的地が画面内か
        bool inside =
            screenPos.x > 0 &&
            screenPos.x < Screen.width &&
            screenPos.y > 0 &&
            screenPos.y < Screen.height &&
            screenPos.z > 0;

        // 画面内なら消す
        if (inside)
        {
            GetComponent<Image>().enabled = false;
            return;
        }

        // 画面外なら表示
        GetComponent<Image>().enabled = true;

        // 画面中央から目的地への方向
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 direction = (Vector2)screenPos - center;
        direction.Normalize();

        // 画面端の位置
        float x = Screen.width / 2f - edgeDistance;
        float y = Screen.height / 2f - edgeDistance;

        float scaleX = Mathf.Abs(direction.x) > 0.001f
            ? x / Mathf.Abs(direction.x)
            : float.MaxValue;

        float scaleY = Mathf.Abs(direction.y) > 0.001f
            ? y / Mathf.Abs(direction.y)
            : float.MaxValue;

        float scale = Mathf.Min(scaleX, scaleY);

        Vector2 arrowPosition = center + direction * scale;

        transform.position = arrowPosition;

        // 矢印を目的地の方向に向ける
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
