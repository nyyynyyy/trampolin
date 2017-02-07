using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour {

    public enum Type
    {
        Horizontal,
        Vertical,
    }

    public Image _stick;
    public Type _type;
    public Vector2 _bottomLeft;
    public Vector2 _topRight;

    private float _axis;
    private float _width;
    private bool _isUsed;
    private int _pinger;

    public float axis
    {
        get
        {
            return _axis;
        }
    }

    void Start()
    {
        _width = _stick.rectTransform.rect.width;
        Init();
    }

    void Update()
    {
        Vector3 pingerPos;

#if UNITY_EDITOR
        pingerPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInRange(pingerPos)) return;
            transform.position = pingerPos;
            _isUsed = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (!_isUsed) return;
            if (!IsInRange(pingerPos))
            {
                Init();
                return;
            }
            _stick.transform.position = FixedPostion(pingerPos);
            SetAxis();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!IsInRange(pingerPos)) return;
            Init();
        }
#elif UNITY_ANDROID
        for(int i = 0; i < 5; i++)
        {
            pingerPos = Input.GetTouch(i).position;
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (!IsInRange(pingerPos)) continue;

                transform.position = pingerPos;
                _isUsed = true;
                _pinger = i;
            }
            if (Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
            {
                if (!_isUsed) continue;
                if (_pinger != i) continue;

                _stick.transform.position = FixedPostion(pingerPos);
                SetAxis();
            }
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                if (i == _pinger) Init();
                if (i < _pinger) _pinger--;
            }
        }
#endif
    }

    void Init()
    {
        transform.position = new Vector3(-_width, -_width);
        _stick.transform.position = transform.position;
        _axis = 0;
        _isUsed = false;
    }

    private Vector3 FixedPostion(Vector3 pinger)
    {
        if(_type == Type.Horizontal)
        {
            pinger.y = transform.position.y;
            pinger.x = Mathf.Clamp(pinger.x, transform.position.x - _width * 0.5f,transform.position.x + _width * 0.5f);
           // Debug.Log(pinger.x + " " + (transform.position.x - _width * 0.5f) + " " + (transform.position.x + _width * 0.5f));
        }
        if(_type == Type.Vertical)
        {
            pinger.x = transform.position.x;
            pinger.y = Mathf.Clamp(pinger.y, transform.position.y - _width * 0.5f, transform.position.y + _width * 0.5f);
        }
        return pinger;
    }

    private bool IsInRange(Vector3 pingerPos)
    {
        if (pingerPos.x > Camera.main.ViewportToScreenPoint(_topRight).x) return false;
        if (pingerPos.y > Camera.main.ViewportToScreenPoint(_topRight).y) return false;
        if (pingerPos.x < Camera.main.ViewportToScreenPoint(_bottomLeft).x) return false;
        if (pingerPos.y < Camera.main.ViewportToScreenPoint(_bottomLeft).y) return false;
        return true;
    }

    private void SetAxis()
    {
        if(_type == Type.Horizontal)
        {
            if (transform.position.x > _stick.transform.position.x) _axis = -1;
            if (transform.position.x < _stick.transform.position.x) _axis = 1;
        }
        if(_type == Type.Vertical)
        {
            if (transform.position.y > _stick.transform.position.y) _axis = -1;
            if (transform.position.y < _stick.transform.position.y) _axis = 1;
        }
    }
}
