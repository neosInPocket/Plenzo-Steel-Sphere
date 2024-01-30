using UnityEngine;

public class PauseManager : MonoBehaviour
{
	[SerializeField] private Spawner spawner;
	[SerializeField] private Ball ball;
	[SerializeField] private CountDown countDown;

	public void Pause()
	{
		gameObject.SetActive(true);
		spawner.Freeze(true);
		ball.Enabled = false;
		ball.Freezed = true;
	}

	public void UnPause()
	{
		gameObject.SetActive(false);
		countDown.Enable();
		countDown.CountEnd += OnCountEnd;
	}

	private void OnCountEnd()
	{
		countDown.CountEnd -= OnCountEnd;
		ball.Enabled = true;
		ball.Freezed = false;
		spawner.Freeze(false);
		ball.AddForce();
	}
}
