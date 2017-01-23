using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour {

    public RectTransform _joyPosition;
    public RectTransform _stickPosition;
    public RectTransform _canvasPosition;
    public Player _playerMove;

    private float _stickhHeight;
    private Vector3 _firstJoyPosition;
    private Vector3 _clickJoyPosition;

    void Start() {
        _firstJoyPosition = _joyPosition.position;
        _stickhHeight = _stickPosition.rect.height;
        //GetComponent("Player");
    }

    void Update() {
        Vector3 mousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0)) {
            _clickJoyPosition = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            JoyInit();
            return;
        }

        if (_clickJoyPosition.x >= _canvasPosition.position.x) { // 오른쪽을 클릭했다면 끝
            JoyInit();
            return;
        }
        
        // 왼쪽 클릭
        
        if (Input.GetMouseButton(0)) {
            mousePosition = new Vector3(_clickJoyPosition.x, Input.mousePosition.y);

            mousePosition.y = ClampPositionY(mousePosition.y);

            _joyPosition.position = _clickJoyPosition;
            _stickPosition.position = mousePosition;

            Move();
        }
    }

    private float ClampPositionY(float mousePositionY)
    {
        if (mousePositionY - _clickJoyPosition.y >= _stickhHeight / 5)
        {
            mousePositionY = _clickJoyPosition.y + _stickhHeight / 5;
        }
        else if (mousePositionY - _clickJoyPosition.y <= _stickhHeight / -5)
        {
            mousePositionY = _clickJoyPosition.y + _stickhHeight / -5;
        }

        return mousePositionY;
    }

    private void JoyInit()
    {
        _joyPosition.position = _firstJoyPosition;
        _stickPosition.position = _firstJoyPosition;
        _playerMove.MoveStop();
    }

    private void Move()
    {
        if (_joyPosition.position.y < _stickPosition.position.y)
        {
            _playerMove.MoveForward();
        }
        else if (_joyPosition.position.y > _stickPosition.position.y)
        {
            _playerMove.MoveBackward();
        }
        else
        {
            _playerMove.MoveStop();
        }
    }
}