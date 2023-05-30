using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private bool isWait = false;
    private bool isHidden = true;
    [SerializeField] private GameObject point;
    void Start()
    {
        point = new GameObject("beetleHelper");
        point.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
    }

  
    void Update()
    {
        if (!isWait)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed * Time.deltaTime);
        }

        if (transform.position == point.transform.position)
        {
            if (!isHidden)
            {
                point.transform.position =
                    new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                isHidden = true;
            }
            else
            {
                point.transform.position =
                    new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                isHidden = false;
            }

            isWait = true;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        isWait = false;
    }
}
