using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private RotatingObject[] prefabs;
	[SerializeField] private RotatingObject initial;
	[SerializeField] private float spawnOffset;
	[SerializeField] private float pointer;
	private List<RotatingObject> currentObstacles;
	private RotatingObject lastObject;
	private bool downDirection;
	private bool changing;

	private void Start()
	{
		currentObstacles = new List<RotatingObject>();
		lastObject = initial;
		currentObstacles.Add(initial);
		downDirection = true;
		Physics2D.gravity = new Vector2(0, -9.81f);
	}

	private void Update()
	{
		if (changing) return;

		if (lastObject == null)
		{
			GetClosestLastObject();
		}


		if (downDirection)
		{
			if (player.position.y - pointer - spawnOffset < lastObject.transform.position.y)
			{
				Vector2 position = new Vector2(0, lastObject.transform.position.y - spawnOffset);
				var obstacle = Instantiate(prefabs[Random.Range(0, 3)], position, Quaternion.identity, transform);
				currentObstacles.Add(obstacle);
				lastObject = obstacle;
			}
		}
		else
		{
			if (player.position.y + pointer + spawnOffset > lastObject.transform.position.y)
			{
				if (lastObject == null)
				{
					GetClosestLastObject();
				}
				Vector2 position = new Vector2(0, lastObject.transform.position.y + spawnOffset);
				var obstacle = Instantiate(prefabs[Random.Range(0, 3)], position, Quaternion.identity, transform);
				currentObstacles.Add(obstacle);
				lastObject = obstacle;
			}
		}
	}

	public void GetClosestLastObject()
	{
		var raycast = Physics2D.CircleCastAll(player.transform.position, 100, Vector3.left);
		var obj = raycast.FirstOrDefault(x => x.collider.GetComponent<Obstacle>() != null);
		lastObject = obj.collider.GetComponentInParent<RotatingObject>();
	}

	public void ChangeDirection()
	{
		changing = true;

		currentObstacles.RemoveAll(x => x == null);
		downDirection = !downDirection;
		var raycast = Physics2D.CircleCastAll(player.transform.position, 10, Vector3.left);
		var obj = raycast.FirstOrDefault(x => x.collider.GetComponent<Obstacle>() != null);
		lastObject = obj.collider.GetComponentInParent<RotatingObject>();
		if (downDirection)
		{
			var down = currentObstacles.Where(x => x.transform.position.y < player.transform.position.y - spawnOffset / 4);
			foreach (var obstacle in down)
			{
				Destroy(obstacle.gameObject);
			}
		}
		else
		{
			var upper = currentObstacles.Where(x => x.transform.position.y > player.transform.position.y + spawnOffset / 4);
			foreach (var obstacle in upper)
			{
				Destroy(obstacle.gameObject);
			}
		}
		changing = false;
	}

	public void Freeze(bool freeze)
	{
		currentObstacles.ForEach(x => x.stopped = freeze);
	}
}
