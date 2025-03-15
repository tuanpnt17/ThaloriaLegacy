using UnityEngine;

public class AlienEnemy : Enemy
{
    [SerializeField] private float attackRange = 5f;
    private AkmGun gun;
    private Transform player1;
    private Transform enemyTransform;

    protected override void Start()
    {
        base.Start();
        enemyTransform = transform; // Gán enemyTransform
        gun = GetComponentInChildren<AkmGun>();
        player1 = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            FlipEnemy();
        }

        if (Vector2.Distance(transform.position, player1.position) < attackRange)
        {
            gun.Shoot(); // Bắn đạn nếu trong phạm vi
        }
    }

    protected override void FlipEnemy()
    {
        if (player == null) return;

        bool facingRight = player1.position.x > transform.position.x;
        enemyTransform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1);
    }
}