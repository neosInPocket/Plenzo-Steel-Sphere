using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	private void Awake()
	{
		var all = GameObject.FindObjectsOfType<MusicManager>();
		var avaliable = all.FirstOrDefault(x => x.gameObject.scene.name == "DontDestroyOnLoad");

		if (avaliable != null && avaliable != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		audioSource.enabled = SettingsManager.Settings.musicEnabled;
	}

	public bool Toggle()
	{
		audioSource.enabled = !audioSource.enabled;
		SettingsManager.Settings.musicEnabled = audioSource.enabled;
		SettingsManager.SetData();
		return audioSource.enabled;
	}
}
