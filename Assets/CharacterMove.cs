using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Burst;

public class CharacterMove : MonoBehaviour
{
    public static CharacterMove _this;


    [SerializeField] bool isJobs;
    [SerializeField] public List<Transform> aTransforms1;
    [SerializeField] public Transform[] aTransforms;
    public TransformAccessArray aaTransforms;
    public NativeList<Vector3> velocity;
    NativeList<Vector3> currPos;

    NativeParallelHashMap<int, int> hmapIndex;
    Unity.Mathematics.Random uRandom;

    private void Awake()
    {
        _this = this;
        aTransforms1 = new List<Transform>();
        aaTransforms = new TransformAccessArray(aTransforms);
        velocity = new NativeList<Vector3>(aTransforms.Length, Allocator.Persistent);
        currPos = new NativeList<Vector3>(aTransforms.Length, Allocator.Persistent);

        hmapIndex = new NativeParallelHashMap<int, int>(aTransforms.Length, Allocator.Persistent);
        for (int i = 0; i < velocity.Length; i++)
        {
            velocity[i] = new Vector3(-1, 0, 1);
        }
        Application.runInBackground = true;
        uRandom = new Unity.Mathematics.Random(0x6E624EB7u);
        //Application.targetFrameRate = 60;
    }

    public void AddTransform(Transform tr)
    {
        aaTransforms.Add(tr);
        velocity.Add(new Vector3(-1, 0, 1));
        hmapIndex.Add(aaTransforms.length - 1, tr.gameObject.GetInstanceID());
        int length = velocity.Length;
        for (int i = 0; i < length; i++)
        {
            float x = uRandom.NextFloat(-20, 20);
            float z = uRandom.NextFloat(-20, 20);
            velocity[i] = new Vector3(x, 0, z);
        }
    }

    public void RemoveTransform(Transform tr)
    {
        aaTransforms.Add(tr);
        velocity.Add(new Vector3(-1, 0, 1));
        hmapIndex.TryGetValue(tr.gameObject.GetInstanceID(), out int index);
        velocity.RemoveAt(index);
        aaTransforms.RemoveAtSwapBack(index);
    }

    public void ChangeOut(int index)
    {
    }

    private void OnDestroy()
    {
        aaTransforms.Dispose();
        velocity.Dispose();
        hmapIndex.Dispose();
    }
    int frameCount;

    private void Update()
    {
        if (isJobs)
        {
            var job = new MoveJob()
            {
                deltaTime = Time.deltaTime,
                velocity = velocity
            };
            JobHandle jobHandle = job.Schedule(aaTransforms);
            jobHandle.Complete();
        }
        else
        {
            for (int i = 0; i < aaTransforms.length; i++)
            {
                var pos = aaTransforms[i].position;
                pos += new Vector3(-1, 0, 1) * Time.deltaTime;
                aaTransforms[i].position = pos;
            }
        }
    }

    [BurstCompile]
    public struct MoveJob : IJobParallelForTransform
    {
        [ReadOnly]public NativeList<Vector3> velocity;
        public float deltaTime;
        public int index1;
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += velocity[index] * deltaTime;
            if (transform.position.x < -50)
            {
                transform.position=Vector3.zero;
            }
        }
    }


    public struct CalSpeedJob : IJob
    {
        public NativeList<Vector3> velocity;
        public void Execute()
        {
        }
    }
}


