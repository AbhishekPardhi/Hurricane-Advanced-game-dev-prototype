using UnityEngine;

public class shooting : MonoBehaviour
{
    public Camera cam;
    public float Range=300f;
    public AudioSource shootaudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            shoot();
        }
    }
    void shoot(){
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,Range)){
            shootaudio.Play();
        }
    }
}
