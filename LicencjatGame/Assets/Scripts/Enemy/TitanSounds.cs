using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanSounds : MonoBehaviour
{
    public GameObject slamZone;

    [SerializeField]
    AudioClip[] shoutClips;
    [SerializeField]
    AudioClip[] attackClips;
    [SerializeField]
    AudioClip stepClip;
    [SerializeField]
    AudioClip deathClip;
    [SerializeField]
    AudioClip[] hurtClips;
    [SerializeField]
    AudioClip slamClip;

    public AudioSource stepSource;
    public AudioSource otherSource;
    public AudioSource slamSource;
    private void Awake()
    {
        slamZone.SetActive(false);
    }
    void TitanAttack1() 
    {
        otherSource.PlayOneShot(attackClips[0]);
    }
    void TitanAttack2() 
    {
        otherSource.PlayOneShot(attackClips[1]);
    }
    void TitanAttack3() 
    {
        otherSource.PlayOneShot(attackClips[2]);
    }
    void GroundSlam() 
    {
        slamZone.SetActive(true);
        slamSource.PlayOneShot(slamClip);
    }
    void TitanDeath() 
    {
        otherSource.PlayOneShot(deathClip);
    }
    void TitanHurt()
    {
        otherSource.PlayOneShot(hurtClips[Random.Range(0,hurtClips.Length)]);
    }
    void TitanStep() 
    {
        stepSource.PlayOneShot(stepClip);
    }
    void TitanShout1() 
    {
        otherSource.PlayOneShot(shoutClips[0]);
    }
    void TitanShout2() 
    {
        otherSource.PlayOneShot(shoutClips[0]);
    }
}
