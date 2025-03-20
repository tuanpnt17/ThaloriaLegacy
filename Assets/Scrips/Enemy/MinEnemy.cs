using UnityEngine;

public class MinEnemy : Enemy
{
    private float lastStayDmgTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null && Time.time - lastStayDmgTime > damageInterval)
            {
                player.TakeDamage(stayDamage);
                lastStayDmgTime = Time.time;
            }
        }
    }
}
