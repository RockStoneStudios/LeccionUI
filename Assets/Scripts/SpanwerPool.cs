using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpanwerPool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField] private List<GameObject> prefabsRandom = new List<GameObject>();
    [SerializeField] private GameObject  boxPrefab;
    [SerializeField] private Transform  positionCreated;
    [SerializeField] private PlayerInput playerInput;
    public GameObject TitleScreen;
    private InputAction createAction;
    private int poolSize;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    public Button restartGame;
    public bool isGameActive;
    private int score;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void AddToPool(int size) {
        for(int i= 0; i< size; i++){
            GameObject box = Instantiate(prefabsRandom[Random.Range(0,4)],positionCreated.position,positionCreated.rotation);
            box.transform.SetParent(positionCreated.transform);
            box.SetActive(false);
            pool.Enqueue(box);
        }
    }

    void RespawnCreated() {
        if(pool.Count > 0){
            GameObject box = pool.Dequeue();
            box.transform.SetPositionAndRotation(positionCreated.position, positionCreated.rotation);
            box.SetActive(true);
        }
         else {
            AddToPool(1);
            RespawnCreated();
         }
    }

   public void DesactivatePoolBox(GameObject box){
        box.SetActive(false);
        pool.Enqueue(box);
    }

   
   private IEnumerator CreatedBox(int dificulty) {
      while(isGameActive){
        RespawnCreated();
        if(dificulty == 1) {
        yield return new WaitForSeconds(Random.Range(1.8f,3.2f));

        }
        if(dificulty == 2) {
        yield return new WaitForSeconds(Random.Range(1.4f,2.5f));

        }
         if(dificulty == 3) {
        yield return new WaitForSeconds(Random.Range(0.6f,1.6f));

        }
        
        
      }
   }


  public void UpdateScore(int scoreToAdd){
      score += scoreToAdd;
      scoreText.text = "Score : "+score;
   }

   public void GameOver(){
      gameOverText.gameObject.SetActive(true);
      isGameActive = false;
      restartGame.gameObject.SetActive(true);
   }

   public void RestatGame(){
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       restartGame.gameObject.SetActive(false);
   }

    public void StartGame(int dificulty){
        score = 0;
        UpdateScore(0);
        playerInput = GetComponent<PlayerInput>();
        createAction = playerInput.actions["Jump"];
        isGameActive  = true;
        AddToPool(5);
        StartCoroutine(CreatedBox(dificulty));
        gameOverText.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);
        TitleScreen.SetActive(false);
    }
}
