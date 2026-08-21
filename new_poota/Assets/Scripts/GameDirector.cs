using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameDirector : MonoBehaviour
{
    GameObject expSliderUI;
    GameObject levelTextUI;
    GameObject expTextUI;
    GameObject player;

    void Start()
    {
        this.expSliderUI = GameObject.Find("expSlider");
        this.levelTextUI = GameObject.Find("LevelText");
        this.expTextUI = GameObject.Find("ExpText");
    }

    public void expUI(int level, int experience, int experienceToNextLevel)
    {
        this.levelTextUI.GetComponent<TextMeshProUGUI>().text = "Lv." + level;
        this.expTextUI.GetComponent<TextMeshProUGUI>().text = "EXP " + experience + "/" + experienceToNextLevel;
        this.expSliderUI.GetComponent<Slider>().maxValue = experienceToNextLevel;
        this.expSliderUI.GetComponent<Slider>().value = experience;
    }
}
