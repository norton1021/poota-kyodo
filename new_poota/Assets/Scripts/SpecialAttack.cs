using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SpecialAttack : MonoBehaviour
{
    public GameObject specialCutIn;
    public Slider specialGauge;
    public float maxGauge = 2000f;
    public int damage = 100;
    private bool canUse = false;
    private bool isPlaying = false;
    public GameObject specialNotReady;
    public GameObject specialReady;
    public float attackRange = 10f;
    public float enemyStopTime = 3f;
    public LayerMask enemyLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        specialCutIn.SetActive(false);
        specialGauge.maxValue = maxGauge;
        specialGauge.value = 0;
        specialNotReady.SetActive(true);
        specialReady.SetActive(false);
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
            specialNotReady.SetActive(false);
            specialReady.SetActive(true);
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
        specialNotReady.SetActive(true);
        specialReady.SetActive(false);
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
    public void UseSpecialAttack()
    {
        Collider2D[] enemies= Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        EnemyGenerator generator = FindObjectOfType<EnemyGenerator>();
            if(generator!=null)
        {
            generator.StopSpawn(enemyStopTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
