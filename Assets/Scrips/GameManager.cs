using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    protected int currentEnergy;

    [SerializeField]
    protected int energyThreshold = 3;

    [SerializeField]
    protected GameObject boss;

    [SerializeField]
    protected GameObject enemySpawner;
    protected bool bossCalled = false;

    [SerializeField]
    protected Image energyBar;

    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    protected AudioManager audioManager;

    protected virtual void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        if (audioManager != null)
        {
            audioManager.StopAudioGame();
        }
    }

    public void AddEnergy()
    {
        if (bossCalled)
            return;
        currentEnergy += 1;
        UpdateEnergyBar();
        if (currentEnergy == energyThreshold)
        {
            CallBoss();
        }
    }

    public void CallBoss()
    {
        bossCalled = true;
        boss.SetActive(true);
        enemySpawner.SetActive(false);
        if (audioManager != null)
        {
            audioManager.PlayBossAudio();
        }
    }

    private void UpdateEnergyBar()
    {
        if (energyBar != null)
        {
            float fillAmount = Mathf.Clamp01((float)currentEnergy / (float)energyThreshold);
            energyBar.fillAmount = fillAmount;
        }
    }
}
