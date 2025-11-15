using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public void Coroutine(int id){
        StartCoroutine(ResquestServer(id));
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

    [System.Serializable]
    public class ItemData{
        public int id;
        public string name;
        public string description;
        public int price;
    }

    [System.Serializable]
    public class ErrorResponse{
        public string error; 
    }
}
