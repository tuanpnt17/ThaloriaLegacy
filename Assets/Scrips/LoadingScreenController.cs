using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public float delayTime = 3f; // Thời gian load màn hình

    void Start()
    {
        StartCoroutine(LoadMenuAfterDelay());
    }

    IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayTime); // Đợi 3 giây
        SceneManager.LoadScene("AuthScene"); // Chuyển sang Menu Scene
    }
}
