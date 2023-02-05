using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public bool isDefeated;
    public bool isAvailable;
    public Button yourButton;

    [SerializeField] private GameObject[] preRequest;

    private void Start()
    {
        isDefeated = false;
        isAvailable = false;
        //GetComponent<SpriteRenderer>().color = Color.red;
        GameObject.Find("Computer 0").GetComponent<Image>().color = Color.red;
        GameObject.Find("Computer 0").GetComponent<Computer>().isAvailable = true;
        
        Button btn = yourButton.GetComponent<Button>();
    }
    
    private void Update()
    {
        if (GetComponent<Computer>().isDefeated != true && 
            GetComponent<Computer>().isAvailable != true)   //Set every computers to red
        {
            GetComponent<Image>().color = Color.red;
            for (int i = 0; i < preRequest.Length; i++)
            {
                if (preRequest[preRequest.Length - 1].GetComponent<Computer>().isDefeated == true)
                {
                    if (preRequest[i].GetComponent<Computer>().isDefeated == true)
                    {
                        GetComponent<Computer>().isAvailable = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        
        if (GetComponent<Computer>().isDefeated == true && 
            GetComponent<Computer>().isAvailable == true)
        {
            for (int i = 0; i < preRequest.Length; i++)
            {
                preRequest[i].GetComponent<Computer>().isAvailable = false;
            }
            GetComponent<Image>().color = Color.blue;
        }
    }

    public void isDefeatedStatusChanged()
    {
        if (GetComponent<Computer>().isDefeated != true && GetComponent<Computer>().isAvailable == true)
        {
            GetComponent<Computer>().isDefeated = true;
            yourButton.interactable = false;
        }
    }
}
