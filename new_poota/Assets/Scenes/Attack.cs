using UnityEngine;

public class PlayerAttack :MonoBehaviour
{
    public int attackPower = 20;
    public float attackRange = 6f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            { 
            Attack();
        }
    }
    void Attack()
    {
        Debug.Log("UŒ‚‚µ‚½");
        Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(
            transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("UŒ‚”ÍˆÍ“à" + enemy.gameObject.name);
            if (enemy.CompareTag("Enemy"))
            {
                Health health1 = enemy.GetComponentInParent<Health>();
                Health health = health1;
                if (health != null)
                {
                    health.TakeDamage(attackPower);
                    Debug.Log("“G‚Éƒ_ƒ[ƒW");
                }
                else
                {
                    Debug.Log("Enemy‚ÉHealth‚ª‚ ‚è‚Ü‚¹‚ñ");
                }
            }
        }
        Debug.Log("UŒ‚");
    }
}