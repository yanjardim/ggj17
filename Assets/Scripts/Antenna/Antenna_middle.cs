using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna_middle : MonoBehaviour {
    public bool starter;

    [Range(0f, 100f)]
	public float frequency; //hz
    [Range(1f,360f)]
	public float amplitude; //rad
	[Range(0f,100f)]
	public float potencia; //j

	public float anguloRecebido;
	public float anguloResposta; //configurado por fora
	public bool recebeuOnda;

	public float distanciaObstaculo;

	void Awake(){
		recebeuOnda = false;
		distanciaObstaculo = 1.5f;
	}

	void Start () {
		amplitude = 90;
		potencia = 0;
		frequency = 60;
	}
	
	// Update is called once per frame
	void Update () {
        

		if (Input.GetKey ("up") && amplitude < 360) { //se apertou algo, aumentar e diminuir amplitude
			amplitude += (int) Time.deltaTime;
		} else if (Input.GetKey ("down") && amplitude > 1) {
			amplitude -= (int) Time.deltaTime;
		}
		//TODO mudar esse numero quando mexer no tamanho do grid
		potencia = distanciaObstaculo*(1 - 1/amplitude);
		frequency = distanciaObstaculo*(1 - 1/amplitude);
	}

	void FixedUpdate() {
		if (recebeuOnda || starter) {


			Vector3 fwd = transform.TransformDirection(Vector3.right * anguloResposta);
			Debug.DrawRay (transform.position, fwd, Color.red);

			RaycastHit hit;
			if (Physics.Raycast (transform.position, fwd, out hit, potencia) &&  hit.transform.gameObject != null) {
				print ("bateu em algo");

				if (hit.transform.gameObject.tag == "Antenna_middle") {
					print(hit.transform.tag);
					//
					Antenna_middle scriptMiddle;
					scriptMiddle = hit.transform.GetComponent<Antenna_middle> ();
					scriptMiddle.recebeuOnda = true;
					scriptMiddle.anguloResposta = GetAnguloFinal (this.transform, hit.transform);

					print("Bateu no middle"); //substitui por acionar o angulo do outro objeto

				}
				if (hit.transform.tag == "Antenna_end"){
					Antenna_end scriptEnd;
					scriptEnd = hit.transform.GetComponent<Antenna_end> ();
					scriptEnd.faseFinalCompleted = true;
					print("Bateu no end"); 
				}

			}

			/**

			Vector3 direction = new Vector3 (0, Mathf.Deg2Rad * Time.deltaTime * anguloResposta, 0);
			Vector3 fwd = transform.TransformDirection(direction);

			if (Physics.Raycast (transform.position, fwd, potencia)) {
				if (transform.gameObject.tag == "Antenna_middle") {
					Antenna_middle scriptMiddle;
					scriptMiddle = GetComponent<Antenna_middle> ();
					scriptMiddle.recebeuOnda = true;
					scriptMiddle.anguloResposta = GetAnguloFinal (this.gameObject, transform.gameObject);

				
					print("Bateu no middle"); 
				}
				if (transform.gameObject.tag == "Antenna_end"){
					Antenna_end scriptEnd;
					scriptEnd = GetComponent<Antenna_end> ();
					scriptEnd.faseFinalCompleted = true;
					print("Bateu no end"); 
				}
					
			}

**/
		}

			
	}

	float GetAnguloFinal (Transform atual, Transform proximo){
		if (atual != null && proximo != null) {
			float catetoOposto;
			if (atual.position.x - proximo.position.x > 0)
				catetoOposto = atual.position.x - proximo.position.x;
			else
				catetoOposto = -(atual.position.x - proximo.position.x);


			Vector2 distanceCalculatedX = new Vector2 (atual.position.x, proximo.position.x);
			Vector2 distanceCalculatedZ = new Vector2 (atual.position.z, proximo.position.z);
			float hip = Vector2.Distance (distanceCalculatedX, distanceCalculatedZ);


			return Mathf.Asin(catetoOposto/hip);
		}
		return 0;
	}
}
