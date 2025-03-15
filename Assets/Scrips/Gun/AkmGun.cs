using UnityEngine;

public class AkmGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f; // Tốc độ bắn
    private Transform player;
    private Transform enemyTransform;
    private float nextFireTime;

    void Start()
    {
        enemyTransform = transform.parent; // Enemy là cha của súng
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
        {
            RotateGun();
        }
    }

    public void Shoot()
    {
        if (Time.time < nextFireTime) return;
        if (bulletPrefab == null || firePoint == null) return;

        // Tính toán hướng từ firePoint đến Player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Xoay viên đạn theo hướng Player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

        // Tạo đạn với góc quay đúng hướng
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        // Gán hướng bay cho đạn
        AkmBullet bulletScript = bullet.GetComponent<AkmBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("⚠️ Lỗi: Không tìm thấy script AkmBullet trên đạn!");
        }

        nextFireTime = Time.time + 1f / fireRate; // Giới hạn tốc độ bắn
    }

    private void RotateGun()
    {
        // Lấy góc quay của Enemy
        float enemyRotation = transform.parent.rotation.eulerAngles.z;

        // Xoay súng theo Enemy
        transform.rotation = Quaternion.Euler(0, 0, enemyRotation);

        // Lật súng theo hướng của Enemy
        bool isFlipped = transform.parent.localScale.x < 0;
        transform.localScale = new Vector3(1, isFlipped ? 1 : 1, 1);
    }


}
