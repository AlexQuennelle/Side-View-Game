using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    int goldCount = 0;
    private void Update()
    {
        textMeshProUGUI.text = goldCount.ToString();
    }
    public void IncrementGold()
    {
        goldCount++;
    }
}
