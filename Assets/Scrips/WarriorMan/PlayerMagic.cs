using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20f;

    [SerializeField]
    private float timeDestroy = 0.5f;

    [SerializeField]
    private float damage = 25f;

    private Animator animator;
    private bool hasExploded = false;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!hasExploded)
                {
                    moveSpeed = 0;
                    animator.SetTrigger("Explore");
                    hasExploded = true;
                }
                enemy.TakeDamage(damage);
            }
        }
    }

    public void DestroyMagic()
    {
        Destroy(gameObject);
    }
}
