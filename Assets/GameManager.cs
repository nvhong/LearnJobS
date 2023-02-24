using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NativeList<JobHandle> jobs;
    public static GameManager _this;
    private void Awake()
    {
        if(_this==null)
        {
            _this = this;
            jobs = new NativeList<JobHandle>(1000, Allocator.TempJob);
        }
    }


    void Update()
    {
        JobHandle.CompleteAll(jobs);
    }
}
