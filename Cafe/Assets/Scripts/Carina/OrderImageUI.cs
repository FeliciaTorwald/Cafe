using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderImageUI : MonoBehaviour
{
    [SerializeField] GameObject orderImage1;
    [SerializeField] GameObject orderImage2;
    [SerializeField] Transform parentSpawning;
    [SerializeField] private List<teaOrderTicket> orderList = new();
    
    
    
    public void SpawnOrderImage(TeaType teaType, Guest guestRef)
    {
        GameObject orderImage = new GameObject();
        GameObject teaOrders;
        switch (teaType)
        {
            case TeaType.TypeA:
                orderImage = orderImage1;
                orderImage.GetComponent<teaOrderTicket>().teaType = teaType;
                orderImage.GetComponent<teaOrderTicket>().guestRef = guestRef;
                teaOrders = Instantiate(orderImage, new Vector3(0, 0, 0), Quaternion.identity);
                teaOrders.transform.SetParent(parentSpawning.transform, false);
                orderList.Add(teaOrders.GetComponent<teaOrderTicket>());
                break;
            
            case TeaType.TypeB:
                orderImage = orderImage2;
                orderImage.GetComponent<teaOrderTicket>().teaType = teaType;
                orderImage.GetComponent<teaOrderTicket>().guestRef = guestRef;
                teaOrders = Instantiate(orderImage, new Vector3(0, 0, 0), Quaternion.identity);
                teaOrders.transform.SetParent(parentSpawning.transform, false);
                orderList.Add(teaOrders.GetComponent<teaOrderTicket>());
                
                break;
        }

        // teaOrders = Instantiate(orderImage, new Vector3(0, 0, 0), Quaternion.identity);
        // teaOrders.transform.SetParent(parentSpawning.transform, false);

        //images.orderImg = Instantiate(images.orderImg, new Vector3(0, 0, 0), Quaternion.identity);
        //images.orderImg.transform.SetParent(parentSpawning.transform, false);
    }

    public void RemoveOrderImage(teaOrderTicket ticket)
    {
        orderList.Remove(ticket);
        ticket.RemoveTicket();
    }
}

[CreateAssetMenu(fileName = "Order", menuName = "Order Image")]
public class OrderImages : ScriptableObject
{
    public TeaType TeaType;
    public Image orderImg;
}
