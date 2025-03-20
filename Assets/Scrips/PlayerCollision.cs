using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private AudioManager audioManager;

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
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("FloatingParent");

            foreach (GameObject enemy in enemies)
            {
                Debug.Log("Found enemy: " + enemy.name);
                Destroy(enemy);
            }
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
            Debug.Log("Load next scene: " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã đến màn cuối cùng!");
        }
    }
}
