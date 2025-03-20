using UnityEngine;

public class BossRockEnemy : Enemy
{
    [SerializeField]
    private GameObject bulletPrefabs;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float speedNormalBullet = 20f;

    [SerializeField]
    private float speedCircureBullet = 10f;

    [SerializeField]
    private float hpValue = 100f;

    [SerializeField]
    private float skillCooldown = 2f;
    private float nextSkillTime = 0f;

    [SerializeField]
    private GameObject usbPrefabs;

    protected bool isBattleStarted = false;
    protected DialogueManager dialogueManager;

    private AudioManager audioManager;
    protected override void Start()
    {
        base.Start();
        audioManager = FindAnyObjectByType<AudioManager>();

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
        Debug.Log(
            "dialogueManager.dialoguePanel.activeSelf: " + dialogueManager.dialoguePanel.activeSelf
        ); // Kiểm tra trạng thái của dialoguePanel

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
        string[] dialogues = { "Boss: You don't wanna wake the stones up.", "Player: The rocks might wake up and... do absolutely nothing!" };
        dialogueManager.StartDialogue(
            dialogues,
            () =>
            {
                audioManager.PlayGolemBossAudio();
                isBattleStarted = true;
                Debug.Log("dialogueManager.dialoguePanel.SetActive(false) được gọi");
                dialogueManager.dialoguePanel.SetActive(false); // Ẩn dialoguePanel sau khi kết thúc hội thoại
            }
        );
    }

    protected virtual void StartDeathDialogue()
    {
        string[] dialogues = { "Boss: Ta không thể thua...!", "Player: Mọi chuyện đã kết thúc!" };
        dialogueManager.StartDialogue(dialogues, OnDeathDialogueEnd);
    }

    private void OnDeathDialogueEnd()
    {
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
            Vector3 bulletDirection = new Vector3(
                Mathf.Cos(Mathf.Deg2Rad * angle),
                Mathf.Sin(Mathf.Deg2Rad * angle),
                0
            );
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

    private void RockSmash()
    {
        if (player != null)
        {
            Vector3 dropPosition = player.transform.position + Vector3.up * 10f; // Start above player
            GameObject boulder = Instantiate(bulletPrefabs, dropPosition, Quaternion.identity);
            EnemyBullet enemyBullet = boulder.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(Vector3.down * speedCircureBullet); // Falls downward
            boulder.transform.localScale = Vector3.one * 2f; // Larger size
            //boulder.GetComponent<SpriteRenderer>().color = Color.gray; // Visual cue
        }
    }

    protected virtual void UseSkillRandom()
    {
        int randomSkill = Random.Range(0, 3);
        switch (randomSkill)
        {
            case 0:
                Debug.Log("Boss đang sử dụng skill: Bắn đạn thường");
                NormalShootBullet();
                break;
            case 1:
                Debug.Log("Boss đang sử dụng skill: Bắn đạn hình tròn");
                RockSmash();
                break;
            case 2:
                Debug.Log("Boss đang sử dụng skill: Hồi máu");
                Heal();
                break;
        }
    }

    protected virtual void UseSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        UseSkillRandom();
    }
}
