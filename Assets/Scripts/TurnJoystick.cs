using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnJoystick : MonoBehaviour {

    public RectTransform _joyPosition;
    public RectTransform _stickPosition;
    public RectTransform _canvasPosition;
    public Player _player;

    private float _stickWidth;
    private Vector3 _firstJoyPosition;
    private Vector3 _clickJoyPosition;

    void Start () {
        _firstJoyPosition = _joyPosition.position;
        _stickWidth = _stickPosition.rect.width;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

//#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _clickJoyPosition = mousePosition;
            if (_clickJoyPosition.x > _canvasPosition.position.x && _clickJoyPosition.y < _canvasPosition.position.y)
            {
                Jump();
                return;
            }
        }

//#elif UNITY_ANDROID
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                _clickJoyPosition = Input.GetTouch(i).position;
                if (_clickJoyPosition.x > _canvasPosition.position.x && _clickJoyPosition.y < _canvasPosition.position.y)
                {
                    Jump();
                    return;
                }
            }
        }
//#endif

        _player.JumpingOff();

        if (Input.GetMouseButtonUp(0))
        {
            JoyInit();
            return;
        }

        if (_clickJoyPosition.x < _canvasPosition.position.x || _clickJoyPosition.y < _canvasPosition.position.y)
        {
            JoyInit();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            mousePosition = new Vector3(Input.mousePosition.x, _clickJoyPosition.y);

            mousePosition.x = ClampPositionX(mousePosition.x);

            _joyPosition.position = _clickJoyPosition;
            _stickPosition.position = mousePosition;

            Turn();
        }
    }

    private float ClampPositionX(float mousePosition)
    {
        if (mousePosition - _clickJoyPosition.x >= _stickWidth / 5)
        {
            mousePosition = _clickJoyPosition.x + _stickWidth / 5;
        }
        else if (mousePosition - _clickJoyPosition.x <= _stickWidth / -5)
        {
            mousePosition = _clickJoyPosition.x + _stickWidth / -5;
        }

        return mousePosition;
    }

    private void JoyInit()
    {
        _joyPosition.position = _firstJoyPosition;
        _stickPosition.position = _firstJoyPosition;
        _player.TurnStop();
    }

    private void Turn()
    {
        if (_joyPosition.position.x > _stickPosition.position.x)
        {
            _player.TurnLeft();
        }
        else if (_joyPosition.position.x < _stickPosition.position.x)
        {
            _player.TurnRight();
        }
        else
        {
            _player.TurnStop();
        }
    }

    private void Jump()
    {
        _player.JumpingOn();
    }
}
