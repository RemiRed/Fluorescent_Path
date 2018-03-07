﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonStartRoom : Interractable
{
    [SerializeField]
    GameObject startRoomDoor;

    [SerializeField]
    RoomLoader roomLoader;

    [SerializeField]
    float doorDelay;

    bool activated = false;
    

    void Press()
    {
        if (activated)
        {
            return;
        }
        activated = true;
        StartCoroutine(OpenDoor());
        player.GetComponent<PlayerCommands>().CmdTest();
    }

    

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(doorDelay);
        startRoomDoor.SetActive(false);
        yield return null;
    }

}
