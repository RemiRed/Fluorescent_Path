﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
<<<<<<< HEAD
{
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carryDistance; //The distance between the player and the carried object
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carrySpeed; //The speed the object travels to "catch up" to a player after being stuck
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float slow; //Modifier for the force added to the object to simulate throwing

    GameObject carriedObject;
    Transform carriedObjectParent;
    GameObject lastInterractedObject;
    bool oldInterraction = false;
    bool keyUp = true;
    bool carrying = false;

    Vector3 oldPos; //Old position, used to calculate the force to emulate throwing
    Quaternion oldRot; //Old Rotation, used to stop the object from rotating


    float defaultDrag;
    private void Start() //saving the drag on the player component
=======
{ 
	GameObject carriedObject;							//The currently carried object 

	[SerializeField][Range(0.001f, int.MaxValue)]
    float carryDistance;								//The distance at which the player interacts with and carries objects 
	[SerializeField][Range(0.001f, int.MaxValue)]
    float normalizeSpeed;								//The speed carried objects return to a normalized position after being dislocated 
	[SerializeField][Range(0.001f, int.MaxValue)]
	float slow;											//Impacts force applied to carried object when dropped

	Vector3 oldPos;										//The carried objects previous position. Used to calculate position normalization and throwing 

	float defaultDrag;									//The players default drag. Used to reset a players movement speed after movement speed adjustments

	bool carrying = false;								//If the player is currently carrying an object

	private void Start()
>>>>>>> 55791796289248d34a93fbc06a434f3974eebc34
    {
		//Sets the defaultDrag variable
        defaultDrag = transform.parent.GetComponent<Rigidbody>().drag;
    }

    void FixedUpdate()
    {
<<<<<<< HEAD
        if (Input.GetAxisRaw("Interract") == 1) //Checks if the key has been pressed and picks up, interracts, or drops an object
        {
            if (!keyUp)
=======
		if (Input.GetButtonDown("Interract"))		
        {
            if (carrying)
            {
                Drop();
            }
     		else
>>>>>>> 55791796289248d34a93fbc06a434f3974eebc34
            {
                keyUp = false;
                if (carrying)
                {
                    Drop();
                }
                else
                {
                    Pickup();
                }
            }
            oldInterraction = Interraction();
        }
<<<<<<< HEAD
        else if (Input.GetAxisRaw("Interract") == 0) //checks for a key release and allows the player to press the button again
        {
            oldInterraction = false;
            keyUp = true;
        }

        if (carrying && carriedObject != null) //Returns the object close to the player should it get stuck
        {
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carriedObject.transform.localRotation = oldRot;
            oldPos = carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, transform.position + transform.forward * carryDistance, Time.deltaTime * carrySpeed);
=======

		//Updates to the carried object
        if (carrying && carriedObject != null)
        {
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			carriedObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			oldPos = carriedObject.transform.position 
			= Vector3.Lerp(carriedObject.transform.position, transform.position + transform.forward * (carryDistance + carriedObject.GetComponent<Collider>().bounds.extents.x), Time.deltaTime * normalizeSpeed);
>>>>>>> 55791796289248d34a93fbc06a434f3974eebc34
        }

        if (lastInterractedObject != null && oldInterraction)
        {

        }
    }

    void Pickup()
    {	
		//Defines a Raycast from camera view
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
<<<<<<< HEAD
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, carryDistance)) //Finds an object that's within carry distance 
        {
            if (hit.transform.tag == "Movable") //If the object is movable it starts moving the object around
            {
                carrying = true;
                carriedObject = hit.transform.gameObject;
                carriedObject.GetComponent<Rigidbody>().useGravity = false;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
                carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                carriedObjectParent = carriedObject.transform.parent;
                carriedObject.transform.parent = transform;
                oldRot = carriedObject.transform.localRotation;
                transform.parent.GetComponent<Rigidbody>().drag += carriedObject.GetComponent<Rigidbody>().mass;
                Physics.IgnoreCollision(carriedObject.GetComponent<Collider>(), GetComponentInParent<Collider>(), true);
            }

        }
    }

    bool Interraction()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, carryDistance)) //Finds an object that's within carry distance 
        {
            if (hit.transform.tag == "Interractable") //If the object is interractable, like a button, it'll interract with the object
            {
                lastInterractedObject = hit.transform.gameObject;
                lastInterractedObject.GetComponent<Interractable>().Interract();
                return true;
=======
		RaycastHit hit;

		//Determines if raycast hit an object
		if (Physics.Raycast (ray, out hit, carryDistance)) {
			//Determines what kind of relevant object the raycast hit
			if (hit.transform.tag == "Movable") {

				//sets changes to carry a movable object
				carrying = true;
				carriedObject = hit.transform.gameObject;
				carriedObject.transform.parent = transform;

				carriedObject.GetComponent<Rigidbody> ().useGravity = false;
				carriedObject.GetComponent<Rigidbody> ().freezeRotation = true;
				transform.parent.GetComponent<Rigidbody> ().drag += carriedObject.GetComponent<Rigidbody> ().mass;
				Physics.IgnoreCollision (carriedObject.GetComponent<Collider> (), GetComponentInParent<Collider> (), true);
			}
			//Calls method in interactible object if object is active
			else if (hit.transform.tag == "Interractable" && hit.transform.GetComponent<Interractable>().active)
            {
				hit.transform.GetComponent<Interractable>().Interract();
>>>>>>> 55791796289248d34a93fbc06a434f3974eebc34
            }

        }
        return false;
    }
<<<<<<< HEAD

    void Drop() //drops a held object
    {
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
<<<<<<< HEAD
        carriedObject.transform.parent = null;
        Physics.IgnoreCollision(carriedObject.GetComponent<Collider>(), GetComponentInParent<Collider>(), false);
        carriedObject = null;
=======
        carriedObject.transform.parent = carriedObjectParent;
>>>>>>> Networking
        transform.parent.GetComponent<Rigidbody>().drag = defaultDrag;
        carriedObject.GetComponent<Rigidbody>().AddForce((carriedObject.transform.position - oldPos) / (Time.deltaTime * slow));
        carriedObject = null;

=======
		
    void Drop()
    {
		//Resets changes to to no longer carry an object
		carriedObject.GetComponent<Rigidbody>().useGravity = true;
		carriedObject.GetComponent<Rigidbody>().freezeRotation = false; 
		transform.parent.GetComponent<Rigidbody>().drag = defaultDrag;
		Physics.IgnoreCollision(carriedObject.GetComponent<Collider>(), GetComponentInParent<Collider>(), false);

		carriedObject.GetComponent<Rigidbody>().AddForce((carriedObject.transform.position - oldPos) / (Time.deltaTime * slow));

		carriedObject.transform.parent = null; 
		carriedObject = null;
		carrying = false;
>>>>>>> 55791796289248d34a93fbc06a434f3974eebc34
    }
}