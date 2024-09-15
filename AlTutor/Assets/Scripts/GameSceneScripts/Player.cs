using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private FirstPersonCamera firstPersonCamera;
    [SerializeField] private GameObject vrCameraObject;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private GameObject handPosition;

    /*
    [SerializeField] private GameObject raycastShootPoint;
    */
    private int layerMask_NPCLayer;
    void Start()
    {
        /*
        layerMask_NPCLayer = LayerMask.GetMask("PickableObjectLayer");
        */
    }

    // Update is called once per frame
    void Update()
    {

        LoadEveryFrame();
        HandlePlayerMovement();
        RestrictPlayerCoordination();
        /*
        InteractDetection();
        */
    }


    private void LoadEveryFrame()
    {
        firstPersonCamera = transform.Find("PlayerFirstPersonCamera").GetComponent<FirstPersonCamera>();
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forwardDir = new Vector3(0, 0, 0);
        if (GameSettings.GetGameInputMode() == 0)
        {
            forwardDir = firstPersonCamera.GetCameraForward();
        }
        else
        {
            forwardDir = vrCameraObject.transform.forward;
        }
        

        return forwardDir;
    }
    private void HandlePlayerMovement()
    {
        Vector3 cameraForwardDirection = GetCameraForward();
        Vector2 inputDirection = InputManager.GetMovementInput();

        //longitudinal movement
        Vector3 moveDir = new Vector3(0, 0, 0);
        moveDir += new Vector3(cameraForwardDirection.x, 0, cameraForwardDirection.z) * inputDirection[0];

        //widthwise movement
        Vector2 cameraForwardDirection_90DegreeRotated = RotationMatrix_270DegreeLeft(new Vector2(cameraForwardDirection.x, cameraForwardDirection.z));
        moveDir += new Vector3(cameraForwardDirection_90DegreeRotated.x, 0, cameraForwardDirection_90DegreeRotated.y) * inputDirection[1];

        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

    }

    private Vector2 RotationMatrix_270DegreeLeft(Vector2 vector)
    {

        float x = vector.y;
        float y = -vector.x;

        return new Vector2(x, y);
    }

    private void RestrictPlayerCoordination()
    {
        if (transform.position.x > 26 ||
            transform.position.x < -26 ||
            transform.position.y > 26 ||
            transform.position.y < -26
            ) transform.position = new Vector3(0, 0, 0);  
    }
    /*
    [SerializeField] private Transform objectGetRaycasted;
    private void InteractDetection()
    {
        //now only interact with GameObject in "NPCLayer"
        float detecttionMaxDistance = 10f;

        if (Physics.Raycast(raycastShootPoint.transform.position, firstPersonCamera.GetCameraForward(), out RaycastHit hit, detecttionMaxDistance, layerMask_NPCLayer))
        {
            //transform.root return the Top parent
            objectGetRaycasted = hit.transform.root;
        }
        else
        {
            objectGetRaycasted = null;
        }
    }
    

    public Transform GetRaycastedObject()
    {
        return objectGetRaycasted;
    }
    */
}
