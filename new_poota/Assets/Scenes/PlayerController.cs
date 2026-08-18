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
        // ¶–îˆó‚ª‰Ÿ‚³‚ê‚½‚Æ‚«
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(-0.1f, 0, 0);
        }

        // ‰E–îˆó‚ª‰Ÿ‚³‚ê‚½‚Æ‚«
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(0.1f, 0, 0);
        }

        // ã–îˆó‚ª‰Ÿ‚³‚ê‚½‚Æ‚«
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(0, 0.1f, 0);
        }

        // ‰º–îˆó‚ª‰Ÿ‚³‚ê‚½‚Æ‚«
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(0, -0.1f, 0);
        }
    }
}
