using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour 
{
    //Access mode for texture
    [SerializeField]
    DepthTextureMode depthTextureMode;
    /// <summary>
    /// When Validating- activate texture mode
    /// </summary>
    private void OnValidate()
    {
        SetCameraDepthTextureMode();
    }
    /// <summary>
    /// On awake- activate texture mode
    /// </summary>
    private void Awake()
    {
        SetCameraDepthTextureMode();
    }
    /// <summary>
    /// Set camera to texture mode
    /// </summary>

    private void SetCameraDepthTextureMode()
    {
        GetComponent<Camera>().depthTextureMode = depthTextureMode;
    }
}
