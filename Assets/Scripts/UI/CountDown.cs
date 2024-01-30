using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CountDown : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	[SerializeField] private TMP_Text gravityText;
	public Action CountEnd;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void OnFingerDown(Finger finger)
	{
		Touch.onFingerDown -= OnFingerDown;
		text.gameObject.SetActive(false);
		CountEnd?.Invoke();
	}

	public void Enable()
	{
		text.gameObject.SetActive(true);
		Touch.onFingerDown += OnFingerDown;
	}

	public void PopupGravity(string caption)
	{
		gravityText.text = caption;
		gravityText.gameObject.SetActive(true);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}
