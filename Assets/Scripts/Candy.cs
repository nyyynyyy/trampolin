using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {

    public Baby _baby;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Ground")
        {
            _baby.GoLocation(transform.position);
            Debug.Log("CALL BABY : " + transform.position);
        }

        if(other.collider.tag == "Baby")
        {
            gameObject.SetActive(false);
        }
    }
    
}
