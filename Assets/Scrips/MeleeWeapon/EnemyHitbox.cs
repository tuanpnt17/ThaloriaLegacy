using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject bloodPrefab;

    [SerializeField]
    private float hitboxDamage = 10f;

    private bool canHit = true;

    void OnEnable()
    {
        canHit = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canHit)
            return;
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(hitboxDamage);
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            canHit = false;
        }
    }
}
