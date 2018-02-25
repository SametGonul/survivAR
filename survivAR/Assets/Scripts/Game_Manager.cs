using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

    public int state;

    public float lat;
    public float lon;

    public string url = "";
    public RawImage img;

    WWW www;

    float kalkanliFirstLat = 35.24707f;
    float kalkanliFirstLon = 33.02466f;

    private bool startChecker = true;
    Vector2 randomObjectCoordinate;
    public Button goldButton;

    private float firstLat;
    private float firstLon;

    private float previousLat;
    private float previousLon;
    private bool locationChanging = true;

    public int zoomNo = 16;
    public float zoomConstant = 4513.988880f;



    public Button zoomInButton;
    public Button zoomOutButton;

    public Button upMoveButton;
    public Button downMoveButton;
    public Button leftMoveButton;
    public Button rightMoveButton;

    public GameObject mapBorder;

    public Text minutesText;
    public Text secondsText;
    int minutes = 3;
    float seconds = 0f;

    public GameObject bigCircle;
    public GameObject smallCircle;
    bool circleSpawnChecker = false;
    bool smallCircleSpawnChecker = false;

    Vector2 bigCircleCoordinates;
    Vector2 smallCircleCoordinates;

    bool shrinkChecker = false;
    float i = 1f;
    float shrinkTime;

    Vector2 bigPos;
    Vector2 smallPos;

    float healthChecker = 0f;
    float healthBarScale = 1f;
    public Image healthBar;
    float j = 1f;

    public float outsideChecker = 600f;

    bool distanceChecker = false;
    float xDif, yDif;
    int zoomChecker;

    public Text healthText;
    private float health;

    private bool endCheck = false;
    public Text endText;
    public Button homeButton;
    public Image backgroundImage;

    public float goldDistChecker = 40f;

    public Button restartButton;
    public Button exitButton;

    public GameObject spawnPoint;
    public GameObject ball;
    public GameObject plane;

    void Start()
    {
        // First, check if user has location service enabled
        //randomObjectCoordinate = randomCoordinateGenerator(Input.location.lastData.latitude, Input.location.lastData.longitude);

        // game starts max zoom level so zoomout button disabled
        zoomOutButton.enabled = false;
        zoomOutButton.GetComponent<Image>().enabled = false;
        zoomOutButton.interactable = false;
        updateMapBorder();
        state = 0;
        health = healthBarScale * 100;
        healthText.text = Mathf.RoundToInt(health).ToString();
        
    }

    // Update is called once per frame

    void Update() {
        if (ball.transform.position.y < plane.transform.position.y - 10)
        {
            ball.transform.position = spawnPoint.transform.position;
            workExitButton();
        }
        //Debug.Log(goldDistChecker);
        if (isGameOver() == true)
        {
            state = 4;
        }
        // outside check user location
        if (isOutside() == true)
        {

            healthChecker = healthChecker + Time.deltaTime;
            if (circleSpawnChecker == true && healthChecker >= j)
            {
                //Debug.Log("bbb");
                healthBarScale = healthBarScale - 0.002f;
                health = healthBarScale * 100;
                if (health <= 0)
                {
                    health = 0f;
                }
                healthText.text = Mathf.RoundToInt(health).ToString();
                healthBar.transform.localScale = new Vector2(healthBarScale, healthBar.transform.localScale.y);
                j++;
            }
        }
        else
        {
            healthChecker = 0f;
            j = 1f;
        }

        seconds = seconds - Time.deltaTime;
        minutesText.text = minutes.ToString() + ":";
        secondsText.text = Mathf.RoundToInt(seconds).ToString();

        if (seconds <= 0)
        {
            minutes -= 1;
            seconds = 60f;
            minutesText.text = minutes.ToString() + ":";
            secondsText.text = Mathf.RoundToInt(seconds).ToString();
        }

        // big circle spawn point
        if (minutes == 2 && seconds <= 55 && circleSpawnChecker == false)
        {
            bigCircleCoordinates = randomCircleCoordinateGenerator(lat, lon);
            circleSpawnChecker = true;
            state = 3;
        }

        // small circle spawn point
        if (minutes == 2 && seconds <= 50 && smallCircleSpawnChecker == false)
        {
            smallCircleCoordinates = randomSmallCircleCoordinateGenerator(bigCircleCoordinates.x, bigCircleCoordinates.y);
            smallCircleSpawnChecker = true;
            //firstBigPos = bigCircle.transform.localPosition;
            state = 3;
        }
        if (minutes <= 2 && seconds <= 45 && shrinkChecker == false)
        {
            // before the shrink take the starting location differences
            if (distanceChecker == false)
            {
                xDif = bigCircleCoordinates.x - smallCircleCoordinates.x;
                yDif = bigCircleCoordinates.y - smallCircleCoordinates.y;
                zoomChecker = zoomNo;
                distanceChecker = true;
            }


            shrinkTime = shrinkTime + Time.deltaTime;
            if (shrinkTime >= i)
            {
                i++;
                shrink(xDif, yDif);
                if (i >= 11f)
                {
                    shrinkTime = 1f;
                    shrinkChecker = true;
                }
            }

            if (shrinkChecker == true)
            {
                bigCircleCoordinates = smallCircleCoordinates;
                smallCircle.GetComponent<Image>().enabled = false;
            }
        }



        if (state == 0)
        {
            //lat = Input.location.lastData.latitude;
            //lon = Input.location.lastData.longitude;
            lat = kalkanliFirstLat;
            lon = kalkanliFirstLon;
            state = 1;
        }
        else if (state == 1)
        {

            // yatayda lon'u 800 pixel 0.0017 etkiliyor
            // dikeyde lat'ı 1425 pixel 0.0017 etkiliyor
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoomNo + "&size=800x800&maptype=hybrid&markers=color:red%7Clabel:S%7C" + lat + "," + lon + "&key=AIzaSyBoYDlpcIawWeJ-TREeCsYheqYTBxmoYuA";

            if (locationChanging == true)
            {
                www = new WWW(url);
                locationChanging = false;
                state = 2;
            }
        }

        else if (state == 2)
        {
            while (!www.isDone)
            {
                state = 2;
            }

            if (www.isDone)
            {
                state = 3;
            }
        }
        else if (state == 3)
        {
            updateMapBorder();
            updateGoldToLocation();
            previousLat = lat;
            previousLon = lon;
            if (circleSpawnChecker == true)
            {
                updateCircleLocation();

            }
            if (smallCircleSpawnChecker == true)
            {
                updateSmallCircleLocation();
            }


            if (startChecker == true)
            {
                firstLat = lat;
                firstLon = lon;
                randomObjectCoordinate = randomCoordinateGenerator(firstLat, firstLon);
                locateGoldMap(goldButton, lat, lon);
                startChecker = false;
            }
            enableObjects();
            img.texture = www.texture;
            state = 0;
        }
        else if (state == 4)
        {
            if (endCheck == false)
            {
                backgroundImage.GetComponent<Image>().enabled = true;
                endText.enabled = true;
                homeButton.GetComponent<Image>().enabled = true;
                homeButton.enabled = true;
                endCheck = true;
            }
        }
    }

    // update big map location
    // it transforms map location to x y coordinates
    public void updateMapBorder()
    {
        Vector2 borderPosition = mapBorder.transform.localPosition;
        float x = firstLat - lat;
        float y = firstLon - lon;

        float a = y * 800 / 0.0056f;
        float b = x * 1425 / 0.0056f;

        for (int i = 0; i < (zoomNo - 16); i++)
        {
            a = a * 2.0f;
            b = b * 2.0f;
        }

        borderPosition = new Vector2(a, b);
        mapBorder.transform.localPosition = borderPosition;
    }

    // update gold location -> coordinates to x,y
    public void updateGoldToLocation()
    {
        Vector2 goldPos = goldButton.transform.localPosition;
        float x = randomObjectCoordinate.x - lat;
        float y = randomObjectCoordinate.y - lon;

        float a = y * 800 / 0.0056f;
        float b = x * 1425 / 0.0056f;

        for (int i = 0; i < (zoomNo - 16); i++)
        {
            a = a * 2.0f;
            b = b * 2.0f;
        }
        goldPos = new Vector2(a, b);
        goldButton.transform.localPosition = goldPos;
    }

    // update big circle location coordinates to x,y
    public void updateCircleLocation()
    {
        Vector2 circlePos = bigCircle.transform.localPosition;
        float x = bigCircleCoordinates.x - lat;
        float y = bigCircleCoordinates.y - lon;

        float a = y * 800 / 0.0056f;
        float b = x * 1425 / 0.0056f;
        for (int i = 0; i < (zoomNo - 16); i++)
        {
            a = a * 2.0f;
            b = b * 2.0f;
        }
        circlePos = new Vector2(a, b);

        bigCircle.transform.localPosition = circlePos;

    }

    // update small circle position from location to x,y
    public void updateSmallCircleLocation()
    {
        Vector2 circlePos = smallCircle.transform.localPosition;
        float x = smallCircleCoordinates.x - lat;
        float y = smallCircleCoordinates.y - lon;

        float a = y * 800 / 0.0056f;
        float b = x * 1425 / 0.0056f;
        for (int i = 0; i < (zoomNo - 16); i++)
        {
            a = a * 2.0f;
            b = b * 2.0f;
        }
        circlePos = new Vector2(a, b);
        smallCircle.transform.localPosition = circlePos;
    }

    public void zoomIn()
    {
        outsideChecker = outsideChecker * 2f;
        bigCircle.transform.localScale = new Vector3(2.0f * bigCircle.transform.localScale.x, 2.0f * bigCircle.transform.localScale.y, 0);
        smallCircle.transform.localScale = new Vector3(2.0f * smallCircle.transform.localScale.x, 2.0f * smallCircle.transform.localScale.y, 0);
        mapBorder.transform.localScale = new Vector3(2.0f * mapBorder.transform.localScale.x, 2.0f * mapBorder.transform.localScale.y, 0);
        if (zoomOutButton.enabled == false)
        {
            zoomOutButton.enabled = true;
            zoomOutButton.GetComponent<Image>().enabled = true;
            zoomOutButton.interactable = true;
        }

        if (zoomNo < 19)
        {
            zoomNo++;
            setGoldPositionWithZoom(0);
            if (zoomNo == 19)
            {
                zoomInButton.enabled = false;
                zoomInButton.GetComponent<Image>().enabled = false;
                zoomInButton.interactable = false;
            }
        }
        else
        {
            zoomInButton.enabled = false;
            zoomInButton.GetComponent<Image>().enabled = false;
            zoomInButton.interactable = false;
        }
        goldDistChecker = goldDistChecker * 2f;
        locationChanging = true;
        state = 1;
    }


    public void zoomOut()
    {
        outsideChecker = outsideChecker / 2f;
        bigCircle.transform.localScale = new Vector3(bigCircle.transform.localScale.x / 2.0f, bigCircle.transform.localScale.y / 2.0f, 0);
        smallCircle.transform.localScale = new Vector3(smallCircle.transform.localScale.x / 2.0f, smallCircle.transform.localScale.y / 2.0f, 0);
        mapBorder.transform.localScale = new Vector3(mapBorder.transform.localScale.x / 2.0f, mapBorder.transform.localScale.y / 2.0f, 0);
        if (zoomInButton.enabled == false)
        {
            zoomInButton.enabled = true;
            zoomInButton.GetComponent<Image>().enabled = true;
            zoomInButton.interactable = true;
        }
        if (zoomNo > 16)
        {
            zoomNo--;
            setGoldPositionWithZoom(1);
            if (zoomNo == 16)
            {
                zoomOutButton.enabled = false;
                zoomOutButton.GetComponent<Image>().enabled = false;
                zoomOutButton.interactable = false;
            }
        }
        else
        {
            zoomOutButton.enabled = false;
            zoomOutButton.GetComponent<Image>().enabled = false;
            zoomOutButton.interactable = false;
        }
        goldDistChecker = goldDistChecker / 2f;
        locationChanging = true;
        state = 1;
    }

    // first locate gold map randomly
    void locateGoldMap(Button goldButton, float lat, float lon)
    {

        Vector2 goldPos = goldButton.transform.localPosition;
        float x = randomObjectCoordinate.x - lat;
        float y = randomObjectCoordinate.y - lon;

        float a = y * 800 / 0.0056f;
        float b = x * 1425 / 0.0056f;


        goldPos = new Vector2(a, b);
        goldButton.transform.localPosition = goldPos;

    }

    // sets gold location with zoom
    void setGoldPositionWithZoom(int zoomType)
    {
        Vector2 goldPos = goldButton.transform.localPosition;
        float x = goldPos.x;
        float y = goldPos.y;
        if (zoomType == 0)
        {
            x = x * 2.0f;
            y = y * 2.0f;
        }
        else if (zoomType == 1)
        {
            x = x / 2.0f;
            y = y / 2.0f;
        }
        goldPos.x = x;
        goldPos.y = y;
        goldButton.transform.localPosition = goldPos;
    }



    Vector2 randomCoordinateGenerator(float lat, float lon)
    {
        Vector2 newCoordinate = new Vector2(Random.Range(lat - 0.0006f, lat + 0.0006f), Random.Range(lon - 0.0006f, lon + 0.0006f));
        return newCoordinate;
    }

    Vector2 randomCircleCoordinateGenerator(float lat, float lon)
    {
        Vector2 newCoordinate = new Vector2(Random.Range(lat - 0.00329f, lat + 0.00329f), Random.Range(lon - 0.00135f, lon + 0.00135f));
        return newCoordinate;
    }

    Vector2 randomSmallCircleCoordinateGenerator(float lat, float lon)
    {
        Vector2 newCoordinate = new Vector2(Random.Range(lat - 0.001f, lat + 0.001f), Random.Range(lon - 0.001f, lon + 0.001f));
        return newCoordinate;
    }


    public void moveUp()
    {
        locationChanging = true;
        kalkanliFirstLat = kalkanliFirstLat + 0.0001f;

    }

    public void moveDown()
    {
        locationChanging = true;
        kalkanliFirstLat = kalkanliFirstLat - 0.0001f;


    }

    public void moveLeft()
    {
        locationChanging = true;
        kalkanliFirstLon = kalkanliFirstLon - 0.0001f;


    }

    public void moveRight()
    {
        locationChanging = true;
        kalkanliFirstLon = kalkanliFirstLon + 0.0001f;

    }

    //shrink big circle to small circle
    public void shrink(float xDif, float yDif)
    {
        float x = bigCircleCoordinates.x;
        float y = bigCircleCoordinates.y;

        float width = bigCircle.GetComponent<RectTransform>().sizeDelta.x;
        float height = bigCircle.GetComponent<RectTransform>().sizeDelta.y;
        float circleShrinkConstant = 60f;   // decrease width and height as this number
        float outsideConstant = 30f;   // to control user location inside or outside change this according to zoom

        xDif = xDif / 10f;
        yDif = yDif / 10f;

        width = width - circleShrinkConstant;
        height = height - circleShrinkConstant;

        for (int i = 0; i < (zoomNo - 16); i++)
        {
            outsideConstant = outsideConstant * 2;
        }
        outsideChecker = outsideChecker - outsideConstant;
        bigCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        bigCircleCoordinates = new Vector2(x - xDif, y - yDif);
        updateCircleLocation();
    }


    bool isOutside()
    {
        float xSquare = bigCircle.transform.localPosition.x;
        float ySquare = bigCircle.transform.localPosition.y;
        xSquare = xSquare * xSquare;
        ySquare = ySquare * ySquare;

        float distance = Mathf.Sqrt(xSquare + ySquare);

        if (distance > outsideChecker)
        {
            return true;
        }
        else
            return false;
    }

    public void restart()
    {
        SceneManager.LoadScene("game");
    }

    public bool isGameOver()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void enableObjects()
    {
        float x = goldButton.transform.localPosition.x;
        float y = goldButton.transform.localPosition.y;


        x = x * x;
        y = y * y;

        float distance = Mathf.Sqrt(x + y);

        if (distance <= goldDistChecker)
        {
            goldButton.enabled = true;
        }
        else
        {
            goldButton.enabled = false;
        }
    }
    public void goHome()
    {
        SceneManager.LoadScene("dashboard");
    }

    public void enableARCamera()
    {
        img.GetComponent<RawImage>().enabled = false;
        goldButton.GetComponent<Image>().enabled = false;
        goldButton.enabled = false;
        goldButton.interactable = false;

        zoomInButton.GetComponent<Image>().enabled = false;
        zoomInButton.enabled = false;
        zoomInButton.interactable = false;

        zoomOutButton.GetComponent<Image>().enabled = false;
        zoomOutButton.enabled = false;
        zoomOutButton.interactable = false;

        upMoveButton.GetComponent<Image>().enabled = false;
        upMoveButton.enabled = false;
        upMoveButton.interactable = false;

        downMoveButton.GetComponent<Image>().enabled = false;
        downMoveButton.enabled = false;
        downMoveButton.interactable = false;

        leftMoveButton.GetComponent<Image>().enabled = false;
        leftMoveButton.enabled = false;
        leftMoveButton.interactable = false;

        rightMoveButton.GetComponent<Image>().enabled = false;
        rightMoveButton.enabled = false;
        rightMoveButton.interactable = false;

        mapBorder.GetComponent<RawImage>().enabled = false;
      

        bigCircle.GetComponent<Image>().enabled = false;
        smallCircle.GetComponent<Image>().enabled = false;

        restartButton.GetComponent<Image>().enabled = false;
        restartButton.enabled = false;
        restartButton.interactable = false;

        exitButton.GetComponent<Image>().enabled = true;
        exitButton.enabled = true;
        exitButton.interactable = true;

    }

    public void workExitButton()
    {
        img.GetComponent<RawImage>().enabled = true;
        goldButton.GetComponent<Image>().enabled = true;
        goldButton.enabled = true;
        goldButton.interactable = true;

        zoomInButton.GetComponent<Image>().enabled = true;
        zoomInButton.enabled = true;
        zoomInButton.interactable = true;

        zoomOutButton.GetComponent<Image>().enabled = true;
        zoomOutButton.enabled = true;
        zoomOutButton.interactable = true;

        upMoveButton.GetComponent<Image>().enabled = true;
        upMoveButton.enabled = true;
        upMoveButton.interactable = true;

        downMoveButton.GetComponent<Image>().enabled = true;
        downMoveButton.enabled = true;
        downMoveButton.interactable = true;

        leftMoveButton.GetComponent<Image>().enabled = true;
        leftMoveButton.enabled = true;
        leftMoveButton.interactable = true;

        rightMoveButton.GetComponent<Image>().enabled = true;
        rightMoveButton.enabled = true;
        rightMoveButton.interactable = true;

        mapBorder.GetComponent<RawImage>().enabled = true;


        bigCircle.GetComponent<Image>().enabled = true;
        smallCircle.GetComponent<Image>().enabled = true;

        restartButton.GetComponent<Image>().enabled = true;
        restartButton.enabled = true;
        restartButton.interactable = true;

        exitButton.GetComponent<Image>().enabled = false;
        exitButton.enabled = false;
        exitButton.interactable = false;

    }
}
    

