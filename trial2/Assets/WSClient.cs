using System; // Add this line
using WebSocketSharp;
using UnityEngine;



public class WSClient : MonoBehaviour
{
    WebSocket ws;
    public Stone stoneScript; // Reference to the Stone script

    // Define an event for receiving step commands
    public event Action<int> OnStepCommandReceived;

public GameObject abc;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8081");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Raw message received2: " + e.Data);
 

            if (string.IsNullOrEmpty(e.Data))
            {
                Debug.LogWarning("Received empty message.");
                return;
            }

            int steps;
            if (int.TryParse(e.Data, out steps))
            {
                Debug.Log("Parsed steps: " + steps);
                stoneScript.MoveStone(steps);
                
            }
            else
            {
                Debug.LogError("Failed to parse steps from message: " + e.Data);
            }
        };
    }

    void Update()
    {
       
       
    
       
       
    
       
        if (ws == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ws.Send("Hello");
        }
    }
}
