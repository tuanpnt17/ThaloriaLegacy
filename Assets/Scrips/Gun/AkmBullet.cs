using UnityEngine;

public class AkmBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] private GameObject bloodPrefab;
    private Vector2 moveDirection;
    private bool isDirectionSet = false; // Kiểm tra đã gán hướng chưa

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        isDirectionSet = true; // Đánh dấu hướng đã được thiết lập
    }

    void Start()
    {
        if (!isDirectionSet) // Nếu chưa có hướng, tự động hủy đạn
        {
            Debug.LogError("⚠️ Lỗi: moveDirection chưa được gán! Đạn sẽ bị hủy.");
            Destroy(gameObject);
        }
        Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
        if (isDirectionSet) // Chỉ di chuyển nếu hướng đã được gán
        {
            transform.position += (Vector3)moveDirection * bulletSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
