using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Generate_Random_Pos : MonoBehaviour
{
    public static Vector3 R_Pos(Vector3 start_Pos, float Radius)
    {
        Vector3 Dir = Random.insideUnitSphere * Radius;
        Dir += start_Pos;
        NavMeshHit Hit_;
        Vector3 Final_Pos = Vector3.zero;
        //1 är en mask som är 1an troligen i listan med area alltså "walkable" 2"Not walkable" 3."Jump"...
        //kollla sampelposition och ändra masken
        //eventuellt generera mask
        //skapa toma objecet med box coliders där bara bobagästen får gå
        //Debug.Log(NavMesh.GetAreaFromName("Bobatea_guest"));
        if (NavMesh.SamplePosition(Dir, out Hit_, Radius, -1))
        {
            Final_Pos = Hit_.position;
        }
        return Final_Pos;
    }
}
