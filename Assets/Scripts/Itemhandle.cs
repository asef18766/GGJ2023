using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itemhandle : MonoBehaviour
{

    public Text ItemName;
    public Image ItemImage;



    void Start()
    {

    }


    void Update()
    {

    }

    public void SetImage(Sprite sprite)
    {

        ItemImage.sprite = sprite;

    }

}
