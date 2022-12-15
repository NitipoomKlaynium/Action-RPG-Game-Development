using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    CharacterController characterController;
    public Transform cameraTarget;
    public Animator animator;
    private float movementSpeed = 5.0f;
    private float gravity = 10f;
    private float maxFallingSpeed = -32.0f;
    private float jumpSpeed = 3.3f;
    private float turnRate = 90.0f;
    private float mouseSensitivity = 2.0f;
    private Vector3 moveMotion;
    private Vector3 turnMotion;
    private float desCharacterFace;
    private Vector2 mouseMotion;
    bool activeBox;
    public Transform door;
    bool isDoorOpen;

    // Start is called before the first frame update
    void Start() {
        characterController = GetComponent<CharacterController>();
        desCharacterFace = characterController.transform.eulerAngles.y;
        isDoorOpen = false;
    }

    void RotateY(float cameraFaceAngle, float characterFaceAngle) {
        characterController.transform.eulerAngles = new Vector3(0.0f, characterFaceAngle, 0.0f);
        cameraTarget.transform.eulerAngles = new Vector3(cameraTarget.transform.eulerAngles.x, cameraFaceAngle, 0.0f);
    }

    // Update is called once per frame
    void Update() {

        #region Character Control

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        mouseMotion.x = Input.GetAxis("Mouse X");
        mouseMotion.y = -Input.GetAxis("Mouse Y");

        cameraTarget.transform.eulerAngles += new Vector3(mouseMotion.y, mouseMotion.x, 0.0f) * mouseSensitivity;

        float cameraFace = cameraTarget.transform.eulerAngles.y;     
        
        float characterFace = characterController.transform.eulerAngles.y;
        
        if (Mathf.Abs(characterFace - desCharacterFace) > 6) {
            // Debug.Log("Face" + characterController.transform.eulerAngles.y);
            if (desCharacterFace > characterFace) {
                if (desCharacterFace - characterFace < 180) {
                    characterFace += 720 * Time.deltaTime;
                }
                else {
                    characterFace -= 720 * Time.deltaTime;
                }
            }
            else {
                if (characterFace - desCharacterFace < 180) {
                    characterFace -= 720 * Time.deltaTime;
                }
                else {
                    characterFace += 720 * Time.deltaTime;
                }
            }
            
            RotateY(cameraFace, characterFace);
            if (Mathf.Abs(characterFace - desCharacterFace) < 5) {
                RotateY(cameraFace, desCharacterFace);
            }
        }

        if (characterController.isGrounded) {
            moveMotion.y = 0;

            animator.SetBool("Jump", false);

            if (inputZ < 0) {
                inputZ *= 0.4f;
            }
            inputX *= 0.6f;            

            moveMotion.x = (Mathf.Sin(Mathf.Deg2Rad * cameraFace) * inputZ) + (Mathf.Cos(Mathf.Deg2Rad * cameraFace) * inputX);
            moveMotion.z = (Mathf.Cos(Mathf.Deg2Rad * cameraFace) * inputZ) - (Mathf.Sin(Mathf.Deg2Rad * cameraFace) * inputX);

            if (inputX != 0 || inputZ != 0) {                
                animator.SetFloat("Speed", 1);
            }      
            else {
                animator.SetFloat("Speed", 0);
            }
            
            if (Input.GetButtonDown("Jump")) {
                // Debug.Log("Jump");
                animator.SetBool("Jump", true);
                moveMotion.y = jumpSpeed;    
            }
        }
        else {
            if (moveMotion.y < maxFallingSpeed) {
                moveMotion.y = maxFallingSpeed;
            }
        }
        moveMotion.y -= gravity * Time.deltaTime;

        if (inputX != 0 || inputZ != 0) {
            float direction = Mathf.Atan2(moveMotion.x , moveMotion.z) * Mathf.Rad2Deg;
            desCharacterFace = (direction);
            // Debug.Log("Player walk to direction " + direction);
        }

        if (desCharacterFace > 360) {
            desCharacterFace -= 360;
        }
        else if (desCharacterFace < -0) {
            desCharacterFace += 360;
        }

        characterController.Move(moveMotion * movementSpeed * Time.deltaTime);
        transform.Rotate(turnMotion * Time.deltaTime);

        #endregion

        float doorAngle = door.transform.eulerAngles.y;
        if (isDoorOpen) {
            if (doorAngle != 0.0f) {
                Debug.Log(doorAngle);
                if (MathF.Abs(0.0f - doorAngle) < 2) {
                    door.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else {
                    door.transform.Rotate(new Vector3(0.0f, -45 * Time.deltaTime, 0.0f));
                }
            }
        }
        else {
            if (doorAngle != 90.0f) {
                if (MathF.Abs(90.0f - doorAngle) < 2) {
                    door.transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));    
                }
                else {
                    door.transform.Rotate(new Vector3(0.0f, 45 * Time.deltaTime, 0.0f));
                }
            }
        }
        
    }

    void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.F)) {
            if(other.tag == "Box") {
                Debug.Log(System.DateTime.Now + " : Interact with Box");
            }
            if(other.tag == "Barrel") {
                Debug.Log(System.DateTime.Now + " : Interact with Barrel");
            }
            if(other.tag == "WeaponRack") {
                Debug.Log(System.DateTime.Now + " : Interact with WeaponRack");
            }
            if(other.tag == "Door") {
                Debug.Log(System.DateTime.Now + " : Interact with Door" + isDoorOpen);
                Debug.Log(door.transform.rotation.y);
                isDoorOpen = !isDoorOpen;
            }
        }
    }

}
