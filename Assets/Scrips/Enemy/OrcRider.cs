using UnityEngine;

public class OrcRider : Enemy
{
    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float attackHeightTolerance = 0.5f;

    [SerializeField]
    private float attackRate = 0.5f;

    private Animator animator;
    private Transform player1;
    private float nextFireTime;
    private bool isDead = false;
    private bool isAttacking = false;
    private float lastStayDmgTime;

    protected override void Start()
    {
        base.Start();
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

        if (
            Mathf.Abs(player1.position.x - transform.position.x) <= attackRange
            && Mathf.Abs(player1.position.y - transform.position.y) <= attackHeightTolerance
        )
        {
            if (Time.time < nextFireTime)
                return;

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
