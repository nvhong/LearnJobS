using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class TestJobs : MonoBehaviour
{
    [SerializeField] bool usingJobS;
    [SerializeField] GameObject gobCharacter;

    private void Update()
    {
        if(usingJobS)
        {
            NativeList<JobHandle> jobHandles = new NativeList<JobHandle>(Allocator.Temp);
            for(int i=0;i<1004;i++)
            {
                jobHandles.Add(new JobC().Schedule());
            }

            JobHandle.CompleteAll(jobHandles);
            jobHandles.Dispose();
        }
        else
        {
            for (int i = 0; i < 1004; i++)
            {
                Mathf.Sqrt((Mathf.Pow(10, 10000000000000f) / 1000000000000000f));
            }
        }

    }
}

public struct JobC : IJob
{

    public void Execute()
    {
        Mathf.Sqrt((Mathf.Pow(10, 10000000000000f) / 1000000000000000f));
    }
}
