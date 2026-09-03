using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    AudioSource audioSource;
    public bool isFade;
    public double FadeOutSeconds = 1.0;
    bool isFadeOut = false;
    double FadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            isFadeOut = true;
        }
        if (isFadeOut)
        {
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                isFadeOut = false;
                SceneManager.LoadScene("StoryScene");
            }
            audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);
        }
    }
}
