using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceCards : MonoBehaviour
{
    [SerializeField]
    private GameObject[] backs = new GameObject[16]; 
    private Dictionary<GameObject, int> backCards = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, int> faceCards = new Dictionary<GameObject, int>();
    [SerializeField]
    private GameObject[] faces = new GameObject[16];
    [SerializeField]
   private int firstCard =-1;
    [SerializeField]
    private int secondCard=-1;
    public Text winText;
    public Text startText;
    void Start()
    {
        Time.timeScale = 0;
        
        for (int i = 0; i < 16; i++)
        {
            backs[i] = transform.GetChild(16 + i).gameObject;
            faces[i] = transform.GetChild(i).gameObject;
           faceCards.Add(faces[i], 0);
          backCards.Add(backs[i], i);
        }

        for (int i = 0; i < 16; i++)
        {

            int rezult = 1;
            do
            {
                int number = Random.Range(0, 16);
                if (faceCards[faces[number]] == 0)
                {
                    faces[number].transform.position = backs[i].transform.position;
                    faceCards[faces[number]] = i;
             
                    rezult = 0;
                }
            } while (rezult != 0);
        }
    }

    void Update()
    {
        int z = -1;
        for (int i = 0; i < 16; i++)
        {
            if (faces[i] != null) z = i;
        }
        if (z == -1)
        {
            GameWin();

        }
    }

    private IEnumerator ExampleCoroutine()
    {
     
        Debug.Log(firstCard + " firstCard");
            yield return new WaitForSeconds(1.1f);
            for (int j = 0; j < 16; j++)
            {
                if (faceCards[faces[j]] == firstCard)
                {
                    Debug.Log(faceCards[faces[j]] + " " + j);
                    if (j <= 7)
                    {
                        if (faceCards[faces[j + 8]] == secondCard)
                        {
                            Debug.Log(secondCard + " secondCard " + j);
                            Destroy(faces[j]);
                            Destroy(faces[j + 8]);
                            Destroy(backs[firstCard]);
                            Destroy(backs[secondCard]);
                        }
                        else
                        {
                            backs[firstCard].SetActive(true);
                            backs[secondCard].SetActive(true);

                        }
                    }
                    else
                    {
                        if (faceCards[faces[j - 8]] == secondCard)
                        {
                            Debug.Log(secondCard + " " + j);
                            Destroy(faces[j]);
                            Destroy(faces[j - 8]);
                            Destroy(backs[firstCard]);
                            Destroy(backs[secondCard]);
                        }
                        else
                        {
                            backs[firstCard].SetActive(true);
                            backs[secondCard].SetActive(true);

                        }
                    }

                }
            }
            secondCard = -1;
            firstCard = -1;
        
    }

    public void OnButtonFirstOrSecond(GameObject image)
    {
        if (firstCard == -1)
        {
            firstCard = backCards[image];
            image.SetActive(false);
        }
        else if (secondCard == -1 )
        {
            secondCard = backCards[image];
            image.SetActive(false);
            StartCoroutine(ExampleCoroutine());
        }
    }

    public void StartGame()
    {
        startText.gameObject.SetActive(false);
        Time.timeScale = 0.5f;
    }
   
    public void GameWin()
    {
        winText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


}
