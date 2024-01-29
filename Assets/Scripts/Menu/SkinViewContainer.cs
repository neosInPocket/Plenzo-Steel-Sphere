using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UI;
using TMPro;

public class SkinViewContainer : MonoBehaviour
{
	[SerializeField] private StoreManager storeManager;
	[SerializeField] private Image left;
	[SerializeField] private Image middle;
	[SerializeField] private Image right;
	[SerializeField] private float sharpFactor;
	[SerializeField] private ScrollRect scrollRect;
	[SerializeField] private float clampSpeed;
	[SerializeField] private string[] names;
	[SerializeField] private int[] prices;
	[SerializeField] private TMP_Text skinName;
	[SerializeField] private TMP_Text price;
	[SerializeField] private TMP_Text buttonText;
	[SerializeField] private Button button;
	[SerializeField] private GameObject notEnoughMoneyText;
	[SerializeField] private GameObject priceContainer;

	private void Start()
	{
		RefreshImages(0.5f);
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerUp += OnFingerUp;
		Touch.onFingerDown += OnFingerDown;
		skinName.text = names[1];
		price.text = prices[1].ToString();
		CheckButton(1);
	}

	public void OnValueChanged(Vector2 value)
	{
		RefreshImages(value.x);
		LoadSkinData();
	}

	private void OnFingerDown(Finger finger)
	{
		StopAllCoroutines();
	}

	private void RefreshImages(float xValue)
	{
		var leftColor = left.color;
		leftColor.a = NormalDistribution(xValue, 0);
		left.color = leftColor;

		var rightColor = right.color;
		rightColor.a = NormalDistribution(xValue, -1f);
		right.color = rightColor;

		var middleColor = middle.color;
		middleColor.a = NormalDistribution(xValue, -0.5f);
		middle.color = middleColor;
	}

	private float NormalDistribution(float xValue, float offset)
	{
		var result = Mathf.Exp(-Mathf.Pow(xValue + offset, 2) / sharpFactor);
		return result;
	}

	private void OnFingerUp(Finger finger)
	{
		StopAllCoroutines();

		if (scrollRect.horizontalNormalizedPosition > 0 && scrollRect.horizontalNormalizedPosition < 0.25f)
		{
			StartCoroutine(ClampToWindow(-1, 0));
			return;
		}

		if (scrollRect.horizontalNormalizedPosition > 0.25f && scrollRect.horizontalNormalizedPosition < 0.5f)
		{
			StartCoroutine(ClampToWindow(1, 0.5f));
			return;
		}

		if (scrollRect.horizontalNormalizedPosition > 0.75f && scrollRect.horizontalNormalizedPosition < 1f)
		{
			StartCoroutine(ClampToWindow(1, 1f));
			return;
		}

		if (scrollRect.horizontalNormalizedPosition > 0.5f && scrollRect.horizontalNormalizedPosition < 0.75f)
		{
			StartCoroutine(ClampToWindow(-1, 0.5f));
			return;
		}
	}

	private void LoadSkinData()
	{
		var currentPosition = scrollRect.normalizedPosition;

		if (currentPosition.x < 0.01f)
		{
			skinName.text = names[0];
			price.text = prices[0].ToString();
			CheckButton(0);
		}

		if (currentPosition.x > 0.49f || currentPosition.x > 0.51f)
		{
			skinName.text = names[1];
			price.text = prices[1].ToString();
			CheckButton(1);
		}

		if (currentPosition.x > 0.99f)
		{
			skinName.text = names[2];
			price.text = prices[2].ToString();
			CheckButton(2);
		}
	}

	private void CheckButton(int value)
	{

		if (SettingsManager.Settings.skinBought[value])
		{
			if (SettingsManager.Settings.skinIndex == value)
			{
				buttonText.text = "SELECTED";
				button.interactable = false;
				notEnoughMoneyText.SetActive(false);
				priceContainer.SetActive(false);
			}
			else
			{
				buttonText.text = "SELECT";
				button.interactable = true;
				notEnoughMoneyText.SetActive(false);
				priceContainer.SetActive(false);
			}
		}
		else
		{
			if (SettingsManager.Settings.diamonds >= prices[value])
			{
				buttonText.text = "BUY";
				button.interactable = true;
				notEnoughMoneyText.SetActive(false);
				priceContainer.SetActive(true);
			}
			else
			{
				buttonText.text = "BUY";
				notEnoughMoneyText.SetActive(true);
				button.interactable = false;
				priceContainer.SetActive(true);
			}
		}
	}

	private IEnumerator ClampToWindow(int direction, float destination)
	{
		Vector2 currentPosition = scrollRect.normalizedPosition;

		while ((currentPosition.x < destination && direction > 0) || (currentPosition.x > destination && direction < 0))
		{
			currentPosition.x += direction * clampSpeed;
			scrollRect.normalizedPosition = currentPosition;
			yield return null;
		}

		currentPosition.x = destination;
		scrollRect.normalizedPosition = currentPosition;

		LoadSkinData();
	}

	public void BuySkin()
	{
		var currentPosition = scrollRect.normalizedPosition;

		if (currentPosition.x < 0.01f)
		{
			if (SettingsManager.Settings.skinBought[0])
			{
				SettingsManager.Settings.skinIndex = 0;
				CheckButton(0);
				return;
			}

			SettingsManager.Settings.skinBought[0] = true;
			SettingsManager.Settings.skinIndex = 0;
			SettingsManager.Settings.diamonds -= prices[0];
			SettingsManager.SetData();
			CheckButton(0);
			storeManager.Refresh();
		}

		if (currentPosition.x > 0.49f || currentPosition.x > 0.51f)
		{
			if (SettingsManager.Settings.skinBought[1])
			{
				SettingsManager.Settings.skinIndex = 1;
				CheckButton(1);
				return;
			}

			SettingsManager.Settings.skinBought[1] = true;
			SettingsManager.Settings.skinIndex = 1;
			SettingsManager.Settings.diamonds -= prices[1];
			SettingsManager.SetData();
			CheckButton(1);
			storeManager.Refresh();
		}

		if (currentPosition.x > 0.99f)
		{
			if (SettingsManager.Settings.skinBought[2])
			{
				SettingsManager.Settings.skinIndex = 2;
				CheckButton(2);
				return;
			}

			SettingsManager.Settings.skinBought[2] = true;
			SettingsManager.Settings.skinIndex = 2;
			SettingsManager.Settings.diamonds -= prices[2];
			SettingsManager.SetData();
			CheckButton(2);
			storeManager.Refresh();
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerUp -= OnFingerUp;
		Touch.onFingerDown -= OnFingerDown;
	}
}
