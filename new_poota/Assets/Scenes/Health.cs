using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int Damage)
    {
        currentHP -= Damage;
        Debug.Log(gameObject.name + "HP:" + currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log(gameObject.name + "‚ª“|‚ê‚½");
        Destroy(gameObject);
    }
}

   