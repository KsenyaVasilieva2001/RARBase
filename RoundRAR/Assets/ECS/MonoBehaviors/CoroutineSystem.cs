using System.Collections;
using ECS.Components;
using ECS.Providers;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Unity.VisualScripting;
using UnityEngine;

namespace ECS.Systems
{
    public class CoroutineSystem : MonoBehaviour
    {
       // public EcsPool<HealthComponent> healthPool;
       // public int playerEntity;
       /*
      public IEnumerator OnHit()
      {
          /*
       //   if (healthPool.Get(playerEntity).isHit)
              GetComponent<SpriteRenderer>().color = new Color(1f, GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color.g - 0.02f,
                  GetComponent<SpriteRenderer>().color.b - 0.02f); //вычислено как 1-0.4 / на кадров в секунду
          else
              GetComponent<SpriteRenderer>().color = new Color(1f, GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color.g + 0.02f,
                  GetComponent<SpriteRenderer>().color.b + 0.02f);
          if (GetComponent<SpriteRenderer>().color.g == 1f) StopCoroutine(OnHit());

          if (GetComponent<SpriteRenderer>().color.g <= 0.4) healthPool.Get(playerEntity).isHit = false;
          yield return new WaitForSeconds(0.01f); // Период корутины
          StartCoroutine(OnHit());
        }
    */
        public void StartCoroutineOnHit()
        {
            GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color = new Color(1f, GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color.g - 0.02f,
                GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().color.b - 0.02f);
         //   Debug.Log(healthPool.Get(playerEntity));
           // StartCoroutine(OnHit());
        }
        public void StopCoroutineOnHit()
        {
           // StopCoroutine(OnHit());
        }
    }
}