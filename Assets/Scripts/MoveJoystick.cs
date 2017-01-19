using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour {

    public RectTransform _joyposition;
    public RectTransform _wsposition;
    public RectTransform _canvasposition;
    public Player _playermove;

    private float _wsheight;
    private Vector3 _firstjoyposition;
    private Vector3 _clickjoyposition;
    private bool _mousedown = false;
    private bool _joyon;
    


    void Start() {
        _firstjoyposition = _joyposition.position;
        _wsheight = _wsposition.rect.height;
        GetComponent("Player");
    }
	
	void Update () {
        // 마우스 좌표
        Vector3 _mouseposition = Input.mousePosition;

        // 클릭했을때 좌표
        if(Input.GetMouseButtonDown(0)) {
            _clickjoyposition = _mouseposition;
        }

        if (_clickjoyposition.x >= _canvasposition.position.x) {
            _mouseposition = _firstjoyposition;
            _joyon = false;
            _mousedown = false;
        }
        else {
            _joyon = true;
        }

        if (_joyon == true && _mousedown == true) {
            _mouseposition = new Vector3(_clickjoyposition.x, Input.mousePosition.y);

            if (_mouseposition.y - _clickjoyposition.y >= _wsheight / 2)
            {
                _mouseposition.y = _clickjoyposition.y + _wsheight / 2;
            }
            else if (_mouseposition.y - _clickjoyposition.y <= _wsheight / -2)
            {
                _mouseposition.y = _clickjoyposition.y + _wsheight / -2;
            }
        }
            
        if (_joyon == true) {
            if (Input.GetMouseButton(0))
            {
                _joyposition.position = _clickjoyposition;
                _wsposition.position = _mouseposition;
                _mousedown = true;

                if (_joyposition.position.y < _wsposition.position.y) {
                    _playermove._move = 1;
                }
                else if (_joyposition.position.y > _wsposition.position.y) {
                    _playermove._move = -1;
                }
                else
                    _playermove._move = 0;
            }
            else
            {
                _joyposition.position = _firstjoyposition;
                _wsposition.position = _firstjoyposition;
                _mousedown = false;
            }
        }
        else {
            _joyposition.position = _firstjoyposition;
            _wsposition.position = _firstjoyposition;
            _mousedown = false;
        }
    }
}