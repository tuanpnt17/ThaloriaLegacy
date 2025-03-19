using UnityEngine;

public class PlayerSlash : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;

    [SerializeField]
    GameObject bloodPrefabs;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                GameObject blood = Instantiate(
                    bloodPrefabs,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(blood, 1f);
            }
            Destroy(gameObject);
        }
    }
}
