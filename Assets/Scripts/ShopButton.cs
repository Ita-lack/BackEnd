using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{   
    [SerializeField]
    private int id;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI textName;
    [SerializeField]
    private TextMeshProUGUI textPrice;
    [SerializeField]
    private StoreManager store;
    private void Awake(){
        store.CourotineUILoad(id, textName, textPrice,  image);
    }
    public void ButtonClick(){
        store.Coroutine(id);
    }
    
}
