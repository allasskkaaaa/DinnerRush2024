using UnityEngine;

public class NPC_SeatingState : NPC_BaseState
{
    private GameObject seat;
    private Seat seatScript; // Reference to the Seat component
    private SeatManager sm;

    public override void EnterState(NPCStateManager npc)
    {
        Debug.Log(npc.name + " has entered the seating state.");

        // Find the SeatManager
        sm = GameObject.FindWithTag("Seat Manager").GetComponent<SeatManager>();
        var (areAllOccupied, seatObject) = sm.CheckAvailability();

        if (areAllOccupied)
        {
            Debug.Log("All seats are occupied.");
        }
        else
        {
            // Assign the seat only to this NPC
            seat = seatObject;
            seatScript = seat.GetComponent<Seat>();

            if (seatScript != null)
            {
                seatScript.isOccupied = true; // Mark seat as occupied
                npc.npc.seat = seatScript;    // Assign the seat to the NPC
                Debug.Log(seat.name + " marked as occupied by " + npc.name);
            }
        }
    }

    public override void UpdateState(NPCStateManager npc)
    {
        if (seat != null)
        {
            // Move the NPC toward their assigned seat
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, seat.transform.position, npc.npc.walkSpeed * Time.deltaTime);

            // Switch to waiting state once seated
            if (Vector3.Distance(npc.transform.position, seat.transform.position) < 0.1f)
            {
                npc.SwitchState(npc.waitingState);
            }
        }
    }

    public override void OnCollisionEnter(NPCStateManager npc)
    {
        // Optional: Handle collision logic if needed
    }
}
