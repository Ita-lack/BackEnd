using UnityEngine;

public class Player : MonoBehaviour
{
    private int playerMoney = 1000;

    private void Start(){
        playerMoney = PlayerPrefs.GetInt("money", 1000);
        HUDController.instance.UpdateMoney(playerMoney);
    }
    public int GetMoney(){
        return playerMoney;
    }
    public void SetMoney(int money){
        playerMoney += money; 
        PlayerPrefs.SetInt("money", money);
        HUDController.instance.UpdateMoney(playerMoney);

    }
}
