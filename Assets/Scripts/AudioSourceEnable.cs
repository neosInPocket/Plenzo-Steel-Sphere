using UnityEngine;

public class AudioSourceEnable : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Start()
	{
		audioSource.enabled = SettingsManager.Settings.sfxEnabled;
	}
}
