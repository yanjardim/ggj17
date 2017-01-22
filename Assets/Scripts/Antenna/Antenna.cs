using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour {
    public bool start;
    public bool active;

    public float angle = 1;
    public int power = 10;
    public int gap = 1;
    public int itensVisible = 0;
    public float rayCount = 1;
    public Vector3 dir;

    public LayerMask mask;

    private List<Antenna> oldVisibleObjs = new List<Antenna>();
    private List<Antenna> visibleObjs= new List<Antenna>();


    Quaternion startingAngle = Quaternion.AngleAxis(0, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

     void DetectThings()
    {
        RaycastHit hit; //variavel que recebe um objeto
        var angle = transform.rotation * Quaternion.AngleAxis(rayCount, Vector3.up);
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        //CERTO
        int  entradas= 0;
        foreach (Antenna g in visibleObjs)
        {
            if (!g.start)
            {
                g.active = false;
            }
           
        }
            visibleObjs.Clear();
        for (var i = 0; i < rayCount; i++) //faz os ray casts
        {
            Debug.DrawRay(pos, direction * power , Color.red); //desenha raios
            if (Physics.Raycast(pos, direction, out hit, power)) //faz raycast
            {
                var otherAntenna = hit.collider.GetComponent<Antenna>();    //verifica se uma antena foi atingida
                if (otherAntenna)
                {
                    bool repeated = false;
                    foreach(Antenna g in visibleObjs) //verifica todos os objetos vistos nesse loop
                    {
                        if(g == otherAntenna) //se 2 ou mais raios batem na mesma antena
                        {
                            repeated = true;    //repetiu
                        }
                    }
                
                    if (!repeated) //se não repetiu
                    {
                        entradas ++;
                        otherAntenna.active = true; //ativa a antena e insere na lista de antenas atingidas
                        visibleObjs.Add(otherAntenna);
                    }

                }

                
            }
            direction = stepAngle * direction; // prepara o próximo raycast
        }

 
    
        
            bool found = false;
            foreach (Antenna g in visibleObjs)
            {
                foreach (Antenna g1 in oldVisibleObjs)
                {
                    if(g == g1)
                    {
                        found = true;
                      
                    }
                }

                if(found == false)
                {
                    if (!g.start)
                    {
                        g.active = false; //desativa a antena
                    }
                    
                    g.oldVisibleObjs.Clear();
                    g.visibleObjs.Clear();
                    oldVisibleObjs.Remove(g);
                }
                    //se antena antiga não foi encontrada na lista atual

                    
                   
                   
                

                found = false; //supõe que o próximo elemento da lista anterior está na atual.

            }
       

        oldVisibleObjs = visibleObjs; //sobreescreve a lista antiga
     //   visibleObjs.Clear();
    }


    // Use this for initialization
    void Start () {
        dir = transform.forward * 1.5f * power;
        Ray r = new Ray(transform.position, dir);

 
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
            itensVisible = visibleObjs.Count;   // adquire o número de objetos vistos
            transform.rotation = Quaternion.Euler(0, angle, 0);

            power = gap <= 44 ? 10 : 10 - (gap / 45);

        }

        else if (!start)
        {
            foreach (Antenna a in visibleObjs)
            {
                if (!a.start)
                {
                    a.active = false;
                }

                a.oldVisibleObjs.Clear();
                a.visibleObjs.Clear();
            }

            foreach (Antenna a in oldVisibleObjs)
            {
                if (!a.start)
                {
                    a.active = false;
                }
                a.oldVisibleObjs.Clear();
                a.visibleObjs.Clear();
            }
           
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