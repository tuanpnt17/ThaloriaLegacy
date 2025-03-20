using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
		string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
		audioManager.PlayMap2Audio();
		switch (sceneName)
        {
            case "MenuScene":
                audioManager.PlayMainMenuAudio();
                break;
            case "Map1":
                Debug.Log("Hello");
				audioManager.PlayMap1Audio();
				break;
			case "Map2":
				audioManager.PlayMap2Audio();
				break;
			case "Map3":
				audioManager.PlayMap3Audio();
				break;
			case "Map4":
				audioManager.PlayMap4Audio();
				break;
			case "MapEnd":
				audioManager.PlayMapEndAudio();
				break;
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
        gameUI.SetActive(false);
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
