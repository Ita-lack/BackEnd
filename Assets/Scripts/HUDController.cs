using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class HUDController : MonoBehaviour
{
   public static HUDController instance;
   public TextMeshProUGUI txtMoney;
   public TextMeshProUGUI txtStatusMessage;
   private Coroutine activeStatusMessageCoroutine;

    void Awake(){
        instance = this;
        txtStatusMessage.text = "";
    }

    public void UpdateMoney(int m){
        txtMoney.text = "Money: " + m;
    }

    public void ShowStatusMessage(string message, float duration = 1.0f)
    {
        if (activeStatusMessageCoroutine != null) StopCoroutine(activeStatusMessageCoroutine);
        activeStatusMessageCoroutine = StartCoroutine(ShowStatusMessageCoroutine(message, duration));
    }
    IEnumerator ShowStatusMessageCoroutine(string message, float duration){
        txtStatusMessage.text = message;
        yield return new WaitForSeconds(duration);
        txtStatusMessage.text = "";
    }
}
