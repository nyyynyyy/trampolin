using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour {

    public float _jumpForce;
    public GameObject _tangTang;
    public AudioClip _jumpSoundClip;
    public AudioSource _jumpSound;

	void Start()
    {
        _tangTang.AddComponent<TrampolinChild>();
        _tangTang.GetComponent<TrampolinChild>()._jumpSoundClip = _jumpSoundClip;
        _tangTang.GetComponent<TrampolinChild>()._jumpSound = _jumpSound;
        _tangTang.GetComponent<TrampolinChild>().jumpForce = _jumpForce;
    }
}
