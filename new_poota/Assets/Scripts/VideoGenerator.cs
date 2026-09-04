using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VideoGenerator : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("GameScene");
            GetComponent<AudioSource>().Play();
        }
    }
}