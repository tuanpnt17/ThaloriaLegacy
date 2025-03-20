using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float maxHp = 200f;

    [SerializeField]
    private float maxMp = 100f; // other mp config is in PlayerActions.cs

    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private Image mpBar;

    [SerializeField]
    private GameManagerUI gameManager;

	private AudioManager audioManager;

    public bool allowFlip = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float currentHp;
    private float currentMp;
    private bool isDead = false;
    private float mpRecoveryRate = 5f;
    private float mpRecoveryDuration = 2f;
    private float nextMpRecoveryTime;
    private bool blockTakeDamage = false;

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("Rigidbody2D is not found in Player!");
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer is not found in Player!");
        if (animator == null)
            Debug.LogError("Animator is not found in Player!");
    }

    private void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
        UpdateHpBar();
        UpdateMpBar();
    }

    private void Update()
    {
        if (isDead)
            return;
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGameMenu();
        }

        RecoverMpOverTime();
    }

    private void MovePlayer()
    {
        Vector2 playerInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        rb.linearVelocity = playerInput.normalized * moveSpeed; // Sửa lỗi linearVelocity

        if (allowFlip && playerInput.x < 0)
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        else if (allowFlip && playerInput.x > 0)
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
		bool isMoving = playerInput != Vector2.zero;
		animator.SetBool("isRun", isMoving);

		if (isMoving)
		{
			audioManager.PlayRunningAudio();
		}
		else
		{
			audioManager.StopRunningAudio();
		}

	}

	public void TakeDamage(float damage)
    {
        if (blockTakeDamage)
            return;
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();

        if (currentHp <= 0)
            Die();
    }

    public void Heal(float healValue)
    {
        currentHp = Mathf.Min(currentHp + healValue, maxHp);
        UpdateHpBar();
    }

    public void Destroy()
    {
        Destroy(gameObject);
        Invoke("LoadGameOver", 1f);
    }

    public void ChangeMp(float mp)
    {
        currentMp += mp;
        currentMp = Mathf.Clamp(currentMp, 0, maxMp);
        UpdateMpBar();
    }

    public float GetMp()
    {
        return currentMp;
    }

    public void SetBlockDamage(bool isBlock)
    {
        blockTakeDamage = isBlock;
    }

    protected virtual void Die()
    {
        //Destroy(gameObject);
        audioManager.PlayGameOverSound();
        audioManager.PlayPlayerDeathSound();
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("Die");
    }

    // For checking collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
    }

    private void LoadGameOver()
    {
        gameManager.GameOverMenu();
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = currentHp / maxHp;
    }

    private void UpdateMpBar()
    {
        if (mpBar != null)
            mpBar.fillAmount = currentMp / maxMp;
    }

    private void RecoverMpOverTime()
    {
        if (Time.time > nextMpRecoveryTime)
        {
            nextMpRecoveryTime = Time.time + mpRecoveryDuration;
            ChangeMp(mpRecoveryRate);
        }
    }
}
