using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour {

    public float _jumpForce;
    public GameObject _tangTang;

	void Start()
    {
        _tangTang.AddComponent<TrampolinChild>();
        _tangTang.GetComponent<TrampolinChild>().jumpForce = _jumpForce;
    }
}
