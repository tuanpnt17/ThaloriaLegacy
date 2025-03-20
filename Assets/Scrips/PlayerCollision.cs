using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    private AudioManager audioManager;

	private void Awake()
	{
		audioManager = FindAnyObjectByType<AudioManager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(10f);
        }
        else if (collision.CompareTag("Usb")) // collect USB -> next level
        {
            Debug.Log("Win Game");
            Destroy(collision.gameObject);
            LoadNextScene();
        }
        else if (collision.CompareTag("Energy")) // collect energy -> enough will call boss
        {
            if (gameManager != null)
            {
                gameManager.AddEnergy();
            }
            else
            {
                Debug.LogError("GameManager chưa được gán trong PlayerCollision!");
            }

            Destroy(collision.gameObject);
            audioManager.PlayEnergySound();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã đến màn cuối cùng!");
        }
    }
}
