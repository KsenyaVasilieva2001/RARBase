using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp = 3;
    private bool isHit = false;
    [SerializeField] private GameManager manager;

    private Animator _animator;

    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    { 
        Debug.Log(curHp);
        CheckGround(); 
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
           _animator.SetInteger("State", 1);
        }
        else
        {
           Flip();
           if (isGrounded)
           {
               _animator.SetInteger("State", 2);
           }
        }
    }

    void FixedUpdate() //для физики
    {
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y); 
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           Jump(jumpHeight);
        }
        
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0,0,0); //если нажата клавиша вправо, то есть инпут = 1, то персонаж смотрит впрово, поворот на 0
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0,180,0); 
        }
    }

    protected internal void Jump(float height)
    {
        rb.AddForce(transform.up * height, ForceMode2D.Impulse);
    }

    void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y); 
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f); //добавляем все коллайдеры, которые попали в круг радиуса .2 и в центре groundcheck
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
        {
            _animator.SetInteger("State", 3);
        }
    }
    
    

    public void RecountHp(int deltaHp)
    {
        curHp += deltaHp;
        if (deltaHp < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        }
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke(nameof(Death), 1.5f);
        }
    }

    void Death()
    {
        manager.GetComponent<GameManager>().Lose();
    }

    IEnumerator OnHit()
    {
        if (isHit) 
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g -0.02f, GetComponent<SpriteRenderer>().color.b -0.02f); //вычислено как 1-0.4 / на кадров в секунду
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.02f, GetComponent<SpriteRenderer>().color.b + 0.02f);
        if(GetComponent<SpriteRenderer>().color.g == 1f) StopCoroutine(OnHit());
        
        if (GetComponent<SpriteRenderer>().color.g <= 0.4) isHit = false;
        yield return new WaitForSeconds(0.01f); // Период корутины
        StartCoroutine(OnHit());
    }
}
