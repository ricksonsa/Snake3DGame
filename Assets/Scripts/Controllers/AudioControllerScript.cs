using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
    public static AudioControllerScript instance;

    public AudioClip PickUpSound, DeadSound;

    // Start is called before the first frame update
    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null) instance = this;
    }

    public void Play_PickUpSound()
    {
        AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
    }

    public void Play_DeadSound()
    {
        AudioSource.PlayClipAtPoint(DeadSound, transform.position);
    }
}
