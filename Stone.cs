using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;

public class Stone : MonoBehaviour
{
    // Reference to WSClient
    public WSClient wsClient;

    public Route currentRoute;
    int routePosition;
    public int steps;
    bool isMoving;

    void Start()
    {
        // Subscribe to the OnStepCommandReceived event of WSClient
        if (wsClient != null)
        {
            wsClient.OnStepCommandReceived += MoveStone;
        }
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) MoveStone(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) MoveStone(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) MoveStone(3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) MoveStone(4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) MoveStone(5);
            if (Input.GetKeyDown(KeyCode.Alpha6)) MoveStone(6);
        }
    }

    public void MoveStone(int keyPressCount)
    {
        Debug.Log("Moving stone " + keyPressCount + " steps.");
        if (routePosition + keyPressCount < currentRoute.childNodeList.Count)
        {
            steps = keyPressCount;
            StartCoroutine(Move());
        }
        else
        {
            Debug.Log("Number is too high");
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }

        isMoving = false;
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }

    public void OnReceiveMessageFromJS(string message)
    {
        int result;
        if (int.TryParse(message, out result) && result >= 0 && result <= 6)
        {
            steps = result;
            if (!isMoving)
            {
                StartCoroutine(Move());
            }
        }
    }
}
