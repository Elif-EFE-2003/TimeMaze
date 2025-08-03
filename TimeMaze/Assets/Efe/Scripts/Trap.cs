using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator wallAnim;
    public AudioSource fallSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            wallAnim.SetBool("icerde", true);
            fallSound.Play();
        }
    }
}
