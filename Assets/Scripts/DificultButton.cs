using UnityEngine;
using UnityEngine.UI;

public class DificultButton : MonoBehaviour
{
    private Button button;
    private SpanwerPool spanwerPool;
    public int dificulty;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        spanwerPool = FindAnyObjectByType<SpanwerPool>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty(){
        spanwerPool.StartGame(dificulty);
    }

   
}
