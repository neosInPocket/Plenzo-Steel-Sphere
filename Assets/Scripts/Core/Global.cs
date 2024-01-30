
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
	[SerializeField] private Ball ball;
	[SerializeField] private CountDown countDown;
	//[SerializeField] private PauseController pauseController;
	[SerializeField] private Image progressImage;
	[SerializeField] private int scorePerCoin;
	[SerializeField] private EndShow endShow;
	[SerializeField] private TMP_Text level;
	[SerializeField] private TutorialShow tutorialShow;
	private int score = 0;
	private int levelScore => (int)(10 * Mathf.Log(Mathf.Sqrt(SettingsManager.Settings.score) + 2));
	private int coinsAfter => (int)(10 * Mathf.Log(Mathf.Pow(SettingsManager.Settings.score, 2) + 2) + 12);

	private void Start()
	{
		level.text = "LEVEL " + SettingsManager.Settings.score.ToString();
		ball.OnCoinEntered += OnCoinEntered;
		ball.OnObstacleEntered += ObstacleEntered;
		progressImage.fillAmount = 0f;
		ball.Enabled = false;
		ball.Freezed = true;
		countDown.CountEnd += OnCountEnd;

		if (SettingsManager.Settings.firstGamePlaying)
		{
			SettingsManager.Settings.firstGamePlaying = false;
			SettingsManager.SetData();
			tutorialShow.Show(OnTutorialCompleted);
		}
		else
		{
			OnTutorialCompleted();
		}
	}

	private void OnTutorialCompleted()
	{
		countDown.Enable();
		countDown.CountEnd += OnCountEnd;
	}

	private void OnCountEnd()
	{
		countDown.CountEnd -= OnCountEnd;
		ball.Enabled = true;
		ball.Freezed = false;
		ball.AddForce();
	}

	private void OnCoinEntered()
	{
		score += scorePerCoin;
		CheckCurrentScore();
	}

	private void CheckCurrentScore()
	{
		if (score >= levelScore)
		{
			score = levelScore;
			Win();
		}

		progressImage.fillAmount = (float)score / (float)levelScore;
	}

	private void Win()
	{
		endShow.Show(true, coinsAfter);
		SettingsManager.Settings.score++;
		SettingsManager.Settings.diamonds += coinsAfter;
		SettingsManager.SetData();

		ball.Enabled = false;
		ball.Freezed = true;
	}

	private void Lose()
	{
		endShow.Show(false);
		ball.Enabled = false;
		ball.Freezed = true;
	}

	private void ObstacleEntered()
	{
		ball.OnCoinEntered -= OnCoinEntered;
		ball.OnObstacleEntered -= ObstacleEntered;
		Lose();
	}

	private void OnDestroy()
	{
		countDown.CountEnd -= OnCountEnd;
		ball.OnCoinEntered -= OnCoinEntered;
		ball.OnObstacleEntered -= ObstacleEntered;
	}

	public void TryAgain()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void Menu()
	{

		SceneManager.LoadScene("MainMenuScene");
	}
}
