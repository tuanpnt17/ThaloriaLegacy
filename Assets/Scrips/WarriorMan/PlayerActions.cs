using UnityEngine;
using UnityEngine.Video;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private float mpCost = 20f; // Cost of using the ability

    [SerializeField]
    private GameObject cursor; // The cursor GameObject (should be a child of the player)

    [SerializeField]
    private float cursorRadius = 1f; // Fixed radius for the cursor around the player

    [SerializeField]
    private GameObject magicPrefab;

    [SerializeField]
    private Transform firePos;

    [SerializeField]
    private Transform fireMagicPos;

    [SerializeField]
    private GameObject slashEffect;

	private AudioManager audioManager;

	private Vector3 mousePosition;
    private int numberOfMeleeAtk = 3;

    private int currentAttackCounter = 1;
    private Animator animator;
    private Player playerScript;
    private bool isAttacking = false;

    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        animator = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        UpdateCursorPositionAndRotation();
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            audioManager.PlaySwordSound();
            MeleeAttack(mousePosition);
        }
        if (Input.GetMouseButtonDown(1) && !isAttacking)
        {
            audioManager.PlayPowerBallSound();
            RangeAttack(mousePosition);
        }
    }

    public void SlashEffect()
    {
        GameObject effect = Instantiate(slashEffect, firePos.position, cursor.transform.rotation);
        Destroy(effect, 0.5f);
    }

    public void AttackDone()
    {
        playerScript.allowFlip = true;
        isAttacking = false;
        //Debug.Log("Attack done");
    }

    public void LaunchMagic()
    {
        Instantiate(magicPrefab, firePos.position, cursor.transform.rotation);
        playerScript.ChangeMp(-mpCost);
    }

    private void UpdateCursorPositionAndRotation()
    {
        // Get the mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the cursor stays in 2D (no Z movement)

        // Calculate direction from the player to the mouse position
        Vector3 directionToMouse = mousePosition - transform.position;

        // Normalize the direction
        directionToMouse.Normalize();

        // Calculate the desired position for the cursor, limited by the radius
        Vector3 desiredPosition = transform.position + directionToMouse * cursorRadius;

        // Set the cursor position to the calculated position (within radius)
        cursor.transform.position = desiredPosition;

        // Calculate the angle between the cursor and the player
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Rotate the cursor to always face the mouse direction (relative to the player)
        // Ensure that the cursor's front points at the mouse position
        cursor.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void MeleeAttack(Vector3 position)
    {
        currentAttackCounter++;
        if (currentAttackCounter > numberOfMeleeAtk)
        {
            currentAttackCounter = 1;
        }

        playerScript.allowFlip = false;
        isAttacking = true;
        Flip(position);
        animator.SetInteger("AttackType", currentAttackCounter);
        animator.SetTrigger("Attack");
    }

    private void RangeAttack(Vector3 position)
    {
        if (playerScript.GetMp() < mpCost)
            return;
        playerScript.allowFlip = false;
        isAttacking = true;
        Flip(position);
        animator.SetInteger("AttackType", 0);
        animator.SetTrigger("Attack");
    }

    private void Flip(Vector3 position)
    {
        transform.localScale = new Vector3(position.x < transform.position.x ? -1 : 1, 1, 1);
    }
}
