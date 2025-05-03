using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 6;
    private float maxSpeed = 13;
    private float maxTorque = 2;
    private float xRange = 4;
    private float ySpawnPos = -2;
    private SpanwerPool spanwerPool;
    [SerializeField] int pointValue;
    [SerializeField] ParticleSystem explosion;
  

     void OnEnable() {
        if (targetRb == null) {
            targetRb = GetComponent<Rigidbody>();
            spanwerPool = FindAnyObjectByType<SpanwerPool>();
        }

        // Reinicia la física ANTES de aplicar fuerza
        targetRb.linearVelocity = Vector3.zero;
        targetRb.angularVelocity = Vector3.zero;

        // Configura posición y movimiento
        transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }


    private void OnMouseDown()
    {
       if(spanwerPool.isGameActive){
          spanwerPool.DesactivatePoolBox(gameObject);
          spanwerPool.UpdateScore(pointValue);
          Instantiate(explosion, transform.position,transform.rotation);
       }
    }


    private Vector3 RandomForce(){
     return Vector3.up * Random.Range(minSpeed,maxSpeed);
   }

   private float RandomTorque(){
     return Random.Range(-maxTorque,maxTorque);
   }

   private Vector3 RandomSpawnPos(){
     return new Vector3(Random.Range(-xRange,xRange),-ySpawnPos,0);
   }


    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <-2.5f) {
            if(transform.gameObject.CompareTag("Bad")){
                spanwerPool.GameOver();
            }
            spanwerPool.DesactivatePoolBox(gameObject);

        }
    }

  
}
