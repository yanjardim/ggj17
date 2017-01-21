using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour {
    public bool start;
    public bool active;

    public float angle;
    public int power, gap;
     
    
    public List<Ray> rays = new List<Ray>();
    public Vector3 dir;

    public LayerMask mask;

    private Antenna currentAntenna;

    private int currentAngle, lastAngle;

    public float distance = 5.0f;
 public int theAngle = 45;
 public int segments = 10;
    // Use this for initialization
    void Start () {
        dir = transform.forward * 1.5f * 1;
        Ray r = new Ray(transform.position, dir);
        rays.Add(r);
        mask = 1 << LayerMask.NameToLayer("Antenna");
    }
	
	// Update is called once per frame
	void Update () {

        if (active)
        {
            dir = transform.forward * 1.5f * power;

            /*foreach(Ray r in rays)
            {

            }*/

            Debug.DrawRay(transform.position, dir, Color.red);



            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, mask))
            {
               
                if (hit.collider.gameObject != null)
                {
                    currentAntenna = hit.collider.GetComponent<Antenna>();
                    currentAntenna.active = true;
                }
            }
            else if(currentAntenna != null && !currentAntenna.start)
            {
                currentAntenna.active = false;
                currentAntenna = null;
            }


           
            transform.rotation = Quaternion.Euler(0, angle, 0);


            power = gap <= 44 ? 10 : 10 - (gap / 45);
            //RaycastSweep();
        }
        else if (currentAntenna != null && !currentAntenna.start)
        {
            currentAntenna.active = false;
            currentAntenna = null;
        }

        gap = Mathf.Clamp(gap, 0, 360);
        power = Mathf.Clamp(power, 1, 10);
        //RayManager();
        

    }
   

    public void RayManager()
    {
        //rays.Clear();
        int maxNumber = 10;
        int raysCount = gap / maxNumber; 



        

    }

    void RaycastSweep()
    {
        Vector3 startPos  = transform.position; // umm, start position !
        Vector3 targetPos  = Vector3.zero; // variable for calculated end position

        int startAngle = (int)(-theAngle * 0.5); // half the angle to the Left of the forward
        int finishAngle = (int)(theAngle * 0.5); // half the angle to the Right of the forward

        // the gap between each ray (increment)
        int inc = (int)(theAngle / segments);

        RaycastHit hit;

        // step through and find each target point
        for (int i = startAngle; i < finishAngle; i += inc ) // Angle from forward
        {
            targetPos = (Quaternion.Euler(0, i, 0) * transform.forward).normalized * distance;

            // linecast between points
            if (Physics.Linecast(startPos, targetPos, out hit))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);
            }

            // to show ray just for testing
            Debug.DrawLine(startPos, targetPos, Color.green);
        }
    }
}
