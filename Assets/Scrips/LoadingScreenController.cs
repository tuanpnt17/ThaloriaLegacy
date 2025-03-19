using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
        SceneManager.LoadScene("Introduction"); // Chuyển sang Menu Scene
    }
}
