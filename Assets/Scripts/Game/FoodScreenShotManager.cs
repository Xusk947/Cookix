using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScreenShotManager : MonoBehaviour
{
    private Camera _screenshotCamera;
    private GameObject _itemHolder;

    private void Start()
    {
        GameObject screenshotGameObject = new GameObject();
        screenshotGameObject.name = "ScreenShotGameObject";
        screenshotGameObject.transform.position = new Vector3(0, 100, 0);
        // Create camera and set Local Transofrm to Vector3.zero
        Camera camera = new GameObject().AddComponent<Camera>();
        camera.name = "ScreenShotCamera";
        camera.targetDisplay = 1;
        camera.transform.SetParent(screenshotGameObject.transform, false);
        camera.transform.localPosition = Vector3.zero;
        
        // Create a plane like a background for Items
        GameObject plane = Instantiate(Content.Instance.ScreenshotBackground);
        plane.transform.SetParent(screenshotGameObject.transform, false);
        plane.transform.localPosition = new Vector3(0, 0, 3);
        plane.transform.localScale = new Vector3(10, 10, 0.2f);
        // Create Item Holder for fast Placing a new FoodEntities
        _itemHolder = new GameObject();
        _itemHolder.name = "ItemHolder";
        _itemHolder.transform.SetParent(screenshotGameObject.transform, false);
        _itemHolder.transform.localPosition = new Vector3(0, -0.2f, 1f);
    }

    public Sprite TakeFoodEntityScreenShot(FoodEntity foodEntity)
    {
        // Create a Instance of FoodEntity and later take a screenshot of it
        FoodEntity cameraFoodEntity = Instantiate(foodEntity);
        Sprite sprite = TakeScreenshot();
        // Destroy shoted Game Object after taking a screenshot
        Destroy(cameraFoodEntity.gameObject);

        return sprite;
    }

    private Sprite TakeScreenshot()
    {
        // Create a RenderTexture to capture the screenshot
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        _screenshotCamera.targetTexture = renderTexture;

        // Render the camera to the RenderTexture
        _screenshotCamera.Render();

        // Create a new Texture2D and read the RenderTexture data into it
        Texture2D screenshotTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshotTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshotTexture.Apply();

        // Create a new Sprite from the Texture2D
        Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), Vector2.zero);

        // Clean up
        RenderTexture.active = null;
        _screenshotCamera.targetTexture = null;
        Destroy(renderTexture);

        // Return the Sprite
        return screenshotSprite;
    }
}