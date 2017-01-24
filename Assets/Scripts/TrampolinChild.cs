using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolinChild : MonoBehaviour {
    
    private float _jumpForce;

    public float jumpForce
    {
        set
        {
            _jumpForce = value;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("JUMP");
        if (other.tag != "Player" ) return;

        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        Vector3 jumpVecter = new Vector3(0, _jumpForce);

        Debug.Log("JUMP");

        rigid.velocity = new Vector3(rigid.velocity.x , 0, rigid.velocity.z); // 중력 가속도 초기화
        rigid.AddForce(jumpVecter);
    }
}
