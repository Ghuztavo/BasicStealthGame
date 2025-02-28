using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private GameObject startText;
    private float startTextTime = 7.0f;
    private float startTextTimer = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (startText != null)
        {
            startText.SetActive(true);
        }
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (startText)
        {       
            if (startTextTimer > startTextTime)
            {
                startText.SetActive(false);
                Debug.Log("text disapeared");
            }
            else
            {
                startTextTimer += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    public void WinGame()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
