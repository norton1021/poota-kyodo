using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    GameObject director;
    public SkillManeger skillManeger;

    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        experienceToNextLevel += 50;
        Debug.Log("レベルアップ Lv." + level);
        skillManeger.ShowSkillChoice();
    }
}