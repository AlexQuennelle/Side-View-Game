using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] GoldManager gm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.IncrementGold();
        gameObject.SetActive(false);
    }
}
