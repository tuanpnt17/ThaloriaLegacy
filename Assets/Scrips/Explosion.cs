using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Sát thương của vụ nổ, có thể chỉnh sửa trong Inspector
    [SerializeField] private float damage = 25f;

    // Hàm xử lý khi có đối tượng khác va chạm với vụ nổ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lấy component Player từ đối tượng bị va chạm (nếu có)
        Player player = collision.GetComponent<Player>();

        // Lấy component Enemy từ đối tượng bị va chạm (nếu có)
        Enemy enemy = collision.GetComponent<Enemy>();

        // Nếu đối tượng va chạm là Player, gây sát thương cho Player
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }

        // Nếu đối tượng va chạm là Enemy, gây sát thương cho Enemy
        if (collision.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
        }
    }

    // Hàm hủy vụ nổ khi cần thiết
    public void DestroyExplosion()
    {
        Destroy(gameObject); // Hủy GameObject chứa vụ nổ
    }
}
