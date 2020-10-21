using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBacklight : MonoBehaviour
{
    
    public bool isSelected = false;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Selected(bool selection)
    {
        isSelected = selection;
        if (isSelected == true)
            Backlight(.4f);
        else
            Backlight(0f);
    }

    public void Backlight(float value)
    {
        Color tempColor = image.color;
        tempColor.a = value;
        image.color = tempColor;
    }

}
