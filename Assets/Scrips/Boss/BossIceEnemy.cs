//using System.Diagnostics;
using UnityEngine;

public class BossIceEnemy : Enemy
{
    [SerializeField]
    private GameObject bulletPrefabs;

    [SerializeField]
    private GameObject slowIcePrefabs;

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

    private void IceShower()
    {
        if (player != null)
        {
            int icicleCount = 5; // Number of icicles to drop
            float spreadWidth = 8f; // Horizontal spread area around the player
            float dropHeight = 10f; // Starting height above the player

            for (int i = 0; i < icicleCount; i++)
            {
                // Randomize horizontal position within spreadWidth centered on player
                float xOffset = Random.Range(-spreadWidth / 2f, spreadWidth / 2f);
                Vector3 dropPosition =
                    player.transform.position + new Vector3(xOffset, dropHeight, 0);

                // Spawn icicle
                GameObject icicle = Instantiate(bulletPrefabs, dropPosition, Quaternion.identity);
                EnemyBullet enemyBullet = icicle.AddComponent<EnemyBullet>();
                enemyBullet.SetMovementDirection(Vector3.down * speedCircureBullet); // Falls downward

                // Visual customization
                icicle.transform.localScale = Vector3.one * 1.5f; // Slightly larger than normal bullets
                icicle.GetComponent<SpriteRenderer>().color = Color.cyan; // Icy visual cue
            }
        }
    }

    private void Heal()
    {
        currentHp = Mathf.Min(currentHp + hpValue, maxHp);
        UpdateHpBar();
    }

    private void IceTrap()
    {
        if (player != null)
        {
            Vector3 trapPosition = player.transform.position;
            GameObject trap = Instantiate(slowIcePrefabs, trapPosition, Quaternion.identity);
            trap.AddComponent<Trap>(); // Custom script for slow effect
            Destroy(trap, 5f); // Lasts 5 seconds
        }
    }

    protected virtual void UseSkillRandom()
    {
        int randomSkill = Random.Range(0, 4);
        switch (randomSkill)
        {
            case 0:
                Debug.Log("Boss is using skill: Normal Shoot");
                NormalShootBullet();
                break;
            case 1:
                Debug.Log("Boss is using skill: IceShower");
                IceShower();
                break;
            case 2:
                Debug.Log("Boss is using skill: Healing");
                Heal();
                break;
            case 3:
                Debug.Log("Boss is using skill: IceTrap");
                IceTrap();
                break;
        }
    }

    protected virtual void UseSkill()
    {
        nextSkillTime = Time.time + skillCooldown;
        UseSkillRandom();
    }
}
