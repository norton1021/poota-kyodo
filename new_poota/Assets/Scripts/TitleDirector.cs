using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
