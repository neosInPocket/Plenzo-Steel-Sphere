using UnityEngine;

public class RotatingObject : MonoBehaviour
{
	[SerializeField] private Vector2 rotationSpeeds;
	[SerializeField] private Transform coin;
	private float rotSpeed;
	public bool stopped { get; set; }

	private void Start()
	{
		rotSpeed = Random.Range(rotationSpeeds.x, rotationSpeeds.y);
	}

	private void Update()
	{
		if (stopped) return;

		var euler = transform.eulerAngles;
		euler.z += rotSpeed * Time.deltaTime;
		transform.eulerAngles = euler;

		if (coin == null) return;
		var coinEuler = coin.eulerAngles;
		coinEuler.z -= rotSpeed * Time.deltaTime;
		coin.eulerAngles = coinEuler;

	}
}
