using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnJoystick : MonoBehaviour {

    public RectTransform _joyposition;
    public RectTransform _adposition;
    public RectTransform _canvasposition;
    public Player _player;

    private float _adwidth;
    private Vector3 _firstjoyposition;
    private Vector3 _clickjoyposition;
    private bool _mousedown = false;
    private bool _joyon;
    private bool _playerjump = false;

    void Start () {
        _firstjoyposition = _joyposition.position;
        _adwidth = _adposition.rect.width;
        GetComponent("Player");
    }
	
	void Update () {
        Vector3 _mouseposition = Input.mousePosition;

		if(Input.GetMouseButtonDown(0)) {
            _clickjoyposition = _mouseposition;
            if (_clickjoyposition.x > _canvasposition.position.x && _clickjoyposition.y < _canvasposition.position.y)
            {
                _playerjump = true;  
            }
        }
        
        if(_playerjump == true) {
            Debug.Log("jump");
            _player._jump = true;
            _playerjump = false;
        }
            

        if (_clickjoyposition.x < _canvasposition.position.x || _clickjoyposition.y < _canvasposition.position.y) {
            _mouseposition = _firstjoyposition;
            _joyon = false;
            _mousedown = false;
        }
        else {
            _joyon = true;
        }

        if (_joyon == true && _mousedown == true) {

            _mouseposition = new Vector3(Input.mousePosition.x, _clickjoyposition.y);

            if (_mouseposition.x - _clickjoyposition.x >= _adwidth / 2)
            {
                _mouseposition.x = _clickjoyposition.x + _adwidth / 2;
            }
            else if (_mouseposition.x - _clickjoyposition.x <= _adwidth / -2)
            {
                _mouseposition.x = _clickjoyposition.x + _adwidth / -2;
            }
        }

        if(_joyon == true) {
            if(Input.GetMouseButton(0)) {
                _joyposition.position = _clickjoyposition;
                _adposition.position = _mouseposition;
                _mousedown = true;

                if (_joyposition.position.x > _adposition.position.x) {
                    _player._rotate = -1;
                }
                else if (_joyposition.position.x < _adposition.position.x) {
                    _player._rotate = 1;
                }
                else {
                    _player._rotate = 0;
                }
            }
            else {
                _joyposition.position = _firstjoyposition;
                _adposition.position = _firstjoyposition;
                _mousedown = false;
            }
        }
        else {
            _joyposition.position = _firstjoyposition;
            _adposition.position = _firstjoyposition;
            _mousedown = false;
        }
        
    }
}
