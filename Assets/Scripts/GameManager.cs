using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Image _background;
    public Text _text;
    public Text _subText;

    private bool _isGameover;
    private bool _isDark;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("ALREADY GAME MANAGER");
        }
        instance = this;
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&_isDark)
        {
            if (_isGameover)
            {
                SceneManager.LoadScene("Title");
            }
            else
            {
                _isDark = false;
                StartCoroutine(FadeOut());
            }
        }
    }

    public void Gameover(bool clear = false)
    {
        StartCoroutine(FadeIn(clear?"탈출 성공":"탈출 실패"));
        _isGameover = true;

        if (!clear) return;
        int room = PlayerPrefs.GetInt("Room");
        int level = PlayerPrefs.GetInt("Level");

        if (room != level) return;
        PlayerPrefs.SetInt("Level", level + 1);
        PlayerPrefs.Save();
    }

    public void RenderMessage(string msg, string msg2)
    {
        StartCoroutine(FadeIn(msg, msg2));
    }

    private IEnumerator FadeOut()
    {
        _text.text = "";
        _subText.text = "";
        Time.timeScale = 0;
        Color color = new Color(0, 0, 0, 255 / 255f);
        while (color.a > 0)
        {
            color.a -= 10 / 255f;
            _background.color = color;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        Time.timeScale = 1;
    }

    private IEnumerator FadeIn(string message, string message2 = "")
    {
        Time.timeScale = 0;
        Color color = new Color(0, 0, 0, 0);
        while (color.a < 200 / 255f)
        {
            color.a += 10 / 255f;
            _background.color = color;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        _text.text = message;
        _subText.text = message2;
        _isDark = true;
    }
}
