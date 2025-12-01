using Shop.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shop.Data
{
    [CreateAssetMenu(fileName = "ShopResources", menuName = "Custom/ShopSystem/Resources", order = 0)]
    public class ShopResources : ScriptableObject
    {
        [field: SerializeField] public ShopProductCompactView ShopProductViewPrefab { get; private set; }
        [field: SerializeField] public ShopView ShopViewPrefab { get; private set; }
        [field: SerializeField] public ShopProductSO[] BuiltInProducts { get; private set; }
        [field: SerializeField] public int ShopCardSceneBuildIndex { get; private set; }
    }
}