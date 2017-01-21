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
    private List<GameObject> visibleObjs= new List<GameObject>();

    Quaternion startingAngle = Quaternion.AngleAxis(0, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

     void DetectThings()
    {
        RaycastHit hit;
        var angle = transform.rotation * Quaternion.AngleAxis(rayCount, Vector3.up);
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < rayCount; i++)
        {
            Debug.DrawRay(pos, direction * power , Color.red);
            if (Physics.Raycast(pos, direction, out hit, 500))
            {
                var otherAntenna = hit.collider.GetComponent<Antenna>();
                if (otherAntenna)
                {
                    visibleObjs.Add(otherAntenna.gameObject);
                    otherAntenna.gap = 1;
                    otherAntenna.power = 10;
                    otherAntenna.active = true;

                }
            }
            direction = stepAngle * direction;
        }
    }


    // Use this for initialization
    void Start () {
        dir = transform.forward * 1.5f * power;
        Ray r = new Ray(transform.position, dir);

        rays.Add(r);
        mask = 1 << LayerMask.NameToLayer("Antenna") | LayerMask.NameToLayer("Glass");
    }

    // Update is called once per frame
    void Update()
    {

        if (active)
        {
            dir = transform.forward * 12.5f * power;


            RayManager();
            DetectThings();

            transform.rotation = Quaternion.Euler(0, angle, 0);

            power = gap <= 44 ? 10 : 10 - (gap / 45);

        }
        else if (currentAntenna != null && !currentAntenna.start)
        {
            currentAntenna.active = false;
            currentAntenna = null;
        }

        gap = Mathf.Clamp(gap, 1, 360);
        power = Mathf.Clamp(power, 1, 10);

        }
 
   

    public void RayManager()
    {

        rayCount = gap / 5;
        if (gap % 5 != 0 && gap >= 1) {
            rayCount = (int) Mathf.Ceil(rayCount) + 1;
        }
        
       
    

    }
    
   

}