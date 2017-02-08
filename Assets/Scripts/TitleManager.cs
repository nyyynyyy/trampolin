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

    public float _turningSpeed = 10f;

    public string[] _rooms;

    private int _roomNumber = 0;
    private int _level;

    void Start()
    {
        Time.timeScale = 1;

        _light.enabled = false;
        _left.SetActive(false);
        _roomName.text = _rooms[0];

        if (PlayerPrefs.HasKey("Level"))
        {
            _level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.Save();
            _level = 0;
        }
    }

    void Update()
    {
        TurnLight();
        TurnUnityChan();
    }

    private void TurnLight()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(1))
        {
            _light.enabled = !_light.enabled;
        }
#elif UNITY_ANDROID
        if (Input.GetTouch(1).phase == TouchPhase.Began)
        {
            _light.enabled = !_light.enabled;
        }
#endif
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
        if (_level < _roomNumber) return;
        StartCoroutine(FadeInAndLoadScene("Room" + _roomNumber));
        PlayerPrefs.SetInt("Room", _roomNumber);
        PlayerPrefs.Save();
    }

    public void Staff()
    {
        StartCoroutine(FadeInAndLoadScene("Staff"));
        /*PlayerPrefs.SetInt("Level", 10);
        PlayerPrefs.Save();*/
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
        if (_level < _roomNumber)
        {
            _roomName.color = new Color(150, 150, 150, 255) / 255f;
        }
        else
        {
            _roomName.color = new Color(0, 0, 0);
        }
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
        if (_level < _roomNumber)
        {
            _roomName.color = new Color(150, 150, 150, 255) / 255f;
        }
        else
        {
            _roomName.color = new Color(0, 0, 0);
        }
    }
}
 