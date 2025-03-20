using UnityEngine;

public class BossPharaEnemy : Enemy
{
    [SerializeField]
    private GameObject bulletPrefabs;

    [SerializeField]
    private GameObject healPrefabs;

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
            Debug.LogError("Could not found DialogueManager in scene.");
        }
    }

    protected override void Update()
    {
        Debug.Log("BossEnemy.Update() called");
        Debug.Log("isBattleStarted: " + isBattleStarted);
        Debug.Log(
            "dialogueManager.dialoguePanel.activeSelf: " + dialogueManager.dialoguePanel.activeSelf
        );

        base.Update();

        if (!isBattleStarted || dialogueManager.dialoguePanel.activeSelf)
        {
            Debug.Log("Boss does not move because the condition has not been met.");
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
        string[] dialogues =
        {
            "Boss: You dare disturb my eternal slumber? Kneel before your king.",
            "Player: you’ve been sleeping for centuries - time to go to sleep again.",
        };
        dialogueManager.StartDialogue(
            dialogues,
            () =>
            {
                audioManager.PlayPharaohBossAudio();
                isBattleStarted = true;
                dialogueManager.dialoguePanel.SetActive(false);
            }
        );
    }

    protected virtual void StartDeathDialogue()
    {
        string[] dialogues = { "Boss: I... I can't lose...!", "Player: It's all over!" };
        dialogueManager.StartDialogue(dialogues, OnDeathDialogueEnd);
    }

    private void OnDeathDialogueEnd()
    {
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
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

    private void Curse()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = (player.transform.position - firePoint.position).normalized;
            float spreadAngle = 30f; // Cone width
            int bulletCount = 5;
            float angleStep = spreadAngle / (bulletCount - 1);

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = -spreadAngle / 2 + i * angleStep;
                Vector3 bulletDir = Quaternion.Euler(0, 0, angle) * directionToPlayer;
                GameObject bullet = Instantiate(
                    bulletPrefabs,
                    firePoint.position,
                    Quaternion.identity
                );
                EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
                enemyBullet.SetMovementDirection(bulletDir * speedCircureBullet);
            }
        }
    }

    private void Heal()
    {
        currentHp = Mathf.Min(currentHp + hpValue, maxHp);
        GameObject heal = Instantiate(healPrefabs, transform.position, Quaternion.identity);
        heal.transform.SetParent(transform);
        heal.transform.localScale = new Vector3(2, 2, 1);
        Destroy(heal, 1f);
        UpdateHpBar();
    }

    protected virtual void UseSkillRandom()
    {
        int randomSkill = Random.Range(0, 3);
        switch (randomSkill)
        {
            case 0:
                audioManager.PlayPharaohBossAttackSound();
                Debug.Log("Boss đang sử dụng skill: Bắn đạn thường");
                NormalShootBullet();
                break;
            case 1:
                audioManager.PlayPharaohBossAttackSound();
                Debug.Log("Boss is using skill: Curse");
                Curse();
                break;
            case 2:
                Debug.Log("Boss is using skill: Healing");
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
