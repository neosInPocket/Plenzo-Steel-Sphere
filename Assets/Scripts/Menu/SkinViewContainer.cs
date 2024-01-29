using System.Collections;
using UnityEngine;

public class SkinViewContainer : MonoBehaviour
{
	[SerializeField] private StoreManager storeManager;

	public void OnValueChanged()
	{

	}

	private IEnumerator ClampToWindow()
	{
		yield return null;
	}
}
