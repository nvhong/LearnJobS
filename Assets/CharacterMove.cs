using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        MoveJob moveJob = new MoveJob() {};// {_transform=transform};
        GameManager._this.jobs.Add(moveJob.Schedule());
    }


}

public struct MoveJob : IJob
{
    public Vector3 vPos;
    public void Execute()
    {
        var rd = new Unity.Mathematics.Random((uint)UnityEngine.Random.Range(1, 100000));

        float x = rd.NextFloat() * 3;
        float z = rd.NextFloat() * 3;
        vPos = new Vector3(x, 0, z) * Time.deltaTime;
    }
}
