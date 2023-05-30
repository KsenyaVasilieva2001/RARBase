using System.Collections;
using ECS.Providers;
using UnityEngine;
/*
namespace ECS.Components
{
    public class PlayerCoroutineComponent : MonoBehaviour
    {

        IEnumerator OnHit()
        {
            if (GetComponent<HealthComponent>().isHit)
                GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.02f,
                    GetComponent<SpriteRenderer>().color.b - 0.02f); //вычислено как 1-0.4 / на кадров в секунду
            else
                GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.02f,
                    GetComponent<SpriteRenderer>().color.b + 0.02f);
            if (GetComponent<SpriteRenderer>().color.g == 1f) StopCoroutine(OnHit());

            if (GetComponent<SpriteRenderer>().color.g <= 0.4) GetComponent<HealthComponentProvider>().isHit = false;
            yield return new WaitForSeconds(0.01f); // Период корутины
            StartCoroutine(OnHit());
        }
    }
}
*/