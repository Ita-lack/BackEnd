using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public void Coroutine(int id){
        StartCoroutine(ResquestServer(id));
    }
    public void CourotineUILoad(int id, TextMeshProUGUI textName, TextMeshProUGUI textPrice, Image image){
        StartCoroutine(UIRequestServerName(id, textName, textPrice, image));
    }
        private IEnumerator ResquestServer(int id){
        string url = "http://localhost/index.php?item_code=" + id + "&player_coins=" + player.GetMoney();
        Debug.Log(url);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
            string json = request.downloadHandler.text;
            ErrorResponse response = JsonUtility.FromJson<ErrorResponse>(json);
            if(response != null){
                HUDController.instance.ShowStatusMessage(response.error);
            }
            ItemData item = JsonUtility.FromJson<ItemData>(json);
            player.SetMoney(-item.price);
        }
    }
    
    private IEnumerator UIRequestServerName(int id, TextMeshProUGUI textName, TextMeshProUGUI textPrice, Image image){
        string url = "http://localhost/index.php?item_code=" + id;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
            string json = request.downloadHandler.text;
            ItemData item = JsonUtility.FromJson<ItemData>(json);
            textName.text = item.name;
            textPrice.text = "Price: " + item.price.ToString();
            Debug.Log(item.image);
            StartCoroutine(UIRequestServerImage(image, item.image));
        }
    }
    
    private IEnumerator UIRequestServerImage(Image image, string url){
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
            Texture2D tex = ((DownloadHandlerTexture) request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2.0f, tex.height / 2.0f));
            image.sprite = sprite;
        }
    }

    [System.Serializable]
    public class ItemData{
        public int id;
        public string name;
        public string image;
        public int price;
    }

    [System.Serializable]
    public class ErrorResponse{
        public string error; 
    }
}
