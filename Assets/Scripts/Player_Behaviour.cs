using StarterAssets;
using TMPro;
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
    bool canInteract = false;
    /// <summary>
    /// Interact Message stuff
    /// </summary>
    [SerializeField]
    TextMeshProUGUI InteractMessage;
    /// <summary>
    /// Interact Message Game object
    /// </summary>
    [SerializeField]
    public GameObject InteractMessageState;
    WorriedNPC_Behaviour currentWorriedNPC = null;
    //Guard NPC interaction
    GuardNPC_Behaviour currentGuard = null;
    //ChillNPC interaction
    ChillingNPC_Behaviour currentChill = null;
    //ChillNPC interaction
    RunawayNPC_Behaviour currentRun = null;
    [SerializeField]
    public GameObject ConvoPanel;
    //Target Player Controller script to lock movement and camera rotation
    private FirstPersonController ControlFirstPerson;
    //Bool for knowing whether game is paused
    public bool isPaused = false;
    //Access PauseMenu behaviour
    public PauseMenu_Behaviour EscPressed;
    void Start()
    {
        SetCursorState(false);
    }
    void Awake()
    {
        ControlFirstPerson = GetComponent<FirstPersonController>();
    }


    void Update()
    {
        //Store info of Raycast hitting objects
        RaycastHit hitInfo;
        Debug.DrawRay(rayStart.position, rayStart.forward * interactionDistance, Color.red);
        //Will run if ray hits something
        if (Physics.Raycast(rayStart.position, rayStart.forward, out hitInfo, interactionDistance))
        {
            //Debug.Log("Interactable: " + hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.CompareTag("NPC"))
            {
                //Debug.Log("Collided with: " + hitInfo.collider.gameObject.name);
                InteractMessage.text = "[E] Interact";
                canInteract = true;
                currentWorriedNPC = hitInfo.collider.gameObject.GetComponent<WorriedNPC_Behaviour>();
            }
            else if (hitInfo.collider.gameObject.CompareTag("GuardNPC"))
            {
                InteractMessage.text = "[E] Interact";
                canInteract = true;
                currentGuard = hitInfo.collider.gameObject.GetComponent<GuardNPC_Behaviour>();
            }
            else if (hitInfo.collider.gameObject.CompareTag("ChillNPC"))
            {
                InteractMessage.text = "[E] Interact";
                canInteract = true;
                currentChill = hitInfo.collider.gameObject.GetComponent<ChillingNPC_Behaviour>();
            }
            else if (hitInfo.collider.gameObject.CompareTag("RunawayNPC"))
            {
                InteractMessage.text = "[E] Interact";
                canInteract = true;
                currentRun = hitInfo.collider.gameObject.GetComponent<RunawayNPC_Behaviour>();
            }
            else if (hitInfo.collider.gameObject.CompareTag("Untagged"))
            {
                ResetRaycast();
            }
        }
        else
        {
            ResetRaycast();
        }
    }
    private void ResetRaycast()
    {
        canInteract = false;
        currentWorriedNPC = null;
        currentGuard = null;
        currentChill = null;
        currentRun = null;
        InteractMessage.text = null;
    }
    public void OnInteract()
    {
        if (canInteract)
        {
            if (currentWorriedNPC != null)
            {
                StartConvo();
                currentWorriedNPC.LookAtPlayer();
            }
            else if (currentGuard != null)
            {
                ConversationStart();
                currentGuard.LookAtPlayer();
            }
            else if (currentChill != null)
            {
                ConversationStart();
                currentChill.WaitTillDeathTalk();
            }
            else if (currentRun != null)
            {
                ConversationStart();
                //currentRun.
            }
        }
    }
    public void StartConvo()
    {
        ConvoPanel.SetActive(true);
        SetMoveCamState(false);
        SetCursorState(true);
        InteractMessageState.SetActive(false);
        
    }
    public void EndConvo()
    {
        ConvoPanel.SetActive(false);
        SetMoveCamState(true);
        SetCursorState(false);
        InteractMessageState.SetActive(true);
    }
    public void ConversationStart()
    {
        SetMoveCamState(false);
        SetCursorState(true);
        InteractMessageState.SetActive(false);
    }
    public void ConversationEnd()
    {
        SetMoveCamState(true);
        SetCursorState(false);
        InteractMessageState.SetActive(true);
    }
    /// <summary>
    /// Lock player camera and position
    /// </summary>
    /// <param name="MoveCamState"></param>
    public void SetMoveCamState(bool MoveCamState)
    {
        //Lock Player movement
        ControlFirstPerson.canMove = MoveCamState;
        //Lock Camera movement
        ControlFirstPerson.CameraMove = MoveCamState;
    }
    /// <summary>
    /// Allow locking of cursor and setting visibility
    /// </summary>
    /// <param name="CursorState"></param>
    public void SetCursorState(bool CursorState)
    {
        if (CursorState == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
    public void OnPause()
    {
        //Debug.Log("Paused?");
        if (!isPaused)
        {
            EscPressed.PauseGame();
            isPaused = true;
            InteractMessageState.SetActive(false);
        }
        else
        {
            EscPressed.ResumeGame();
            isPaused = false;
            InteractMessageState.SetActive(true);
        }
    }
}
