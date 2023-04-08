using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class RouletteWheelSelection<T>
{
    private List<RouletteElement<T>> elements;
    private float totalWeight;

    public RouletteWheelSelection()
    {
        elements = new List<RouletteElement<T>>();
        totalWeight = 0;
    }

    public void Add(T item, float difficulty)
    {
        float newWeight = 1f / Mathf.Exp(Mathf.Log(difficulty) * 0.75f);
        elements.Add(new RouletteElement<T>(item, newWeight));
        totalWeight += newWeight;
    }

    public T Spin()
    {
        float randomWeight = Random.Range(0f, totalWeight);
        float weightSum = 0;
        foreach (RouletteElement<T> element in elements)
        {
            weightSum += element.weight;
            if (randomWeight < weightSum)
            {
                totalWeight -= element.weight;
                element.weight = element.weight / 2f;
                totalWeight += element.weight;
                return element.item;
            }
            Debug.Log(element.item + " : " + element.weight);
        }
        return default(T);
    }

    private class RouletteElement<U>
    {
        public U item;
        public float weight;

        public RouletteElement(U item, float weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }
}
