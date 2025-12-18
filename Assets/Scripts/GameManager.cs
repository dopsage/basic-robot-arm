
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RobotBehaviour robot;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Start()
    {
        // Touch the object by rotating arms down
        float lowRot        = -75.0F;
        float highRot       = -45.0F;
        float torchBaseRot  = -30.0F;
        robot.AddRotation(RobotPart.LOW_ARM,    0.0F, 2.0F, lowRot);
        robot.AddRotation(RobotPart.HIGH_ARM,   0.5F, 1.5F, highRot);
        robot.AddRotation(RobotPart.TORCH_BASE, 1.0F, 1.0F, torchBaseRot);
        
        // Rotate basis and return other parts to default state
        float basisRot = +45.0F;
        robot.AddRotation(RobotPart.BASIS,      2.5F, 1.00F, basisRot);
        robot.AddRotation(RobotPart.LOW_ARM,    2.5F, 1.75F, -1 * lowRot);
        robot.AddRotation(RobotPart.HIGH_ARM,   2.5F, 2.50F, -1 * highRot);
        robot.AddRotation(RobotPart.TORCH_BASE, 2.5F, 2.25F, -1 * torchBaseRot);
        
        // Rotate torch tip around
        float torchTipRot = +360.0F;
        robot.AddRotation(RobotPart.TORCH_TIP,  5.0F, 1.0F, torchTipRot);
        
        // Wait for a while by doing nothing
        robot.AddRotation(RobotPart.BASIS,      6.5F, 0.0F, 0.0F);
    }
    
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();
    }
}
