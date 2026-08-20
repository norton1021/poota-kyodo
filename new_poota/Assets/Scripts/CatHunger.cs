using UnityEngine;
using UnityEngine.UI;

public class CatHunger : MonoBehaviour
{
    [Header("ãÛï†ê›íË")]
    public float maxHunger = 100f;
    public float currentHunger = 100f;
    public float hungerDecreaseSpeed = 1f;
    [Header("UI")]
    public Slider hungerSlider;
    public GameObject hungerBubble;
    public float hungerWarning = 30f;

    void Start()
    {
        currentHunger = maxHunger;
        if (hungerSlider != null)
        {
            hungerSlider.maxValue = maxHunger;
            hungerSlider.value = currentHunger;
        }
        if (hungerBubble != null)
        {
            hungerBubble.SetActive(false);
        }
    }

    void Update()
    {
        currentHunger -= hungerDecreaseSpeed * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        if (hungerSlider != null)
        {
            hungerSlider.value = currentHunger;
        }
        if (currentHunger <= hungerWarning)
        {
            if (hungerBubble != null)
            {
                hungerBubble.SetActive(true);
            }
        }
        else
        {
            if (hungerBubble != null)
            {
                hungerBubble.SetActive(false);
            }
        }
    }

    public void Feed(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        if (hungerSlider != null)
        {
            hungerSlider.value = currentHunger;
        }
    }
}