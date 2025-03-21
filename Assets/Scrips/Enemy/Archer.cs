using UnityEngine;

public class Archer : Enemy
{
    [SerializeField]
    private float attackRange = 5f;
    private GameObject archery;
    private Archery archeryScript;
    private Transform player1;
    private Transform enemyTransform;
    private Animator animator;
    private bool isDead = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        enemyTransform = transform; // Assign enemyTransform
        archery = transform.Find("Archery").gameObject;
        archeryScript = GetComponentInChildren<Archery>();
        HideArchery();
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
            PlayAction("Attack");
            archeryScript.Shoot(); // Shoot if in range
        }
        else
        {
            PlayAction("Run");
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
        PlayAction("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void HideArchery()
    {
        archery.SetActive(false);
    }

    private void ShowArchery()
    {
        archery.SetActive(true);
    }

    private void PlayAction(string action)
    {
        if (action.Equals("Attack"))
        {
            StopMoving();
            ShowArchery();
        }
        else if (action.Equals("Run"))
        {
            HideArchery();
        }
        else if (action.Equals("Death"))
        {
            StopMoving();
            HideArchery();
        }
        animator.Play(action);
    }
}
