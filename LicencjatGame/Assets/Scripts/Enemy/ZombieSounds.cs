using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] chasingClips;
    [SerializeField]
    private AudioClip[] searchingClips;
    [SerializeField]
    private AudioClip[] attackingClips;
    [SerializeField]
    private AudioClip dieClip;
    [SerializeField]
    private AudioClip stepClip;

    public AudioSource otherSource;
    public AudioSource stepSource;

    bool makeSound=true;

    private void ZombieChasing()
    {
        if (makeSound)
            otherSource.PlayOneShot(chasingClips[Random.Range(0, chasingClips.Length)]);
        makeSound = !makeSound;
    }
    private void ZombieSearching()
    {
        if (makeSound)
            otherSource.PlayOneShot(searchingClips[Random.Range(0, searchingClips.Length)]);
        makeSound = !makeSound;
    }
    private void ZombieAttack()
    {
        otherSource.PlayOneShot(attackingClips[Random.Range(0, attackingClips.Length)]);
    }
    private void ZombieStep()
    {
        stepSource.PlayOneShot(stepClip);
    }
    private void ZombieDie()
    {
        otherSource.PlayOneShot(dieClip);
    }
    
}
