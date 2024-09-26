using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
	//reference to the text object that displays score
	[SerializeField] TextMeshProUGUI textMeshProUGUI;
	//counter that keeps track of the player's current gold count
	int goldCount = 0;

	//method that is called once for every frame drawn to the screen
	//the tim between frames can be inconsistent
	private void Update()
	{
		//convert the gold count variable to a string of text, and display that in the score text object
		textMeshProUGUI.text = goldCount.ToString();
	}
	//public method that contains any logic related to incrementing the gold count
	public void IncrementGold()
	{
		//increments the gold counter by 1
		goldCount++;
	}
}
