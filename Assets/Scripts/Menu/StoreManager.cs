using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
	[SerializeField] private TMP_Text diamondsAmount;
	[SerializeField] private Image skin;
	[SerializeField] private Sprite[] skins;
	public int currentIndex { get; set; }

	private void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		diamondsAmount.text = SettingsManager.Settings.diamonds.ToString();
	}

	public void BuySkinUpgrade()
	{

	}
}
