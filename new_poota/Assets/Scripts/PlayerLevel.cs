using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevel : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    public Slider expBar;
    public TMP_Text levelText;
    public TMP_Text expText;

    void Start()
    {
        UpdateUI();
    }
    public void AddExperience(int amount)
    {
        experience += amount;
        Debug.Log("経験値+" + amount);
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            LevelUp();
        }
        UpdateUI();
    }
    void LevelUp()
    {
        level++;
        experienceToNextLevel += 50;
        Debug.Log("レベルアップ Lv." + level);
    }
    void UpdateUI()
    {
        levelText.text = "Lv." + level;
        expText.text = "EXP" + experience + "/" + experienceToNextLevel;
        expBar.maxValue = experienceToNextLevel;
        expBar.value = experience;
    }
}