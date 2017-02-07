using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapArea : MonoBehaviour {

    public Vector2 _bottomLeft;
    public Vector2 _topRight;

    private float _width;
    private bool _press;
    private int _pinger;

    public bool press
    {
        get
        {
            return _press;
        }
    }

    void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
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
            _press = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (IsInRange(pingerPos)) return;
            Init();
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
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (!IsInRange(pingerPos)) continue;
                transform.position = pingerPos;
                _press = true;
                _pinger = i;
            }
            if(Input.GetTouch(i).phase == TouchPhase.Ended)
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
        _press = false;
    }

    private bool IsInRange(Vector3 pingerPos)
    {
        if (pingerPos.x > Camera.main.ViewportToScreenPoint(_topRight).x) return false;
        if (pingerPos.y > Camera.main.ViewportToScreenPoint(_topRight).y) return false;
        if (pingerPos.x < Camera.main.ViewportToScreenPoint(_bottomLeft).x) return false;
        if (pingerPos.y < Camera.main.ViewportToScreenPoint(_bottomLeft).y) return false;
        return true;
    }
}
