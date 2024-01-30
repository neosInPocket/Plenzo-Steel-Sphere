using System.Collections;
using UnityEngine;

public class PanelsTransition : MonoBehaviour
{
	[SerializeField] private AnimationCurve fadeIn;
	[SerializeField] private float speed;
	[SerializeField] private GameObject startScreen;
	private GameObject currentScreen;

	private void Start()
	{
		currentScreen = startScreen;
	}

	public void Transition(GameObject to)
	{
		StopAllCoroutines();
		StartCoroutine(FadeInTransition(to));
	}

	private IEnumerator FadeInTransition(GameObject destination)
	{
		Vector3 currentScale = currentScreen.transform.localScale;
		float time = 1;

		while (time > 0)
		{
			time -= speed * Time.deltaTime;

			currentScale.x = fadeIn.Evaluate(time);
			currentScale.y = currentScale.x;
			currentScreen.transform.localScale = currentScale;
			yield return null;
		}

		time = 0;
		currentScale.x = fadeIn.Evaluate(time);
		currentScale.y = currentScale.x;
		currentScreen.transform.localScale = currentScale;

		currentScreen.SetActive(false);
		currentScreen = destination;
		StartCoroutine(FadeOutTransition());
	}


	private IEnumerator FadeOutTransition()
	{
		currentScreen.SetActive(true);

		Vector3 currentScale = currentScreen.transform.localScale;
		float time = 0;

		while (time < 1)
		{
			time += speed * Time.deltaTime;
			Debug.Log(time);
			currentScale.x = fadeIn.Evaluate(time);
			currentScale.y = currentScale.x;
			currentScreen.transform.localScale = currentScale;
			yield return null;
		}

		time = 1;
		currentScale.x = fadeIn.Evaluate(time);
		currentScale.y = currentScale.x;
		currentScreen.transform.localScale = currentScale;
	}
}
