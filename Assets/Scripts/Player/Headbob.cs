using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    //[SerializeField] private Camera playerCamera;

    [SerializeField] float transitionSpeed = 20f; //smooths out the transition from moving to not moving.
    [SerializeField] float bobSpeed = 4.8f; //how quickly the player's head bobs.
    [SerializeField] float bobAmount = 0.1f; //how dramatic the bob is. Increasing this in conjunction with bobSpeed gives a nice effect for sprinting.
    [SerializeField] float runningBobSpeedMod = 1.6f;
    [SerializeField] float runningBobAmountMod = 1.6f;
    [SerializeField] Transform camTransform;

    private float timer = Mathf.PI / 2; //initialized as this value because this is where sin = 1. So, this will make the camera always start at the crest of the sin wave, simulating someone picking up their foot and starting to walk--you experience a bob upwards when you start walking as your foot pushes off the ground, the left and right bobs come as you walk.
    private CharacterController characterController;
    private PlayerController playerController;
    private Vector3 restPosition; //local position where your camera would rest when it's not bobbing.

    void Start()
    {
        restPosition = camTransform.localPosition;
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //walking bob
        if (!playerController.isRunning && characterController.isGrounded && playerController.movement != Vector2.zero) //moving
        {
            timer += bobSpeed * Time.deltaTime;

            //use the timer value to set the position
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)), restPosition.z); //abs val of y for a parabolic path
            camTransform.localPosition = newPosition;
        }

        //running bob
        else if(playerController.isRunning && characterController.isGrounded && playerController.movement != Vector2.zero)
        {
            timer += bobSpeed * runningBobSpeedMod * Time.deltaTime;

            //use the timer value to set the position
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount * runningBobAmountMod, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount * runningBobAmountMod)), restPosition.z); //abs val of y for a parabolic path
            camTransform.localPosition = newPosition;
        }
        else
        {
            timer = Mathf.PI / 2; //reinitialize

            Vector3 newPosition = new Vector3(Mathf.Lerp(camTransform.localPosition.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(camTransform.localPosition.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(camTransform.localPosition.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            camTransform.localPosition = newPosition;
        }

        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;
    }

    
}
