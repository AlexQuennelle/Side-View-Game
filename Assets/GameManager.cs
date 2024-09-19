using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//references to the text objects the display score in the game and on the end screen
	[SerializeField] TextMeshProUGUI gameTxt, endTxt;

	[SerializeField] int maxSeconds = 60;
	[SerializeField] TextMeshProUGUI timerTxt;
	[SerializeField] UnityEvent OnGameEnd = new UnityEvent();
	bool runTimer;
	float endTime;

	void Update()
	{
		if (!runTimer) return;

		float timeRemaining = endTime - Time.time;
		timerTxt.text = Mathf.FloorToInt(timeRemaining).ToString();
		if (timeRemaining <= 0)
		{
			runTimer = false;
			timerTxt.text = "000";
			OnGameEnd.Invoke();
		}
	}
	public void StartGame()
	{
		endTime = Time.time + maxSeconds;
		runTimer = true;
	}
	//method for copying the player's final score from the gameplay text object to the end screen one
	public void MoveScore()
	{
		endTxt.text = gameTxt.text;
		runTimer = false;
	}
	//method for restarting the game
	//this method is called when the "Play Again" button is pressed on the end screen
	public void RestartGame()
	{
		//loads scene 0 in the project
		//since there is only one scene, this effectively reloads the scene
		SceneManager.LoadScene(0);
	}
}
