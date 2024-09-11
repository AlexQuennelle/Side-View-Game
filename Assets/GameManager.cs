using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    public void StartGame()
    {
    }
    public void EndGame()
    {
		gameOver.SetActive(true);
	}
	public void RestartGame()
	{

	}
}
