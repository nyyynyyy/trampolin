using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

    public Baby _baby;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Ground")
        {
            if (_baby.transform.position.y > transform.position.y)
            {
                _baby.FindCandy(transform);
                Debug.Log("CALL BABY : " + transform.position);
            }
        }

        if(other.collider.tag == "Baby")
        {
            gameObject.SetActive(false);
        }
    }
    
}
