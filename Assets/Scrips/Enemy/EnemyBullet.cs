using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Biến lưu hướng di chuyển của viên đạn
    private Vector3 movementDirection;

    // Prefab hiệu ứng máu khi đạn trúng Player (có thể gán trong Inspector)
    [SerializeField] private GameObject bloodPrefabs;

    void Start()
    {
        // Hủy viên đạn sau 5 giây để tránh tồn tại mãi trong game
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Nếu chưa đặt hướng di chuyển, không làm gì cả
        if (movementDirection == Vector3.zero) return;

        // Di chuyển viên đạn theo hướng đã đặt, đảm bảo tốc độ ổn định theo thời gian
        transform.position += movementDirection * Time.deltaTime;
    }

    // Hàm đặt hướng di chuyển cho viên đạn
    public void SetMovementDirection(Vector3 direction)
    {
        movementDirection = direction; // Gán hướng di chuyển
    }

    // Xử lý va chạm với các đối tượng khác trong game
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu viên đạn chạm vào người chơi (Player)
        if (collision.CompareTag("Player"))
        {
            // Lấy script Player từ đối tượng bị va chạm
            Player player = collision.GetComponent<Player>();

            if (player != null) // Đảm bảo Player không null trước khi tiếp tục
            {
                // Nếu có Prefab hiệu ứng máu, tạo hiệu ứng tại vị trí va chạm
                if (bloodPrefabs != null)
                {
                    Instantiate(bloodPrefabs, transform.position, Quaternion.identity);
                }

                // Hủy viên đạn sau khi chạm vào Player
                Destroy(gameObject);
            }

            // Hủy viên đạn dù có tìm thấy Player hay không
            Destroy(gameObject);
        }
    }
}
