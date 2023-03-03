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
    public SListPath sListPaths;
    public static GameManager _this;
    private void Awake()
    {
        if (_this == null)
        {
            sListPaths= new SListPath();         
        }
    }
}


public struct SListPath
{
    public NativeParallelHashMap<int,SPath> sPaths ;
    public SListPath(int a)
    {
           sPaths = new NativeParallelHashMap<int,SPath>(0,Allocator.Persistent);
    }
    public void Dispose()
    {
        sPaths.Dispose();
    }

    public void AddPath(string key, SPath sp)
    {
        int count = key.Length;
        int id=0;
        for(int i=0;i<count;i++){
            id +=(int)key[i];
        }
        sPaths.Add(id, sp);
    }

     public SPath GetPath(string key)
    {
        int count = key.Length;
        int id=0;
        for(int i=0;i<count;i++){
            id +=(int)key[i];
        }
        return sPaths[id];
    }

}

public struct SPath
{
    public NativeList<SPoint> naPoints;
    public SPath(int a)
    {
        naPoints= new NativeList<SPoint>(0, Allocator.Persistent);
    }
    public void Dispose(){
       naPoints.Dispose();
    }
    public void AddPoints(SPoint p){
        naPoints.Add(p);
    }
}

public struct SPoint
{
    public int index;
    public float fx;
    public float fy;
}
