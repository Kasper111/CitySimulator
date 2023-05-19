
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    public float scale = 1f;

    public float offsetX = 100;
    public float offsetY = 100;

    Renderer renderer;

    void Start()
    {
        offsetX = Random.Range(0f, 999f);
        offsetY = Random.Range(0f, 999f);


        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    void Update()
    {

        
        

    }


    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }

        }

        texture.Apply();
        return texture;
    }



    Color CalculateColor(int x, int y)
    {
        
        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float) y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        //Debug.Log(sample);
        return new Color(sample, sample, sample);
    }


    //----------------



    public float CalculateVectorAngle(int x, int y)
    {

        float xCoord = (float) x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;



        float sample = Mathf.PerlinNoise(xCoord, yCoord);


        //sample = sample % 360;
        

        return sample;
    }

}
