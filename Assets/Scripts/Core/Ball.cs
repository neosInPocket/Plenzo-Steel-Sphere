using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Ball : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Sprite[] skins;
	[SerializeField] private GameObject deathEffect;
	[SerializeField] private float force;

	public Action OnCoinEntered { get; set; }
	public Action OnObstacleEntered { get; set; }

	public bool Enabled
	{
		get => m_enabled;
		set
		{
			m_enabled = value;
			if (value)
			{
				Touch.onFingerDown += OnFingerDown;
			}
			else
			{
				Touch.onFingerDown -= OnFingerDown;
			}
		}
	}

	public bool Freezed
	{
		get => m_freezed;
		set
		{
			m_freezed = value;
			if (value)
			{
				rigid.constraints = RigidbodyConstraints2D.None;
			}
			else
			{
				rigid.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
	}

	private bool m_enabled;
	private bool m_freezed;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		spriteRenderer.sprite = skins[SettingsManager.Settings.skinIndex];
	}

	private void OnFingerDown(Finger finger)
	{
		rigid.AddForce(-Physics2D.gravity.normalized * force);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			OnCoinEntered?.Invoke();
			return;
		}

		if (collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
		{
			OnObstacleEntered?.Invoke();
			StartCoroutine(Effect());
			Freezed = true;
			Enabled = true;
			return;
		}
	}

	private IEnumerator Effect()
	{
		spriteRenderer.enabled = false;
		trailRenderer.time = 0.1f;

		deathEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		deathEffect.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}
