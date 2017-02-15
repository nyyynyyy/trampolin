using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

    public Baby _baby;
    private Rigidbody _rigid;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    void Updae()
    {
        _rigid.velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Ground"))
        {
            if (_baby.transform.position.y > transform.position.y)
            {
                _baby.FindCandy(transform);
                Debug.Log("CALL BABY : " + transform.position);
            }
        }

        if(other.collider.CompareTag("Baby"))
        {
            gameObject.SetActive(false);
        }
    }
    
}
