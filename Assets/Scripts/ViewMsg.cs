using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMsg : MonoBehaviour {

    public string _msg = "";
    public string _subMsg = "";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.RenderMessage(_msg,_subMsg);
            gameObject.SetActive(false);
        }
    }
}
