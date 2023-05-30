using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Player Data")]
    public class PlayerInitData : ScriptableObject
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private float defaultSpeed;
        
       // public static PlayerInitData LoadFromAssets() => Resources.Load("Data/PlayerInitData") as PlayerInitData;
    }
}
