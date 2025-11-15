using UnityEngine;
using UnityEngine.UI;
public class ShopButton : MonoBehaviour
{   
    [SerializeField]
    private int id;

    [SerializeField]
    private StoreManager store;

    public void ButtonClick(){
        store.Coroutine(id);
    }
}
