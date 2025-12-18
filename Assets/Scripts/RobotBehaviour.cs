
using System.Collections.Generic;
using UnityEngine;

public enum RobotPart
{
    BASIS,
    LOW_ARM,
    HIGH_ARM,
    TORCH_BASE,
    TORCH_TIP
}

public class RobotBehaviour : MonoBehaviour
{
    public const int PARTS_COUNT = 5;
    
    private float                               farestTimePoint;
    private float                               localTime;
    private Transform[]                         parts;
    private Dictionary<RobotPart, List<float>>  rotations;
    
    [Header("Part references")]
    [SerializeField] private Transform basis;
    [SerializeField] private Transform lowArm;
    [SerializeField] private Transform highArm;
    [SerializeField] private Transform torchBase;
    [SerializeField] private Transform torchTip;
    
    private void Awake()
    {
        farestTimePoint = 0.0F;
        localTime       = 0.0F;
        parts           = new Transform[PARTS_COUNT] { basis, lowArm, highArm, torchBase, torchTip };
        rotations       = new Dictionary<RobotPart, List<float>>();
        
        // Initialize timelines for each robot part
        rotations.Add(RobotPart.BASIS,      new List<float>() { 0.0F });
        rotations.Add(RobotPart.LOW_ARM,    new List<float>() { 0.0F });
        rotations.Add(RobotPart.HIGH_ARM,   new List<float>() { 0.0F });
        rotations.Add(RobotPart.TORCH_BASE, new List<float>() { 0.0F });
        rotations.Add(RobotPart.TORCH_TIP,  new List<float>() { 0.0F });
    }

    private void Update()
    {
        // Cycle the local animation time
        if(localTime > farestTimePoint)
            localTime = 0.0F;
        
        // Run through all part timelines and do rotations according to them
        for(int p = 0; p < PARTS_COUNT; p++)
        {
            List<float> rotationData    = rotations[(RobotPart)p];
            int         rotationCount   = (int)rotationData[0];
            
            int ri = 1;
            while(ri < rotationCount * 3)
            {
                float t0    = rotationData[ri++];
                float d     = rotationData[ri++];
                float a     = rotationData[ri++];
                
                // Part is rotated by exact angle when it is its animation time
                if(d > 0.0F && localTime > t0 && localTime < t0 + d)
                    parts[p].localEulerAngles += Vector3.forward * (a / d) * Time.deltaTime;
            }
        }
        
        localTime += Time.deltaTime;
    }
    
    /* Rotates the robot part `part` along its rotation axis by `a` degrees in
     * `d` seconds with delay of `t0` seconds relative to the local animation time.  */
    public void AddRotation(RobotPart part, float t0, float d, float a)
    {
        // Update the `part` rotation counter and timeline
        rotations[part][0]++;
        rotations[part].AddRange(new float[3] { t0, d, a });
        
        // Farest time point is the total duration of one animation cycle.
        float endTimePoint = t0 + d;
        if(endTimePoint > farestTimePoint)
            farestTimePoint = endTimePoint;
    }
}
