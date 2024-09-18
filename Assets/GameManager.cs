using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	//references to the text objects the display score in the game and on the end screen
	[SerializeField] TextMeshProUGUI gameTxt, endTxt;
	//method for copying the player's final score from the gameplay text object to the end screen one
	public void MoveScore()
	{
		endTxt.text = gameTxt.text;
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
