using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{

    public float _moveSpeed;
    public GameObject[] _coursePoint;

    private Rigidbody _rigid;

    private const float EQUALS_AREA = 1f;
    private const float TURN_SPEED = 50f;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        MoveCourse(0);
        SetAngle(0);
        StartCoroutine(LoopCourse());
    }

    void Update()
    {
        DebugCourse();
    }

    private void DebugCourse()
    {
        for(int i = 0; i < _coursePoint.Length; i++)
        {
            Vector3 start = _coursePoint[i].transform.position;
            Vector3 finish = _coursePoint[i != _coursePoint.Length - 1 ? i + 1 : 0].transform.position;
            Debug.DrawRay(start, finish - start, Color.red);
        }
    }

    /*
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag + " : ENTER");
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log(other.collider.tag + " : EXIT");
    }
    */

    private IEnumerator LoopCourse()
    {
        if (_coursePoint.Length == 0) yield break;

        foreach (GameObject finish in _coursePoint)
        {
            yield return StartCoroutine(GoToCourse(finish.transform.position));
        }

        StartCoroutine(LoopCourse());
    }

    private IEnumerator GoToCourse(Vector3 finish)
    {
        yield return StartCoroutine(TurnToAngle(finish));

        while (Vector3.Distance(transform.position, finish) > EQUALS_AREA)
        {
            Vector3 arrow = finish - transform.position;
            arrow.Normalize();
            Vector3 movePos = transform.position + arrow * _moveSpeed * Time.fixedDeltaTime;
            transform.position = movePos;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator TurnToAngle(Vector3 finish)
    {
        Vector3 arrowVector = finish - transform.position;

        float angle = GetAngle();
        float finishAngle = Mathf.Atan2(arrowVector.x, arrowVector.z) * Mathf.Rad2Deg;

        //Debug.Log(" ME : " + angle + " / " + "FINSIH : " + finishAngle);

        float angleDis = finishAngle - angle;
        TurnNumber(ref angleDis, 360);

        float arrow = finishAngle - angle > angle - finishAngle ? 1: -1;

        //Debug.Log(angleDis);
    
        while (Mathf.Abs(angleDis) > 1f)
        {
            angle += TURN_SPEED * Time.fixedDeltaTime * arrow;
            TurnNumber(ref angle, 360);

            angleDis = finishAngle - angle;
            TurnNumber(ref angleDis, 360);
            //Debug.Log(angleDis);

            SetAngle(angle);

            yield return new WaitForFixedUpdate();
        }

        SetAngle(finishAngle);
        //Debug.Log("TURN END : " + GetAngle());

       // yield return new WaitForSeconds(1f);
    }

    private void TurnNumber(ref float number, float limit)
    {
        while(number > limit)
        {
            number -= limit;
        }
        while(number < 0)
        {
            number += limit;
        }
    }

    private void SetAngle(float angle)
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private float GetAngle()
    {
        float angle = transform.rotation.eulerAngles.y;
        TurnNumber(ref angle, 360f);
        return angle;
    }

    private void MoveCourse(int index)
    {
        transform.position = _coursePoint[index].transform.position;
    }

    
}
