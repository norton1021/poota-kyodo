using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;

    void Start()
    {
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().expUI(level, experience, experienceToNextLevel);
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
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().expUI(level, experience, experienceToNextLevel);
    }

    void LevelUp()
    {
        level++;
        experienceToNextLevel += 50;
        Debug.Log("レベルアップ Lv." + level);
    }
}