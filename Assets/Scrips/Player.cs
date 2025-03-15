using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameManagerUI gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("Rigidbody2D không tìm thấy trên Player!");
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer không tìm thấy trên Player!");
        if (animator == null)
            Debug.LogError("Animator không tìm thấy trên Player!");
    }

    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGameMenu();
        }
    }

    void MovePlayer()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed; // Sửa lỗi linearVelocity

        if (playerInput.x < 0)
            spriteRenderer.flipX = true;
        else if (playerInput.x > 0)
            spriteRenderer.flipX = false;

        animator.SetBool("isRun", playerInput != Vector2.zero);
    }

    public void TakeDamage(float damage)
    {
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

    protected virtual void Die()
    {
        Destroy(gameObject);
        gameManager.GameOverMenu();
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = currentHp / maxHp;
    }

    // **Thêm Debug để kiểm tra va chạm**
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Va chạm với: " + collision.gameObject.name);
    }
}
