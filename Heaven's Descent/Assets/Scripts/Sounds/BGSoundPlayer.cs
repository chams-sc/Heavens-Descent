using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundPlayer : MonoBehaviour
{
    public AudioSource src;
    public AudioClip backgoundMusic;
    // Start is called before the first frame update
    void Start()
    {
        src.clip = backgoundMusic;
        src.Play();
    }
}
