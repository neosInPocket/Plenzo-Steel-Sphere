using TMPro;
using UnityEngine;

public class EndShow : MonoBehaviour
{
	[SerializeField] private TMP_Text result;
	[SerializeField] private TMP_Text diamondsText;
	[SerializeField] private TMP_Text button;

	public void Show(bool win, int diamonds = 0)
	{
		gameObject.SetActive(true);

		if (win)
		{
			result.text = "YOU WIN!!";
			button.text = "CONTINUE";
		}
		else
		{
			result.text = "YOU LOSE...";
			button.text = "TRY AGAIN";
		}

		diamondsText.text = diamonds.ToString();
	}
}
