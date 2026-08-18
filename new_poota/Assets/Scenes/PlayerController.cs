using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // 食べ物を拾うことで減速
        float speedy;
        if (GetComponent<playerfood>().hasfood)
            speedy = 0.7f;
        else
            speedy = 1.0f;

        // Aキーで左に移動
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(-0.1f * speedy, 0, 0);
        }


        // Dキーで右に移動
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(0.1f * speedy, 0, 0);
        }

        // Wキーで上に移動
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(0, 0.1f * speedy, 0);
        }

        // Sキーで下に移動
        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(0, -0.1f * speedy, 0);
        }
    }
}
