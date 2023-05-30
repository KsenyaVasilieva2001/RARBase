using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.SceneManagement;
using NotImplementedException = System.NotImplementedException;

namespace ECS.Systems
{
    public class SceneSystem : MonoBehaviour
    {
        public void RestartScene()
        {
            Invoke(nameof(LoseLevel), 0.5f);
        }
       
        void LoseLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}