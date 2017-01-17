using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float _movespeed = 10f;
    public float _turnspeed = 180f;
    public float _movepoint;

    private float _move;
    private float _rotate;
    private Rigidbody _rigidbody;

	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        _move = Input.GetAxis("Vertical");
        _rotate = Input.GetAxis("Horizontal");
	}

    private void FixedUpdate() {
        Moving();
        Turning();
    }

    private void Moving() {
        Vector3 movement = transform.forward * _move * Time.deltaTime * _movespeed;

        _rigidbody.MovePosition(_rigidbody.position + movement);
        
    }

    private void Turning() {
        float turn = _rotate * Time.deltaTime * _turnspeed;

        Quaternion _turnrotate = Quaternion.Euler(0f, turn, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * _turnrotate);
    }
}
