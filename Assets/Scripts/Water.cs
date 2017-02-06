using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        string _player = other.collider.tag;

        if (_player == "Player") {
            Debug.Log("water");
        }

    }
}
