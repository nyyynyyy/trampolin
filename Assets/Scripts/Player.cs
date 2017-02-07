using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimType
{
    Run,
    Back,
    Right,
    Left,
    JumpUp,
    JumpDown,
    Rest,
}

public class Player : MonoBehaviour {

    public Joystick _horizontal;
    public Joystick _vertical;
    public TapArea _jumpArea;

    public float _moveSpeed = 10f;
    public float _turnSpeed = 120f;
    public float _jumpHigh = 500f;

    private Animator _anim;
    private Rigidbody _rigid;
    private bool _isJumpOn;
    private bool _isTouching;

    private float _move;
    private float _rotate;
    private bool _isJumpKeyDown;

    private bool _isRest;
    private bool _isForward;
    private bool _isBackward;
    private bool _isJumping;

    private string[] keys;

    void Awake()
    {
        _anim = GetComponent<Animator>();

        keys = new string[Enum.GetValues(typeof(AnimType)).Length];
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = "Is" + ((AnimType)i).ToString();
        }
    }

    void Start () {
        _rigid = GetComponent<Rigidbody>();
        _isJumpOn = true;
	}
	
	void Update () {
#if UNITY_EDITOR
        _move = Input.GetAxis("Vertical");
        _rotate = Input.GetAxis("Horizontal");
        _isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
#elif UNITY_ANDROID
        _move = _vertical.axis;
        _rotate = _horizontal.axis;
        _isJumpKeyDown = _jumpArea.press;
#endif
    }

    void FixedUpdate() {
        if (_move == 0 && _rotate == 0 && !_isJumpOn) Anim(AnimType.Rest);

        Turning();
        Moving();
        Jumping();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isJumpOn = false;
        }
        if (other.collider.CompareTag("Desk"))
        {
             _isTouching = true;
        }
        if(other.collider.CompareTag("Water"))
        {
            GameManager.instance.Gameover();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            _isJumpOn = true;
        }
        if (other.collider.CompareTag("Desk"))
        {
            _isJumpOn = true;
            _isTouching = false;
        }
    }

    private void Moving() {
        if (_isJumpOn && _isTouching) return;

        if (_move < 0 && _isJumpOn) _move = 0;

        if (_move < 0)
            _move *= 0.5f;

        Vector3 movement = transform.forward * _move * Time.fixedDeltaTime * _moveSpeed;
        movement.y = _rigid.velocity.y;
        _rigid.velocity = movement;

        if (_isJumpOn) return;

        if (_move > 0) Anim(AnimType.Run);
        if (_move < 0) Anim(AnimType.Back);
    }

    private void Turning() {
        float angle = transform.rotation.eulerAngles.y;
        float arrow = _rotate * Time.fixedDeltaTime * _turnSpeed;

        transform.rotation = Quaternion.AngleAxis(
            angle + arrow,
            transform.up);

        if (_isJumpOn) return;

        if (_rotate > 0) Anim(AnimType.Right);
        if (_rotate < 0) Anim(AnimType.Left);
    }

    private void Jumping() {
        if (_isJumpOn) return;
        if (!_isJumpKeyDown) return;

        _rigid.AddForce(new Vector3(0, _jumpHigh, 0));
        _isJumpOn = true;
        Debug.Log("Jump On");
    }

    private void Anim(AnimType type)
    {
        foreach (string key in keys)
        {
            _anim.SetBool(key, false);
        }

        _anim.SetBool("Is" + type.ToString(), true);
    }

    public void TrampolinJump(Vector3 jumpVector)
    {
        Debug.Log("JUMP");

        _rigid.velocity = new Vector3(_rigid.velocity.x, 0, _rigid.velocity.z); // 중력 가속도 초기화
        _rigid.AddForce(jumpVector);
    }
}
