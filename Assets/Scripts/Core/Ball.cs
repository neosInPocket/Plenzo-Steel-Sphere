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
				rigid.constraints = RigidbodyConstraints2D.FreezeAll;
			}
			else
			{
				rigid.constraints = RigidbodyConstraints2D.None;
			}
		}
	}

	private bool m_enabled;
	private bool m_freezed;
	private bool m_dead;

	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		spriteRenderer.sprite = skins[SettingsManager.Settings.skinIndex];
	}

	private void OnFingerDown(Finger finger)
	{
		AddForce();
	}

	public void AddForce()
	{
		rigid.velocity = Vector2.zero;
		rigid.AddForce(-Physics2D.gravity.normalized * force, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			if (coin.collected) return;
			OnCoinEntered?.Invoke();
			coin.Collect();
			return;
		}

		if (collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
		{
			if (m_dead) return;
			m_dead = true;
			OnObstacleEntered?.Invoke();
			StartCoroutine(Effect());
			Freezed = true;
			Enabled = false;
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

	public void ResetSpeed()
	{
		rigid.velocity = Vector2.zero;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnFingerDown;
	}
}
