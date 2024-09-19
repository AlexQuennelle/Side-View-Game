using UnityEngine;

public class Gold : MonoBehaviour
{
	//reference to the gold manager
	[SerializeField] GoldManager gm;
	//method that is called when a 2D collider enters a trigger
	//both objects must have a rigidbody2D attached
	//provides a reference to the collider not attached to the current game object
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//call the IncrementGold() method on the gold manager and turns the current game object off
		gm.IncrementGold();
		gameObject.SetActive(false);
	}
}
