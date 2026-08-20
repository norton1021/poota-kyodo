using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;
    
    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int Damage)
    {
        currentHP -= Damage;
        if (currentHP <= 0)
        {
            Debug.Log(gameObject.name + "‚Í“|‚ê‚½");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(gameObject.name + " HP:" + currentHP);
        }
    }
}

   