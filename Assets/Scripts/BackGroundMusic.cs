using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour {

    public static BackGroundMusic instance;

    public AudioClip _backGroundClip;
    public AudioClip _findPlayerClip;
    public AudioSource _backGroundMusic;

    private bool _isFindPlayer;

    void Awake()
    {
        instance = this;
    }

	void Start () {
        
    }
	
	void Update () {
        
    }

    public void FindPlayer()
    {
        _backGroundMusic.clip = _findPlayerClip;
        _backGroundMusic.loop = false;
        _backGroundMusic.Play();
    }

    public void FindCandy()
    {
        _backGroundMusic.clip = _backGroundClip;
        _backGroundMusic.loop = true;
        _backGroundMusic.Play();
    }
}
