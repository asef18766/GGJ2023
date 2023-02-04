using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shopmgr : MonoBehaviour
{

    public GameObject ItemPrefab;
    public GameObject ContentPool;

    void Start()
    {

        for (int i = 0; i < 10; i++)
        {

            GameObject temp = Instantiate(ItemPrefab);
            temp.transform.parent = ContentPool.transform;
            //temp.transform.localPosition = Vector3.zero;
            temp.transform.localEulerAngles = Vector3.zero;
            temp.transform.localScale = Vector3.one;
            Itemhandle handler = temp.GetComponent<Itemhandle>();



            int _randomIndex = Random.Range(0, GameDB.res.itemList.Count);
            handler.SetImage(GameDB.res.itemList[_randomIndex]);

            int temp_I = i;
            temp.GetComponent<Button>().onClick.AddListener(
            () => { OnItemClick(i); });

        }
    }

    void Update()
    {

    }

    public void OnItemClick(int iteamId)
    {

        Debug.Log(iteamId);

    }

}
