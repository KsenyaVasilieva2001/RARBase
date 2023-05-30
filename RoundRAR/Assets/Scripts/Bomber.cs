using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] private GameObject bulletWorm;
    [SerializeField] private GameObject bulletFire;
    [SerializeField] private Transform shoot;
    [SerializeField] private float timeShootPeriod;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletWorm.AddComponent<Bullet>();
        bulletWorm.AddComponent<Rigidbody2D>();
        shoot.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(timeShootPeriod);
        //Shoot(GetBullet());
        Shoot(bulletFire);
        StartCoroutine(Shooting());
    }

    void Shoot(GameObject bullet)
    {
        Instantiate(bullet, shoot.transform.position, transform.rotation);
    }

    GameObject GetBullet()
    {
        GameObject[] bulletsTypes = new GameObject[2];
        bulletsTypes[0] = bulletFire;
        bulletsTypes[1] = bulletWorm;
        int rand = UnityEngine.Random.Range(0, 2);
        return bulletsTypes[rand];
    }
}
