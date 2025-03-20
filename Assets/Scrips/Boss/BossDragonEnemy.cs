using UnityEngine;

public class BossDragonEnemy : Enemy
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
    private GameObject miniEnemies;

    [SerializeField]
    private float skillCooldown = 2f;
    private float nextSkillTime = 0f;

    [SerializeField]
    private GameObject usbPrefabs;

    [SerializeField]
    float breathDuration = 3f; // How long the dragon breathes (adjustable)

    [SerializeField]
    float fireRate = 0.5f; // Time between each bullet (adjustable)

    protected bool isBattleStarted = false;
    protected DialogueManager dialogueManager;
    private bool isBreathing = false;

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

        if (Time.time >= nextSkillTime && !isBreathing)
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
        string[] dialogues = { "Boss: I will destroy you!", "Player: Let's see you try!" };
        dialogueManager.StartDialogue(
            dialogues,
            () =>
            {
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

    private void CreateMiniEnemy()
    {
        Instantiate(miniEnemies, transform.position, Quaternion.identity);
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

    private void DragonBreath()
    {
        StartCoroutine(DragonBreathCoroutine());
    }

    private System.Collections.IEnumerator DragonBreathCoroutine()
    {
        float elapsed = 0f;
        isBreathing = true;
        while (elapsed < breathDuration)
        {
            if (player != null) // Ensure player exists
            {
                // Calculate direction toward the player at this moment
                Vector3 directionToPlayer = (
                    player.transform.position - firePoint.position
                ).normalized;

                // Spawn bullet and set its direction
                GameObject bullet = Instantiate(
                    bulletPrefabs,
                    firePoint.position,
                    Quaternion.identity
                );
                EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
                enemyBullet.SetMovementDirection(directionToPlayer * speedCircureBullet);
                //bullet.GetComponent<SpriteRenderer>().color = Color.green; // Visual cue for dragon breath
            }

            elapsed += fireRate; // Increment elapsed time by fire rate
            yield return new WaitForSeconds(fireRate); // Wait before firing next bullet
        }
        isBreathing = false;
    }

    protected virtual void UseSkillRandom()
    {
        int randomSkill = Random.Range(0, 5);
        switch (randomSkill)
        {
            case 0:
                Debug.Log("Boss is using skill: Normal Shoot");
                NormalShootBullet();
                break;
            case 1:
                Debug.Log("Boss is using skill: Circular Bullet Attack");
                CircureBullet();
                break;
            case 2:
                Debug.Log("Boss is using skill: Healing");
                Heal();
                break;
            case 3:
                Debug.Log("Boss is using skill: Summoning Minions");
                CreateMiniEnemy();
                break;
            case 4:
                Debug.Log("Boss is using skill: Dragon Breath");
                DragonBreath();
                break;
        }
    }

    protected virtual void UseSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        UseSkillRandom();
    }
}
