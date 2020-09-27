using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour
{
    private bool healRequest, healSuccess;
    public float healTime = 2f;
    private float healStartTime;
    public PlayerHealth playerHp;
    public Slider slider;
    private GameObject sliderGo;
    public bool HealInProgress { get; set; }

    private void Awake()
    {
        sliderGo = slider.transform.gameObject;
        sliderGo.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Heal") && playerHp.PlayerHp<100f)
        {
            if (!HealInProgress) 
            {
                sliderGo.SetActive(true);
                StartCoroutine(Heal());
            }
        }
        if (healRequest)
        {
            progressSlider();
            if (Input.GetButtonUp("Heal"))
            {
                sliderGo.SetActive(false);
                HealFail();
            }
        }
    }
     private IEnumerator Heal()
    {
        HealInProgress = true;
        RequestHeal();
        yield return new WaitUntil(() => healRequest == false);
        
        if (healSuccess)
        {
            if (playerHp.PlayerHp <= 70f)
                playerHp.PlayerHp += 30f;
            else
                playerHp.PlayerHp = playerHp.PlayerHp + (100f - playerHp.PlayerHp);
            print("Cast was suucces");
            sliderGo.SetActive(false);
        }
        slider.value = 0;
        HealInProgress = false;
    }

    private void RequestHeal()
    {
        healRequest = true;
        healSuccess = false;
        slider.value = 0;
        healStartTime = Time.time;
        Invoke("HealSuccess", healTime);

    }
    private void HealFail()
    {
        healRequest = false;
        healSuccess = false;

    }
    private void HealSuccess()
    {
        healRequest = false;
        healSuccess = true;
    }

    private void progressSlider()
    {
        float timePassed = Time.time - healStartTime;
        float percentComplete = timePassed / healTime;
        slider.value = percentComplete;
    }
}
