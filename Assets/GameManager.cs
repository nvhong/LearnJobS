using System.Drawing;
using System.IO;
using System.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
using System;
using Unity.Burst;

public class GameManager : MonoBehaviour
{
    public Unity.Collections.NativeList<JobHandle> jobs;
    public static GameManager _this;
    private void Awake()
    {
        if (_this == null)
        {
            // _this = this;
            // jobs = new NativeList<JobHandle>(1000, Allocator.TempJob);
            // JobHandle.CompleteAll(jobs);
        }
    }
}


public struct SListPath
{
    public NativeParallelHashMap<int,SPath> sPaths ;
    public SListPath()
    {
           sPaths = new NativeParallelHashMap<int,SPath>(0,Allocator.Persistent);
    }
    public void Dispose()
    {
        sPaths.Dispose();
    }
}

public struct SPath
{
    public NativeHashMap<int, int> naPoints;
    public SPath()
    {
        naPoints= new NativeHashMap<int, int>();
       // naPoints = new NativeArray<SPoint>(0, Allocator.Persistent);
    }
    // public void Dispose(){
    //    // naPoints.Dispose();
    // }
    // public void AddPoints(SPoint p){
    //     naPoints.Add(p);
    // }
}

public struct SPoint
{
    public int index;
    public float fx;
    public float fy;
}
