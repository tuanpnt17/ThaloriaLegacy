using UnityEngine;

public class EliteOrc : Enemy
{
    [SerializeField]
    private float attackRange = 1.3f;

    [SerializeField]
    private float attackHeightTolerance = 0.5f;

    [SerializeField]
    private float attackRate = 1f;

    private Animator animator;
    private Transform player1;
    private float nextFireTime;
    private bool isDead = false;
    private bool isAttacking = false;
    private float lastStayDmgTime;

    private AudioManager audioManager;

    protected override void Start()
    {
        base.Start();
        audioManager = FindAnyObjectByType<AudioManager>();
        animator = GetComponent<Animator>();
        player1 = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        if (isAttacking || isDead)
            return;
        if (player != null)
        {
            FlipEnemy();
        }
        else
            return;

        if (
            Mathf.Abs(player1.position.x - transform.position.x) <= attackRange
            && Mathf.Abs(player1.position.y - transform.position.y) <= attackHeightTolerance
        )
        {
            if (Time.time < nextFireTime)
                return;

            audioManager.PlayEnemyAxeSound();
            StopMoving();
            animator.SetBool("IsWalking", false);
            animator.Play("Attack");
            isAttacking = true;
            nextFireTime = Time.time + 1f / attackRate;
        }
        else
        {
            animator.SetBool("IsWalking", true);
            MoveToPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null && Time.time - lastStayDmgTime > damageInterval)
            {
                player.TakeDamage(stayDamage);
                lastStayDmgTime = Time.time;
            }
        }
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

    public void DoneAttack()
    {
        isAttacking = false;
    }
}
