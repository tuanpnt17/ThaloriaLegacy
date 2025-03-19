using UnityEngine;

public class BasicEnemy : Enemy
{
    private float lastTime;

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
            if (player != null)
            {
                if (Time.time - lastTime < 2f)
                {
                    return;
                }
                lastTime = Time.time;
                player.TakeDamage(stayDamage);
            }
        }
    }
}
