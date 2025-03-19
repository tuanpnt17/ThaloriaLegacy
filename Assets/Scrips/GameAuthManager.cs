using UnityEngine;

public class GameAuthManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject login;

    [SerializeField]
    protected GameObject register;

    [SerializeField]
    protected GameObject authMenu;

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        login.SetActive(false);
        register.SetActive(false);
        authMenu.SetActive(true);
    }

    public void Login()
    {
        login.SetActive(true);
        authMenu.SetActive(false);
        register.SetActive(false);
    }

    public void Register()
    {
        login.SetActive(false);
        authMenu.SetActive(false);
        register.SetActive(true);
    }

    public void Back()
    {
        login.SetActive(false);
        authMenu.SetActive(true);
        register.SetActive(false);
    }
}
