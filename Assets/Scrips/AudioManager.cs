using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private AudioSource effectAudioSource;
	[SerializeField] private AudioSource defaultAudioSource;
	[SerializeField] private AudioSource runningAudioSource;
	[SerializeField] private AudioSource swordAudioSource;
	[SerializeField] private AudioSource powerBallAudioSource;
	[SerializeField] private AudioSource playerHurtAudioSource;
	[SerializeField] private AudioSource bossAudioSource;
	[SerializeField] private AudioSource enemyAudioSource;
	[SerializeField] private AudioClip shootClip;
	[SerializeField] private AudioClip reLoadClip;
	[SerializeField] private AudioClip enegyClip;
	[SerializeField] private AudioClip mainMenuClip;
	[SerializeField] private AudioClip map1Clip;
	[SerializeField] private AudioClip map2Clip;
	[SerializeField] private AudioClip map3Clip;
	[SerializeField] private AudioClip map4Clip;
	[SerializeField] private AudioClip mapEndClip;
	[SerializeField] private AudioClip runningClip;
	[SerializeField] private AudioClip swordClip;
	[SerializeField] private AudioClip explosionClip;
	[SerializeField] private AudioClip playerDeathClip;
	[SerializeField] private AudioClip gameOverClip;
	[SerializeField] private AudioClip enemyDeathClip;
	[SerializeField] private AudioClip enemyClip1;
	[SerializeField] private AudioClip enemyClip2;
	[SerializeField] private AudioClip enemyClip3;
	[SerializeField] private AudioClip enemyWizardClip;
	[SerializeField] private AudioClip enemyArcherClip;
	[SerializeField] private AudioClip enemySlashClip;
	[SerializeField] private AudioClip enemyAxeClip;
	[SerializeField] private AudioClip enemyExplosionClip;
	[SerializeField] private AudioClip dragonBossClip;
	[SerializeField] private AudioClip dragonBossAttackClip;
	[SerializeField] private AudioClip roboticBossClip;
	[SerializeField] private AudioClip golemBossClip;
	[SerializeField] private AudioClip pharaohBossClip;
	[SerializeField] private AudioClip pharaohBossAttackClip;
	[SerializeField] private AudioClip frostBossClip;
	[SerializeField] private AudioClip frostBossAttackClip;


	public bool isRunning = false;  // Track running state

	public void PlayShootSound()
	{
		effectAudioSource.PlayOneShot(shootClip);
	}
	public void PlayReLoadSound()
	{
		effectAudioSource.PlayOneShot(reLoadClip);
	}
	public void PlayEnergySound()
	{
		effectAudioSource.PlayOneShot(enegyClip);
	}
	public void PlayRunningAudio()
	{
		if (!isRunning)
		{
			isRunning = true;
			runningAudioSource.clip = runningClip;
			runningAudioSource.Play();
		}
	}

	public void StopRunningAudio()
	{
		if (isRunning)
		{
			isRunning = false;
			runningAudioSource.Stop();
		}
	}
	public void PlaySwordSound()
	{
		swordAudioSource.PlayOneShot(swordClip);
	}

	public void PlayPowerBallSound()
	{
		powerBallAudioSource.PlayOneShot(explosionClip);
	}


	public void PlayPlayerDeathSound()
	{
		playerHurtAudioSource.PlayOneShot(playerDeathClip);
	}


	public void PlayEnemySound()
	{
		int randomIndex = Random.Range(1, 3); // Generate a random number between 0 and 2
		AudioClip selectedClip = null;

		switch (randomIndex)
		{
			case 1:
				selectedClip = enemyClip1;
				break;
			case 2:
				selectedClip = enemyClip2;
				break;
			case 3:
				selectedClip = enemyClip3;
				break;
		}

		if (selectedClip != null)
		{
			enemyAudioSource.PlayOneShot(selectedClip);
		}
	}

	public void PlayEnemyWizardSound()
	{
		enemyAudioSource.PlayOneShot(enemyWizardClip);
	}
	public void PlayEnemyArcherSound()
	{
		enemyAudioSource.PlayOneShot(enemyArcherClip);
	}

	public void PlayEnemySlashSound()
	{
		enemyAudioSource.PlayOneShot(enemySlashClip);
	}

	public void PlayEnemyAxeSound()
	{
		enemyAudioSource.PlayOneShot(enemyAxeClip);
	}
	public void PlayEnemyExplosionSound()
	{
		enemyAudioSource.PlayOneShot(enemyExplosionClip);
	}

	public void PlayEnemyDeathSound()
	{
		enemyAudioSource.PlayOneShot(enemyDeathClip);
	}

	public void PlayDragonBossAudio()
	{
		bossAudioSource.clip = dragonBossClip;
		bossAudioSource.Play();
	}

	public void PlayDragonBossAttackSound()
	{
		enemyAudioSource.PlayOneShot(dragonBossAttackClip);
	}

	public void PlayRoboticBossAudio()
	{
		bossAudioSource.clip = roboticBossClip;
		bossAudioSource.Play();
	}
	public void PlayGolemBossAudio()
	{
		bossAudioSource.clip = golemBossClip;
		bossAudioSource.Play();
	}

	public void PlayPharaohBossAudio()
	{
		bossAudioSource.clip = pharaohBossClip;
		bossAudioSource.Play();
	}

	public void PlayPharaohBossAttackSound()
	{
		enemyAudioSource.PlayOneShot(pharaohBossAttackClip);
	}

	public void PlayFrostBossAudio()
	{
		bossAudioSource.clip = frostBossClip;
		bossAudioSource.Play();
	}

	public void PlayFrostBossAttackSound()
	{
		enemyAudioSource.PlayOneShot(frostBossAttackClip);
	}

	public void PlayMainMenuAudio()
	{
	}

	public void PlayMap1Audio()
	{
		defaultAudioSource.clip = map1Clip;
		defaultAudioSource.Play();
	}
	public void PlayMap2Audio()
	{
		defaultAudioSource.clip = map2Clip;
		defaultAudioSource.Play();
	}
	public void PlayMap3Audio()
	{
		defaultAudioSource.clip = map3Clip;
		defaultAudioSource.Play();
	}
	public void PlayMap4Audio()
	{
		defaultAudioSource.clip = map4Clip;
		defaultAudioSource.Play();
	}
	public void PlayMapEndAudio()
	{
		defaultAudioSource.clip = mapEndClip;
		defaultAudioSource.Play();
	}

	public void StopAudioGame()
	{
		defaultAudioSource.Stop();
		effectAudioSource.Stop();
	}

	public void StopBossAudio()
	{
		bossAudioSource.Stop();
	}

	public void PlayGameOverSound()
	{
		defaultAudioSource.PlayOneShot(gameOverClip);
	}

}
