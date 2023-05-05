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
        if (NavMesh.SamplePosition(Dir, out Hit_, Radius, 1))
        {
            Final_Pos = Hit_.position;
        }
        return Final_Pos;
    }
}
