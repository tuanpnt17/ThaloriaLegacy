using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float enemyMoveSpeed = 1f;
    protected Player player;

    [SerializeField]
    protected float maxHp = 50f;
    protected float currentHp;

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    protected float enterDamage = 10f;

    [SerializeField]
    protected float stayDamage = 1f;

    [SerializeField]
    protected int killScore = 10;

    private Rigidbody2D rb;
    private EnemySpawner spawnerInstance;
    private float lastTime;

    protected virtual void Start()
    {
        player = FindAnyObjectByType<Player>();
        if (player == null)
        {
            Debug.LogError("Không tìm thấy Player trong scene.");
        }
        currentHp = maxHp;
        UpdateHpBar();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Enemy cần có Rigidbody2D!");
        }
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    protected void MoveToPlayer()
    {
        if (player != null)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = moveDirection * enemyMoveSpeed;
            FlipEnemy();
        }
    }

    protected void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    protected virtual void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(
                player.transform.position.x < transform.position.x ? -1 : 1,
                1,
                1
            );
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        if (spawnerInstance == null)
            spawnerInstance = spawner;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    // Xử lý va chạm vật lý với Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(enterDamage); // Player mất máu
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null)
            {
                if (Time.time - lastTime < 2f)
                {
                    return;
                }
                playerScript.TakeDamage(stayDamage * Time.deltaTime); // Player mất máu liên tục
                lastTime = Time.time;
            }
        }
    }

    private void OnDestroy()
    {
        //ScoreManager.Instance.UpdateKillScore(transform.position, killScore);
        if (spawnerInstance != null)
            spawnerInstance.EnemyDie(transform.position);
    }
}
