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
//#if UNITY_EDITOR
    private Vector3 _clickJoyPosition;
/*#elif UNITY_ANDROID
    private Vector3[] _clickJoyPosition = new Vector[5];
#endif*/
    void Start() {
        _firstJoyPosition = _joyPosition.position;
        _stickhHeight = _stickPosition.rect.height;
        //GetComponent("Player");
    }

    void Update() {
//#if UNITY_EDITOR
        Vector3 mousePosition = Input.mousePosition;
/*#elif UNITY_ANDROID
        Vector3[] mousePosition = new Vector3[5]{
            Input.GetTouch(0).position,    
            Input.GetTouch(1).position,    
            Input.GetTouch(2).position,    
            Input.GetTouch(3).position,    
            Input.GetTouch(4).position
        }
#endif*/

//#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _clickJoyPosition = mousePosition;
        }
/*#elif UNITY_ANDROID
        for (int i = 0; i < 5; i++)
        { 
            if (Input.GetTouch(i).phase != TouchPhase.Began) continue;
            _clickJoyPosition[i] = Input.GetTouch(i).position;
        }    
#endif*/
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