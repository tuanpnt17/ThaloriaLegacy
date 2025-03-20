using UnityEngine;

public class Archery : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate = 1f; // Shooting speed
    private Transform player;
    private float nextFireTime;
    private Animator animator;
    private bool isShooting = false;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            Rotate();
        }
    }

    public void Shoot()
    {
        if (isShooting || Time.time < nextFireTime)
            return;
        if (arrowPrefab == null || firePoint == null)
            return;

        audioManager.PlayEnemyArcherSound();
        animator.Play("Shoot");
        Invoke("LaunchArrow", 1f);
        isShooting = true;
    }

    private void Rotate()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (transform.lossyScale.x < 0)
        {
            angle += 180f;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void LaunchArrow()
    {
        // Calculate the direction from firePoint to Player
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Rotate the arrow towards the Player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion arrowRotation = Quaternion.Euler(0, 0, angle);

        // Create an arrow with the correct rotation
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, arrowRotation);

        // Assign the flying direction to the arrow
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("Error: Cannot found Arrow script!");
        }

        nextFireTime = Time.time + 1f / fireRate; // Limit the shooting speed
        isShooting = false;
    }
}
