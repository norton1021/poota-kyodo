using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SpecialAttack : MonoBehaviour
{
    public GameObject specialCutIn;
    public Slider specialGauge;
    public float maxGauge = 100f;
    public int damage = 100;
    private bool canUse = false;
    private bool isPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        specialCutIn.SetActive(false);
        specialGauge.maxValue = maxGauge;
        specialGauge.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current!=null&&Keyboard.current.qKey.wasPressedThisFrame)
        {
            UseSpecial();
        }
    }
    public void AddGauge(float amount)
    {
        specialGauge.value += amount;
        if (specialGauge.value >= maxGauge)
        {
            specialGauge.value = maxGauge;
            canUse = true;
        }
    }
    void UseSpecial()
    {
        if (!canUse) return;
        if (isPlaying) return;
        StartCoroutine(SpecialAttackCoroutine());
    }
    IEnumerator SpecialAttackCoroutine()
    {
        isPlaying = true;
        canUse = false;
        specialGauge.value = 0;
        specialCutIn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys)
        {
            enemy.SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
        }
        specialCutIn.SetActive(false);
        isPlaying = false;
    }
}
