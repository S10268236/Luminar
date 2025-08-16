using UnityEngine;

public class NormalsReplacementShader : MonoBehaviour
{
    //Input for normal shaders
    [SerializeField]
    Shader normalsShader;
    //Render texture input
    private RenderTexture renderTexture;
    //New camera set
    private new Camera camera;

    /// <summary>
    /// Adjust camera to render textures
    /// </summary>
    private void Start()
    {
        Camera thisCamera = GetComponent<Camera>();

        // Create a render texture matching the main camera's current dimensions.
        renderTexture = new RenderTexture(thisCamera.pixelWidth, thisCamera.pixelHeight, 24);
        // Surface the render texture as a global variable, available to all shaders.
        Shader.SetGlobalTexture("_CameraNormalsTexture", renderTexture);

        // Setup a copy of the camera to render the scene using the normals shader.
        GameObject copy = new GameObject("Normals camera");
        camera = copy.AddComponent<Camera>();
        camera.CopyFrom(thisCamera);
        camera.transform.SetParent(transform);
        camera.targetTexture = renderTexture;
        camera.SetReplacementShader(normalsShader, "RenderType");
        camera.depth = thisCamera.depth - 1;
    }
}
