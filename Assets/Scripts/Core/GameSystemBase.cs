using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public abstract class GameSystemBase: MonoBehaviour
    {
        public abstract UniTask InitializeSystem();
    }
}