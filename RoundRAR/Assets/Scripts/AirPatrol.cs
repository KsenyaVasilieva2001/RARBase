using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class AirPatrol : MonoBehaviour
{
     [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private bool canFly = true;
    private bool moveLeft = true;
    void Start()
    {
        gameObject.transform.position = new Vector3(point1.transform.position.x, point1.transform.position.y,
            transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(canFly) Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
        if (transform.position == point1.position)
        {
            (point1, point2) = (point2, point1);
            Rotate();
            canFly = false;
            StartCoroutine(Waiting());
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        canFly = true;
    }

     void Rotate()
    {
        if (moveLeft)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            moveLeft = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveLeft = true;
        }
    }
}
