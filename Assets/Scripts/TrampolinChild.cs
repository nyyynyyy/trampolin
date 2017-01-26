using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinChild : MonoBehaviour {

    public AudioClip _jumpSoundClip;
    public AudioSource _jumpSound;

    private float _jumpForce;

    public float jumpForce
    {
        set
        {
            _jumpForce = value;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        _jumpSound.clip = _jumpSoundClip;
        _jumpSound.loop = false;
        _jumpSound.pitch = 0.65f;
        _jumpSound.Play();
      
        if (other.tag != "Player") return;

        Vector3 jumpVecter = new Vector3(0, _jumpForce);

        other.gameObject.GetComponent<Player>().TrampolinJump(jumpVecter);
    }
}
