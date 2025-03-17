using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField]
    private float magicSpeed = 10f;

    [SerializeField]
    private float magicDamage = 10f;

    [SerializeField]
    private float magicLifetime = 3f;

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
        if (!isDirectionSet) // If there is no direction, automatically destroy the object
        {
            Debug.LogError("Error: Direction is not assigned. Magic will be destroyed.");
            Destroy(gameObject);
        }
        Destroy(gameObject, magicLifetime);
    }

    void Update()
    {
        if (isDirectionSet) // Only move if the direction is set
        {
            transform.position += (Vector3)moveDirection * magicSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(magicDamage);
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
