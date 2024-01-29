using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
	[SerializeField] private Image music;
	[SerializeField] private Image sfx;
	[SerializeField] private Color disabledColor;
	private MusicManager currentManager;

	private void Start()
	{
		currentManager = GameObject.FindFirstObjectByType<MusicManager>();
		bool musicEnabled = SettingsManager.Settings.musicEnabled;
		bool sfxEnabled = SettingsManager.Settings.sfxEnabled;

		if (musicEnabled)
		{
			music.color = Color.white;
		}
		else
		{
			music.color = disabledColor;
		}

		if (sfxEnabled)
		{
			sfx.color = Color.white;
		}
		else
		{
			sfx.color = disabledColor;
		}
	}

	public void ToggleMusic()
	{
		bool enabled = currentManager.Toggle();
		if (enabled)
		{
			music.color = Color.white;
		}
		else
		{
			music.color = disabledColor;
		}
	}

	public void ToggleSFX()
	{
		if (sfx.color == Color.white)
		{
			sfx.color = disabledColor;
			SettingsManager.Settings.sfxEnabled = false;
		}
		else
		{
			sfx.color = Color.white;
			SettingsManager.Settings.sfxEnabled = true;
		}

		SettingsManager.SetData();
	}
}
