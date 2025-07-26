using UnityEngine;

/*
*Authors: Richard Wong Zhi Hui, Ang Wei Siang Jeffrey, Tay Yi Ting, Geng BaiHui
*Date: 26/7/2025
*Description: Interactions of player with items and environment
*/
public class Player_Behaviour : MonoBehaviour
{
    //Set max interaction distance
    [SerializeField]
    float interactionDistance = 4f;
    //Allow setting of position for Raycast to start from
    [SerializeField]
    Transform rayStart;

    void Update()
    {
        //Store info of Raycast hitting objects
        RaycastHit hitInfo;
        Debug.DrawRay(rayStart.position, rayStart.forward * interactionDistance, Color.red);
        //Will run if ray hits something
        if (Physics.Raycast(rayStart.position, rayStart.forward, out hitInfo, interactionDistance))
        {
            //Debug.Log("Interactable: " + hitInfo.collider.gameObject.name);
        }
    }
}
