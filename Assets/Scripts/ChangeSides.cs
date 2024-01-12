using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ChangeSides: MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject[] sides;
    [SerializeField]
    private GameObject[] sides_Objects;

    private int sideIndex = 0;


    private void Awake ()
    {
        for (int i = 1; i < sides.Length; i++)
        {
            sides[i].SetActive(false);
            sides_Objects[i].SetActive(false);
        }

        StartCoroutine(SidesSwapped());
    }

    private void SideSwap()
    {
        sides[sideIndex].SetActive(false);
        sides_Objects[sideIndex].SetActive(false);

        sideIndex++;

        if (sideIndex >= sides.Length)
        {
            sideIndex = 0;
        }

        sides[sideIndex].SetActive(true);
        sides_Objects[sideIndex].SetActive(true);

        players[sideIndex].GetComponent<PlayerController>().isMoving = true;


    }

    private IEnumerator SidesSwapped()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            players[sideIndex].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            players[sideIndex].GetComponent<PlayerController>().isMoving = false;

            SideSwap();

            yield return null; 
        }
    }






}
