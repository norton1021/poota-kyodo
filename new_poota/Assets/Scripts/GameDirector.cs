using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject player;
    GameObject HPUI;
    GameObject playerHpSlider;
    GameObject expSliderUI;
    GameObject levelTextUI;
    GameObject expTextUI;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        
        // プレイヤーの体力のUI表示
        this.HPUI = GameObject.Find("HP");
        this.playerHpSlider = GameObject.Find("PlayerHpSlider");

        // レベルと経験値のUI表示
        this.levelTextUI = GameObject.Find("LevelText");
        this.expTextUI = GameObject.Find("ExpText");
        this.expSliderUI = GameObject.Find("ExpSlider");
    }
    void Update()
    {
        HpUI(this.player.GetComponent<PlayerStatus>().maxHP, this.player.GetComponent<PlayerStatus>().currentHP);
        ExpUI(this.player.GetComponent<PlayerLevel>().level, this.player.GetComponent<PlayerLevel>().experience, this.player.GetComponent<PlayerLevel>().experienceToNextLevel);
    }

    public void HpUI(int maxHP, int currentHP)
    {
        this.HPUI.GetComponent<TextMeshProUGUI>().text = "HP: " + currentHP + "/" + maxHP;
        this.playerHpSlider.GetComponent<Slider>().maxValue = maxHP;
        this.playerHpSlider.GetComponent<Slider>().value = currentHP;
    }
    public void ExpUI(int level, int experience, int experienceToNextLevel)
    {
        this.levelTextUI.GetComponent<TextMeshProUGUI>().text = "Lv." + level;
        this.expTextUI.GetComponent<TextMeshProUGUI>().text = "EXP: " + experience + "/" + experienceToNextLevel;
        this.expSliderUI.GetComponent<Slider>().maxValue = experienceToNextLevel;
        this.expSliderUI.GetComponent<Slider>().value = experience;
    }
}
