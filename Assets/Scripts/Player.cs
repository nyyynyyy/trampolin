using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float _moveSpeed = 10f;
    public float _turnSpeed = 180f;
    public float _jumpHigh = 500f;

    private float _moveJoy;
    private float _rotateJoy;
    private bool _isJumpJoy;

    private Rigidbody _rigidbody;
    private bool _isJumpOn;
    private float _move;
    private float _rotate;
    private bool _isJumpKeyDown;
  
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        _move = Input.GetAxis("Vertical");
        _rotate = Input.GetAxis("Horizontal");
        _isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
	}

    private void FixedUpdate() {
        if (_moveJoy != 0)
            _move = _moveJoy;
        if (_rotateJoy != 0)
            _rotate = _rotateJoy;
        if (_isJumpJoy)
            _isJumpKeyDown = _isJumpJoy;

        Turning();
        Jumping();
        Moving();
    }

    private void Moving() {
        if (_move < 0 && _isJumpOn) _move = 0;

        Vector3 movement = transform.forward * _move * Time.fixedDeltaTime * _moveSpeed;
        
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }

    private void Turning() {
        float turn = _rotate * Time.fixedDeltaTime * _turnSpeed;

        Quaternion turnRotate = Quaternion.Euler(0f, turn, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotate);
    }
    
    private void Jumping() {
        if (_isJumpOn) return;
        if (!_isJumpKeyDown) return;

        _rigidbody.AddForce(new Vector3(0, _jumpHigh, 0));
    }

    private void OnCollisionEnter(Collision other)
    {
        // 착지
        string tag = other.collider.tag;

        if(tag == "Ground")
            _isJumpOn = false;

        if (tag == "Water")
            _moveSpeed = 0;

        if (_moveSpeed == 0 && _isJumpOn == true) {
            Debug.Log("die");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // 점프
        string tag = other.collider.tag;

        if(tag == "Ground")
            _isJumpOn = true;

        else if (tag == "Water")
            _moveSpeed = 10f;
    }

    public void MoveForward()
    {
        _moveJoy = 1;
    }

    public void MoveBackward()
    {
        _moveJoy = -1;
    }

    public void MoveStop()
    {
        _moveJoy = 0;
    }

    public void JumpingOn()
    {
        _isJumpJoy = true;
    }

    public void JumpingOff()
    {
        _isJumpJoy = false;
    }

    public void TurnRight()
    {
        _rotateJoy = 1;
    }

    public void TurnLeft()
    {
        _rotateJoy = -1;
    }

    public void TurnStop()
    {
        _rotateJoy = 0;
    }
}
