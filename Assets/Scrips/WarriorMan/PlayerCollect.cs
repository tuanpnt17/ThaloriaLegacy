using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefabs;

    [SerializeField]
    private GameObject hpPrefabs;

    [SerializeField]
    private GameObject mpPrefabs;

    [SerializeField]
    private float blockPeriod = 10f;

    [SerializeField]
    private float hpBonusAmount = 50f;

    [SerializeField]
    private float mpBonusAmount = 30f;

    private Player playerScript;
    private GameObject activeShield;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
            GameObject shield = Instantiate(blockPrefabs, transform.position, Quaternion.identity);
            shield.transform.SetParent(transform);
            activeShield = shield;
            playerScript.SetBlockDamage(true);
            Invoke("DeactivateShield", blockPeriod);
        }
        else if (collision.CompareTag("HPBonus"))
        {
            Destroy(collision.gameObject);
            GameObject hp = Instantiate(hpPrefabs, transform.position, Quaternion.identity);
            hp.transform.SetParent(transform);
            Destroy(hp, 1f);
            playerScript.Heal(hpBonusAmount);
        }
        else if (collision.CompareTag("MPBonus"))
        {
            Destroy(collision.gameObject);
            GameObject mp = Instantiate(mpPrefabs, transform.position, Quaternion.identity);
            mp.transform.SetParent(transform);
            Destroy(mp, 1f);
            playerScript.ChangeMp(mpBonusAmount);
        }
    }

    private void DeactivateShield()
    {
        playerScript.SetBlockDamage(false);
        Destroy(activeShield);
    }
}
