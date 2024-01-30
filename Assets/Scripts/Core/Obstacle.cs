using UnityEngine;

public class Obstacle : MonoBehaviour
{


	public bool Freezed
	{
		get => m_freezed;
		set
		{
			m_freezed = value;
		}
	}

	private bool m_freezed;


}
