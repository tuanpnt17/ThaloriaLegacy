using UnityEngine;

public class EnergyEnemy : Enemy
{
    [SerializeField]
    private GameObject energyObject;

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

    protected override void Die()
    {
        if (energyObject != null)
        {
            GameObject energy = Instantiate(energyObject, transform.position, Quaternion.identity);
            Destroy(energy, 5f);
        }
        base.Die();
    }
}
