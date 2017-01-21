using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour {
    public bool start;
    public bool active;

    public float angle;
    public int power = 10;
    public int gap = 1;

    public float rayCount = 1;
    public List<Ray> rays = new List<Ray>();
    public Vector3 dir;

    public LayerMask mask;

    private Antenna currentAntenna;

    // Use this for initialization
    void Start () {
       // dir = transform.forward * 1.5f ;
        Ray r = new Ray(transform.position, dir);

        rays.Add(r);
        mask = 1 << LayerMask.NameToLayer("Antenna");
    }
	
	// Update is called once per frame
	void Update () {

        if (active)
        {
            dir = transform.forward * 12.5f * power;
            List<Ray> raysTemp = new List<Ray>();

            for (int i = 0; i < rays.Count; ++i) 
            {
                Ray r = rays[i];
                r.origin = transform.position;
                //  raysTemp.Add(r);
                  
                Debug.DrawRay(r.origin, r.direction*power, Color.red);
            }
            rays.Clear();
          //  Debug.DrawRay(transform.position, dir, Color.red);



            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir * 1000, out hit, mask))
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

        }
        else if (currentAntenna != null && !currentAntenna.start)
        {
            currentAntenna.active = false;
            currentAntenna = null;
        }

        gap = Mathf.Clamp(gap, 1, 360);
      //  power = Mathf.Clamp(power, 1, 10);
        RayManager();

    }
   

    public void RayManager()
    {

        rayCount = gap / 5;
        if (gap % 5 != 0 && gap >= 1) {
            rayCount = (int) Mathf.Ceil(rayCount) + 1;
        }

        while (rays.Count < rayCount)
        { //se preciso de mais rays
            Ray r = new Ray(transform.position, dir * 1000);
            r.direction = Quaternion.AngleAxis(5 * Mathf.Ceil(rays.Count / 2), Vector3.up) * r.direction;
            Ray r2 = new Ray(transform.position, dir * 1000);
            r2.direction = Quaternion.AngleAxis(-5 * Mathf.Ceil(rays.Count / 2), Vector3.up) * r2.direction;
            rays.Add(r);
            rays.Add(r2);

        }

        while (rays.Count > rayCount && rays.Count >=3) //se preciso de menos rays
        {
            rays.RemoveAt(rays.Count-1);
            rays.RemoveAt(rays.Count-1);
        }

    }

   
}
