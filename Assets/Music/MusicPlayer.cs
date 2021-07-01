using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip Track;
    AudioSource Musicplayer;

     void Awake()
    {
        Musicplayer = GetComponent<AudioSource>();



        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length; 

        if (numMusicPlayers > 1) 
        {
            Destroy(gameObject);
        }
        else 

            DontDestroyOnLoad(gameObject);
    }
     void Update()
    {
        if (!Musicplayer.isPlaying)
        { 
            Musicplayer.PlayOneShot(Track);
        }
    }
}
