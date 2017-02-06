using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    public GameObject _unityChan;
    public Light _light;

    public GameObject _right;
    public GameObject _left;

    public Image _background;

    public Text _roomName;

    public float _turningSpeed = 5f;

    public string[] _rooms;

    private int _roomNumber = 0;

    void Start()
    {
        Time.timeScale = 1;

        _light.enabled = false;
        _left.SetActive(false);
        _roomName.text = _rooms[0];
    }

    void Update()
    {
        TurnLight();
        TurnUnityChan();
    }

    private void TurnLight()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _light.enabled = !_light.enabled;
            _turningSpeed *= 10f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _turningSpeed *= 0.1f;
        }
    }

    private void TurnUnityChan()
    {
        float nowAngle = _unityChan.transform.rotation.eulerAngles.y;
        _unityChan.transform.rotation = Quaternion.Euler(0, nowAngle + _turningSpeed * Time.deltaTime, 0);
    }

    private IEnumerator FadeInAndLoadScene(string sceneName)
    {
        _background.gameObject.SetActive(true);
        Color color = new Color(0, 0, 0, 0);
        while (color.a < 1)
        {
            color.a += 10 / 255f;
            _background.color = color;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void Room()
    {
        StartCoroutine(FadeInAndLoadScene("Room" + _roomNumber));
    }

    public void Staff()
    {
        StartCoroutine(FadeInAndLoadScene("Staff"));
    }

    public void Exit()
    {
        Debug.Log("EXIT");
    }

    public void Right()
    {
        _left.SetActive(true);
        _roomNumber++;
        if (_roomNumber == _rooms.Length - 1)
        {
            _roomNumber = _rooms.Length - 1;
            _right.SetActive(false);
        }
        _roomName.text = _rooms[_roomNumber];
    }

    public void Left()
    {
        _right.SetActive(true);
        _roomNumber--;
        if (_roomNumber == 0)
        {
            _roomNumber = 0;
            _left.SetActive(false);
        }
        _roomName.text = _rooms[_roomNumber];
    }
}
 