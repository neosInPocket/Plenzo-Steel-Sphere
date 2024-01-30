using System.Collections;
using Cinemachine;
using UnityEngine;

public class EventChanger : MonoBehaviour
{
	[SerializeField] private Ball ball;
	[SerializeField] private Spawner spawner;
	[SerializeField] private float directionChangeChance;
	[SerializeField] private CinemachineCameraOffset offset;
	[SerializeField] private float transitionSpeed;
	[SerializeField] private CountDown countDown;
	[SerializeField] private float slowMotionFactor;
	[SerializeField] private float waitTime;

	private void Start()
	{
		ball.OnCoinEntered += OnCoinCollected;
	}

	private void OnCoinCollected()
	{
		if (Random.Range(0, 1f) < directionChangeChance)
		{
			Physics2D.gravity *= -1;
			spawner.ChangeDirection();
			StartCoroutine(ChangeCameraOffset());
			StartCoroutine(SlowMotion());
			countDown.PopupGravity("GRAVITY CHANGE!");
			ball.ResetSpeed();
		}
	}

	private void OnDestroy()
	{
		ball.OnCoinEntered -= OnCoinCollected;
	}

	private IEnumerator ChangeCameraOffset()
	{
		var currentOffset = offset.m_Offset;
		var destination = offset.m_Offset.y * -1;
		int direction = destination < 0 ? -1 : 1;

		while ((currentOffset.y < destination && direction > 0) || (currentOffset.y > destination && direction < 0))
		{
			currentOffset.y += direction * transitionSpeed * Time.deltaTime;
			offset.m_Offset = currentOffset;
			yield return null;
		}

		currentOffset.y = destination;
		offset.m_Offset = currentOffset;
	}

	private IEnumerator SlowMotion()
	{
		Time.timeScale = slowMotionFactor;
		var scale = Time.fixedDeltaTime;
		Time.fixedDeltaTime = Time.timeScale * 0.01f;
		yield return new WaitForSeconds(2 * slowMotionFactor);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = scale;
	}
}
