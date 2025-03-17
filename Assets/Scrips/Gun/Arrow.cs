using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float arrowSpeed = 10f;

    [SerializeField]
    private float arrowDamage = 10f;

    [SerializeField]
    private float arrowLifetime = 3f;

    [SerializeField]
    private GameObject bloodPrefab;
    private Vector2 moveDirection;
    private bool isDirectionSet = false; // Check if the direction is set

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        isDirectionSet = true; // Mark the direction has been set
    }

    void Start()
    {
        if (!isDirectionSet) // If there is no direction, automatically destroy the arrow
        {
            Debug.LogError("Error: Direction is not assigned. Arrow will be destroyed.");
            Destroy(gameObject);
        }
        Destroy(gameObject, arrowLifetime);
    }

    void Update()
    {
        if (isDirectionSet) // Only move if the direction is set
        {
            transform.position += (Vector3)moveDirection * arrowSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(arrowDamage);
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
