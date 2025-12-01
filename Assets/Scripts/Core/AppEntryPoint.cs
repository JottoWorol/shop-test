using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class AppEntryPoint: MonoBehaviour
    {
        [SerializeField] private GameSystemBase[] gameSystemsInitOrder;
        
        private PlayerData playerData;
        
        private void Awake()
        {
            playerData = new PlayerData();

            InitializeSystems().Forget(OnException);
        }

        /// <summary>
        /// Initializing all systems, ensuring specific order defined in the scene
        /// </summary>
        private async UniTask InitializeSystems()
        {
            foreach (var system in gameSystemsInitOrder)
            {
                await system.InitializeSystem();
            }
        }

        private void OnException(Exception e)
        {
            Debug.LogError($"Exception during systems initialization: {e}");
        }
    }
}