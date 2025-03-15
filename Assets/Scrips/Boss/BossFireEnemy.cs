using UnityEngine;

public class BossFireEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedNormalBullet = 20f;
    [SerializeField] private float speedCircureBullet = 10f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private float skillCooldown = 2f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject usbPrefabs;

    protected bool isBattleStarted = false;
    protected DialogueManager dialogueManager;


    protected override void Start()
    {
        base.Start();
        dialogueManager = Object.FindFirstObjectByType<DialogueManager>();
        if (dialogueManager != null)
        {
            StartBattleDialogue();
        }
        else
        {
            Debug.LogError("Không tìm thấy DialogueManager trong scene.");
        }
    }

    protected override void Update()
    {
        Debug.Log("BossEnemy.Update() called"); // Kiểm tra xem Update() có được gọi không
        Debug.Log("isBattleStarted: " + isBattleStarted); // Kiểm tra giá trị của isBattleStarted
        Debug.Log("dialogueManager.dialoguePanel.activeSelf: " + dialogueManager.dialoguePanel.activeSelf); // Kiểm tra trạng thái của dialoguePanel

        base.Update();

        if (!isBattleStarted || dialogueManager.dialoguePanel.activeSelf)
        {
            Debug.Log("Boss không di chuyển vì điều kiện chưa được đáp ứng.");
            return;
        }

        if (Time.time >= nextSkillTime)
        {
            UseSkill();
        }
    }

    protected override void Die()
    {
        StartDeathDialogue();
    }

    protected virtual void StartBattleDialogue()
    {
        isBattleStarted = false;
        string[] dialogues = {
        "Boss: Ta sẽ tiêu diệt ngươi!",
        "Player: Hãy thử xem nào!",
    };
        dialogueManager.StartDialogue(dialogues, () =>
        {
            isBattleStarted = true;
            Debug.Log("dialogueManager.dialoguePanel.SetActive(false) được gọi");
            dialogueManager.dialoguePanel.SetActive(false); // Ẩn dialoguePanel sau khi kết thúc hội thoại
        });
    }


    protected virtual void StartDeathDialogue()
    {
        string[] dialogues = {
            "Boss: Ta không thể thua...!",
            "Player: Mọi chuyện đã kết thúc!"
        };
        dialogueManager.StartDialogue(dialogues, OnDeathDialogueEnd);
    }

    private void OnDeathDialogueEnd()
    {
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
            if (player != null)
            {
                player.TakeDamage(stayDamage);
            }
        }
    }

    private void NormalShootBullet()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedNormalBullet);
        }
    }

    private void CircureBullet()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedCircureBullet);
        }
    }
    private void Heal()
    {
        currentHp = Mathf.Min(currentHp + hpValue, maxHp);
        UpdateHpBar();
    }

    private void Tele()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }
    }

    private void UseSkillRandom()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                NormalShootBullet();
                break;
            case 1:
                CircureBullet();
                break;
            case 2:
                Heal();
                break;
            case 3:
                Tele();
                break;
        }
    }

    private void UseSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        UseSkillRandom();
    }
}
