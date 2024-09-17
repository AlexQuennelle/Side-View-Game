using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI gameTxt, endTxt;
	public void MoveScore()
	{
		endTxt.text = gameTxt.text;
	}
	public void RestartGame()
	{
		SceneManager.LoadScene(0);
	}
}
