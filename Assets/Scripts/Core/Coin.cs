using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject explosionEffect;
	[SerializeField] private GameObject glowEffect;
	public bool collected { get; set; }

	public void Collect()
	{
		if (collected) return;
		collected = true;
		StartCoroutine(CollectRoutine());
	}

	private IEnumerator CollectRoutine()
	{
		spriteRenderer.enabled = false;
		glowEffect.SetActive(false);
		explosionEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
