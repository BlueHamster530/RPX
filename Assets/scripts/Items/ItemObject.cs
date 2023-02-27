using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    string ItemName;
    [SerializeField]
    bool bIsGetOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (bIsGetOnce)
            {
                print("NowGet");
                Destroy(gameObject);
            }
            else
            {
                other.GetComponent<PlayerKeyInput>().AddItemList(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bIsGetOnce)
            {
                print("NowGet");
            }
            else
            {
                other.GetComponent<PlayerKeyInput>().RemoveItemList(gameObject);
            }
        }
    }
    public void PrintName()
    {
        print(ItemName);
    }
}
