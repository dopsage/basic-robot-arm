using UnityEngine;

public class FreeLook : MonoBehaviour
{
    public float sensitivity    = 200.0F;
    public float speed          = 3.0F;
    
    private Vector3 eulerRotation;
    
    void Awake()
    {
        eulerRotation = transform.eulerAngles;
    }
    
    void Update()
    {
        // Do camera rotation
        eulerRotation += new Vector3(-1 * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * sensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(eulerRotation);
        
        // Do camera movement
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;
        transform.position += (transform.right * moveDir.x + transform.forward * moveDir.z) * speed * Time.deltaTime;
    }
}
