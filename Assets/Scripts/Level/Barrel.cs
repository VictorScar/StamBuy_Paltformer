using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IOnEventCallback
{
    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 25)
        {
            int barrelID = (int)photonEvent.CustomData;

            if (barrelID == gameObject.GetInstanceID())
            {
                Destroy(gameObject);
                //PhotonNetwork.D
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.PointCounter.AddPoint();

            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOption = new SendOptions { Reliability = true };

            PhotonNetwork.RaiseEvent(25, gameObject.GetInstanceID(), options, sendOption);
           
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
