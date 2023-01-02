using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("---Car Settings")]
    public GameObject[] Cars; 
    public int carCount = 4;
    int carIndex = 0;
    int remainingCar;

    [Header("---Canvas Settings")]
    public Sprite carParking;
    public TextMeshProUGUI textCarCount;
    public GameObject[] CanvasCarImages;
    public TextMeshProUGUI[] texts;
    public GameObject[] Panels;
    public GameObject[] ContinueButtons;

    [Header("---Platform Settings")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] rotateSpeed;

    [Header("---Level Settings")]
    public int diamondCount;
    public ParticleSystem crash;
    public AudioSource[] SFX;

    public bool endGame;

    bool startGame;

    void Start()
    {
        startGame = true;
        endGame = false;
        Check();

        remainingCar = carCount;
        textCarCount.text = remainingCar.ToString();

        for (int i = 0; i < carCount; i++)
        {
            CanvasCarImages[i].SetActive(true);
        }

        Cars[carIndex].SetActive(true);
    }

    void Update()
    {
        if (Input.touchCount ==1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (startGame)
                {
                    Panels[0].SetActive(false);
                    Panels[3].SetActive(true);
                    startGame = false;

                }
                else
                {
                    Cars[carIndex].GetComponent<Car>().forward = true;
                    carIndex++;
                }
            }
        }

        if (!endGame)
        {
            Platform_1.transform.Rotate(new Vector3(0, 0, rotateSpeed[0] * Time.deltaTime * 50), Space.Self);
            Platform_2.transform.Rotate(new Vector3(0, 0, rotateSpeed[1] * Time.deltaTime * 50), Space.Self);
        }
    }

    public void GetNewCar()
    {
       
        remainingCar--;
        if (carIndex<carCount)
        {
            Cars[carIndex].SetActive(true);
        }
        else
        {
            Win();
        }

        CanvasCarImages[carIndex - 1].GetComponent<Image>().sprite = carParking;
        textCarCount.text = remainingCar.ToString();
    }

    void Check()
    {
        
        texts[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[1].text = SceneManager.GetActiveScene().buildIndex.ToString();
    }

    public void Lose()
    {
        //
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + diamondCount);
        //

        texts[6].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[7].text = SceneManager.GetActiveScene().buildIndex.ToString();
        texts[8].text = (carCount - remainingCar).ToString();
        texts[9].text = diamondCount.ToString();

        Panels[1].SetActive(true);
        Panels[3].SetActive(false);
        Invoke("LoseButtonsOn", 2f);
        SFX[1].Play();
    }

    void Win()
    {
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + diamondCount);
        texts[2].text = PlayerPrefs.GetInt("Diamond").ToString();
        texts[3].text = SceneManager.GetActiveScene().buildIndex.ToString();
        texts[4].text = (carCount - remainingCar).ToString();
        texts[5].text = diamondCount.ToString();

        Panels[2].SetActive(true);
        Panels[3].SetActive(false);
        Invoke("WinButtonsOn", 2f);
        SFX[2].Play();
    }

    void LoseButtonsOn()
    {
        ContinueButtons[1].SetActive(true);
    }

    void WinButtonsOn()
    {
        ContinueButtons[0].SetActive(true);
    }

    public void NextGame()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Replay()
    {
        //startGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //public void WatchAndGain()
    //{
        
    //}
    //public void WatchRevive()
    //{

    //}
}
