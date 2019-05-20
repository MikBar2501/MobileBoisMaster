using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveGenerator : MonoBehaviour
{
    public static CaveGenerator main;

    public float moveSpeed = 1;

    float currentGate = 1;

    public GameObject cavePart;
    public List<Sprite> cavePartSprites;

    float currentX = 0;
    public float xStep = 30;

    float yTop;

    public float curretY = 0;

    float yMaxOffset;

    float yStep = 20;

    public float sizeStep = 0.1f;

    public float currentSize;

    Vector2 ToScreen(Vector3 position)
    {
        RectTransform canvas = transform.parent.gameObject.GetComponent<RectTransform>();

        Vector2 scale = new Vector2(
     canvas.rect.width / Screen.width,
     canvas.rect.height / Screen.height);

     return Vector2.Scale(
    position,
    scale
);
    }

    void Step(float scale)
    {
        GameObject top = Instantiate(cavePart, transform);
        GameObject bottom = Instantiate(cavePart, transform);

        float currentScale = scale;

        float curMaxY = yTop - currentScale / 2;

        curretY += (Random.Range(0, 100) < 50 ? yStep : -yStep);
        curretY = Mathf.Clamp(curretY, -curMaxY, curMaxY);
        //curretY = 0;

        float currentTop = curretY + currentScale / 2;
        float currentBottom = curretY - currentScale / 2;

        float trueTop = currentTop + (yTop - currentTop) / 2;
        float trueBottom = currentBottom + (-yTop - currentBottom) / 2;

        top.transform.localPosition = ToScreen(new Vector2(currentX, trueTop));
        bottom.transform.localPosition = ToScreen(new Vector2(currentX, trueBottom));

        top.GetComponent<Image>().sprite = cavePartSprites[Random.Range(0, cavePartSprites.Count)];
        bottom.GetComponent<Image>().sprite = cavePartSprites[Random.Range(0, cavePartSprites.Count)];
        //top.transform.localScale = ToScreen(new Vector2(top.transform.localScale.x, yTop - currentTop));
        //bottom.transform.localScale = ToScreen(new Vector2(bottom.transform.localScale.x, Mathf.Abs(-yTop - currentBottom)));

        currentX += xStep;
    }

    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = Vector3.zero;
        yTop = Screen.height / 2;
        yMaxOffset = yTop / 1.5f;

        StartCoroutine(MoveMap());
    }

    // Update is called once per frame
    IEnumerator MoveMap()
    {
        float gate = transform.position.x - 1;

        float moveSpeed = 10;

        float sizeStep = this.sizeStep;
        float minSize = -Screen.height / 2;
        currentSize = Screen.height / 2;

        while (true)
        {

            transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);

            //print(transform.position);

            if (ToScreen(transform.position).x < gate)
            {
                Step(currentSize);
                gate -= 0.4f;

                if (currentSize > minSize) 
                    currentSize -= sizeStep;
            }

            if (transform.position.x < -20)
                moveSpeed = this.moveSpeed;

            yield return null;
        }
    }

    public void Save(PlayerData data)
    {
        data.size = currentSize;
    }

    public void Load(PlayerData data)
    {
        if (data != null)
            currentSize = data.size;
        else
            currentSize = 0;

        if (currentSize == 0)
            currentSize = Screen.height / 2;
    }
}
