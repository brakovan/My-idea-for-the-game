using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;

    private Vector3 playerPos;

    [SerializeField]
    private float cameraMoveSpeed = 5f;
    [SerializeField]
    private float cameraPosZ = -10f;
    
    private int playerIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerIndex++;

            if (playerIndex >= players.Length)
            {
                playerIndex = 0;
            }
        }

        activePlayerPos();

        transform.position = Vector3.Lerp(transform.position, playerPos, cameraMoveSpeed * Time.deltaTime);

    }

    private Vector3 activePlayerPos()
    {
        return playerPos = new Vector3(players[playerIndex].transform.position.x, players[playerIndex].transform.position.y, cameraPosZ);
    }
}
