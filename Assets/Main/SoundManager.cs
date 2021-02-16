using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource sound;

    public void PlaySomeSound()
    {
        sound.Play();
    }
}
