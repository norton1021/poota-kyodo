using UnityEngine;

public class PlayerAttack :MonoBehaviour
{
    // UŒ‚—Í
    public int attackPower = 20;
    // UŒ‚”ÍˆÍ
    public float attackRange = 6f;
    // UŒ‚ŠÔŠu
    public float span = 1.0f;
    float delta = 0;

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            Attack();
        }
    }
    void Attack()
    {
        GetComponent<CircleRenderer>().DrawCircle(attackRange);
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