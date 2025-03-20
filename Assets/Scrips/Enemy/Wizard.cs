using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField]
    private float attackRange = 5f;

    [SerializeField]
    private GameObject magicPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireRate = 1f; // Shooting speed

    private Transform player1;
    private Transform enemyTransform;
    private Animator animator;
    private float nextFireTime;
    private bool isShooting = false;
    private bool isDead = false;

    private AudioManager audioManager;

    protected override void Start()
    {
        base.Start();
        audioManager = FindAnyObjectByType<AudioManager>();
        animator = GetComponent<Animator>();
        enemyTransform = transform; // Assign enemyTransform
        player1 = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        if (isDead)
            return;
        if (player != null)
        {
            FlipEnemy();
        }
        else
            return;

        if (Vector2.Distance(transform.position, player1.position) < attackRange)
        {
            if (isShooting || Time.time < nextFireTime)
                return;
            if (magicPrefab == null || firePoint == null)
                return;

            audioManager.PlayEnemyWizardSound();
            StopMoving();
            animator.Play("Attack");
            Invoke("ShootMagic", 1f);
            isShooting = true;
        }
        else
        {
            animator.Play("Walk");
            MoveToPlayer();
        }
    }

    protected override void FlipEnemy()
    {
        if (player == null)
            return;

        bool facingRight = player1.position.x > transform.position.x;
        enemyTransform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
    }

    protected override void Die()
    {
        isDead = true;
        StopMoving();
        animator.Play("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void ShootMagic()
    {
        // Calculate the direction from firePoint to Player
        Vector2 direction = (player1.position - firePoint.position).normalized;

        // Rotate the arrow towards the Player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion arrowRotation = Quaternion.Euler(0, 0, angle);

        // Create an arrow with the correct rotation
        GameObject arrow = Instantiate(magicPrefab, firePoint.position, arrowRotation);

        // Assign the flying direction to the arrow
        Magic magicScript = arrow.GetComponent<Magic>();
        if (magicScript != null)
        {
            magicScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("Error: Cannot found Magic script!");
        }

        nextFireTime = Time.time + 1f / fireRate; // Limit the shooting speed
        isShooting = false;
    }
}
