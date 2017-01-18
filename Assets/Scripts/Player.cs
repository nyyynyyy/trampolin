using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float _movespeed = 10f;
    public float _turnspeed = 180f;
    public float _jumphigh = 500f;

    private float _move;
    private float _rotate;
    private Rigidbody _rigidbody;
    private bool _jump;
    private bool _jumpon;

	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        _move = Input.GetAxis("Vertical");
        _rotate = Input.GetAxis("Horizontal");
        _jump = Input.GetKeyDown(KeyCode.Space);
	}

    private void FixedUpdate() {
        
        Turning();
        if (_jumpon == false) {
            Moving();
            if (_jump == true)
            {
                Jumping();
            }
        }
        else {
            JumpMoving();
        }
    }

    private void Moving() {
        Vector3 movement = transform.forward * _move * Time.deltaTime * _movespeed;

        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void JumpMoving() {
        if(_move < 0) _move = 0;

        Vector3 movement = transform.forward * _move * Time.deltaTime * _movespeed;

        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void Turning() {
        float turn = _rotate * Time.deltaTime * _turnspeed;

        Quaternion _turnrotate = Quaternion.Euler(0f, turn, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * _turnrotate);
    }
    
    private void Jumping() {
        _rigidbody.AddForce(new Vector3(0, _jumphigh, 0));
    }

    private void OnCollisionEnter(Collision other)
    {
        // 착지
        string tag = other.collider.tag;

        if(tag == "Ground")
            _jumpon = false;

    }

    private void OnCollisionExit(Collision other)
    {
        // 점프
        string tag = other.collider.tag;

        if(tag == "Ground")
            _jumpon = true;
    }
}
