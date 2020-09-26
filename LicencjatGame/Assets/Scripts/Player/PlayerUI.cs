using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public int maxMeds = 3;
    public int currMeds;
    public GameObject medsDisplay;
    public ItemPickUp ItemPick;
    void Update()
    {
        currMeds = ItemPick.currMeds;
        medsDisplay.GetComponent<Text>().text = "" + currMeds;
    }
}
