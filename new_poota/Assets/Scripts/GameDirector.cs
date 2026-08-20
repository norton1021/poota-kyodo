using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject expBarUI;
    GameObject levelTextUI;
    GameObject expTextUI;

    void Start()
    {
        this.expBarUI = GameObject.Find("ExpBar");
        this.levelTextUI = GameObject.Find("LevelText");
        this.expTextUI = GameObject.Find("ExpText");
    }

    public void expUI(int level, int experience, int experienceToNextLevel)
    {
        this.levelTextUI.GetComponent<TextMeshProUGUI>().text = "Lv." + level;
        this.expTextUI.GetComponent<TextMeshProUGUI>().text = "EXP " + experience + "/" + experienceToNextLevel;
        this.expBarUI.GetComponent<Slider>().maxValue = experienceToNextLevel;
        this.expBarUI.GetComponent<Slider>().value = experience;
    }
}
