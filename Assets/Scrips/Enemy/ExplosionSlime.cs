using UnityEngine;

public class ExplosionSlime : Enemy
{
    [SerializeField]
    private GameObject explosionPrefabs;

    private Animator animator;
    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (isDead)
            return;
        MoveToPlayer();
    }

    private void CreateExplosion()
    {
        if (explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        }
    }

    protected override void Die()
    {
        isDead = true;
        StopMoving();
        animator.Play("Death");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateExplosion();
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
