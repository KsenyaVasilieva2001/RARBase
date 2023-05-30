using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundPatrol : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool moveLeft = true;
    [SerializeField] private Transform groundDetect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);
        if (!groundInfo.collider)
        {
            Rotate();
        }
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
