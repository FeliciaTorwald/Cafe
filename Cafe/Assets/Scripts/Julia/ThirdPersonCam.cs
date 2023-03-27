using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    //[Header("Referemces")]
    public Transform orientation;
    public Transform Player;
    public Transform Player_Object;

    public Rigidbody rb;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate prientation
        Vector3 viewDir = Player.position - new Vector3(transform.position.x,Player.position.y,transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput+ orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
            Player_Object.forward = Vector3.Slerp(Player_Object.forward, inputDir.normalized,Time.deltaTime * rotationSpeed);

    }
}
