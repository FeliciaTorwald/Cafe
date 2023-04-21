using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderPopUpUI : MonoBehaviour
{

    [SerializeField] GameObject orderImage;
    [SerializeField] Transform parentSpawning;
    GameObject teaOrders;
    [SerializeField] OrderImages images;

    public void SpawnOrderImage()
    {

        teaOrders = Instantiate(orderImage, new Vector3(0, 0, 0), Quaternion.identity);
        teaOrders.transform.SetParent(parentSpawning.transform, false);

        //images.orderImg = Instantiate(images.orderImg, new Vector3(0, 0, 0), Quaternion.identity);
        //images.orderImg.transform.SetParent(parentSpawning.transform, false);
    }

    public void RemoveOrderImage()
    {
        Destroy(teaOrders);
    }
}



[CreateAssetMenu(fileName = "Order", menuName = "Order Image")]
public class OrderImages : ScriptableObject
{

    public TeaType TeaType;
    public GameObject orderImg;
}
