using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimType
{
    Run,
    Back,
    Right,
    Left,
    Jump,
    Rest,
}

public class Player : MonoBehaviour {

    public float _moveSpeed = 10f;
    public float _turnSpeed = 180f;
    public float _jumpHigh = 500f;

    private float _moveJoy;
    private float _rotateJoy;
    private bool _isJumpJoy;

    private Animator _anim;
    private Rigidbody _rigidbody;
    private bool _isJumpOn;
    private float _move;
    private float _rotate;
    private bool _isJumpKeyDown;

    private bool isRest;
    private bool isForward;
    private bool isBackward;
    private bool isJumping;

    private string[] keys = new string[6];

    void Awake()
    {
        _anim = GetComponent<Animator>();
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = "Is" + ((AnimType)i).ToString();
            Debug.Log(keys[i]);
        }
    }

    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        _move = Input.GetAxis("Vertical");
        _rotate = Input.GetAxis("Horizontal");
        _isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
	}

    private void FixedUpdate() {
        if (_moveJoy != 0) _move = _moveJoy;
        if (_rotateJoy != 0) _rotate = _rotateJoy;
        if (_isJumpJoy) _isJumpKeyDown = _isJumpJoy;

        if (_move == 0 && _rotate == 0 && !_isJumpOn) Anim(AnimType.Rest);

        Turning();
        Moving();
        Jumping();
    }

    private void Moving() {
        if (_move < 0 && _isJumpOn) _move = 0;

        Vector3 movement = transform.forward * _move * Time.fixedDeltaTime * _moveSpeed;
        
        _rigidbody.MovePosition(_rigidbody.position + movement);

        if (_isJumpOn) return;

        if (_move > 0) Anim(AnimType.Run);
        if (_move < 0) Anim(AnimType.Back);
    }

    private void Turning() {
        float turn = _rotate * Time.fixedDeltaTime * _turnSpeed;

        Quaternion turnRotate = Quaternion.Euler(0f, turn, 0f);

        _rigidbody.MoveRotation(_rigidbody.rotation * turnRotate);

        if (_isJumpOn) return;

        if (_rotate > 0) Anim(AnimType.Right);
        if (_rotate < 0) Anim(AnimType.Left);
    }

    private void Jumping() {
        if (_isJumpOn) return;
        if (!_isJumpKeyDown) return;

        _rigidbody.AddForce(new Vector3(0, _jumpHigh, 0));
        Anim(AnimType.Jump);
    }

    private void OnCollisionEnter(Collision other)
    {
        // 착지
        string tag = other.collider.tag;
        
        if(tag == "Ground" || tag == "Desk")
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

        if(tag == "Ground" || tag == "Desk")
            _isJumpOn = true;

        else if (tag == "Water")
            _moveSpeed = 10f;
    }

    #region Setter
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
    #endregion

    void Anim(AnimType type)
    {
        foreach (string key in keys)
        {
            _anim.SetBool(key, false);
        }

        _anim.SetBool("Is" + type.ToString(), true);
    }
}
