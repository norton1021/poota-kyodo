using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillManeger : MonoBehaviour
{
    public GameObject skillPanel;
    public Button skillButton1;
    public Button skillButton2;
    public Button skillButton3;

    public TMP_Text skillText1;
    public TMP_Text skillText2;
    public TMP_Text skillText3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        skillPanel.SetActive(false);
    }

  
    public void ShowSkillChoice()
    {
        Debug.Log("スキル選択開始");
        skillPanel.SetActive(true);
        skillText1.text = "臭い";
        skillText2.text = "回復";
        skillText3.text = "攻撃";
        Time.timeScale = 0f;
    }
    public void SelectSkill1()
    {
        CloseSkillPanenl();
    }
    public void SelectSkill2()
    {
        CloseSkillPanenl();
    }
    public void SelectSkill3()
    {
        CloseSkillPanenl();
    }
    void CloseSkillPanenl()
    {
        skillPanel.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().control = true;
        Time.timeScale = 1f;
    }
}
