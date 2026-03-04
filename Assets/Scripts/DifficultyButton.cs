using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       button = GetComponent<Button>();
       gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       button.onClick.AddListener(SetDifficulty); 
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
