using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorialShow : MonoBehaviour
{
	[SerializeField] private TMP_Text characterText;
	[SerializeField] private GameObject arrow;
	[SerializeField] private Button pauseButton;
	private Action End;
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void Show(Action end)
	{
		pauseButton.interactable = false;
		End = end;
		gameObject.SetActive(true);
		Touch.onFingerDown += Next;
		characterText.text = "WELCOME TO PLINKO!";
	}

	private void Next(Finger finger)
	{
		Touch.onFingerDown -= Next;
		Touch.onFingerDown += Next1;
		characterText.text = "YOUR GOAL IS TO COMPLETE LEVELS AVOIDING OBSTACLES IN YOUR PATH";
	}

	private void Next1(Finger finger)
	{
		Touch.onFingerDown -= Next1;
		Touch.onFingerDown += Next2;
		characterText.text = "BY COLLECTING COINS, YOUR PROGRESS BAR IS FILLING! FILL IT COMPLETELY TO PASS THE LEVEL";
		arrow.SetActive(true);
	}

	private void Next2(Finger finger)
	{
		Touch.onFingerDown -= Next2;
		Touch.onFingerDown += Next3;
		arrow.SetActive(false);
		characterText.text = "BE CAREFUL! GRAVITY CAN CHANGE AT ANY MOMENT";
	}

	private void Next3(Finger finger)
	{
		Touch.onFingerDown -= Next3;
		Touch.onFingerDown += Next4;
		characterText.text = "GOOD LUCK!";
	}

	private void Next4(Finger finger)
	{
		Touch.onFingerDown -= Next4;
		End();
		gameObject.SetActive(false);
		pauseButton.interactable = true;
	}
}
