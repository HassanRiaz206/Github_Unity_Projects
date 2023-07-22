using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject OtherCamera;

    private Camera mainCameraComponent;
    private Camera otherCameraComponent;

    private void Start()
    {
        // Get the Camera components from the MainCamera and OtherCamera game objects
        mainCameraComponent = MainCamera.GetComponent<Camera>();
        otherCameraComponent = OtherCamera.GetComponent<Camera>();

        // Initially, activate the MainCamera and deactivate the OtherCamera
        mainCameraComponent.enabled = true;
        otherCameraComponent.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Switch the camera states
            mainCameraComponent.enabled = !mainCameraComponent.enabled;
            otherCameraComponent.enabled = !otherCameraComponent.enabled;
        }
    }
}

