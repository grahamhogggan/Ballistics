using System;
using System.Collections.Generic;

public class IntegratedFunction : Function
{
    private Function f;
    public float initialValue;

    private SortedDictionary<float, float> tableOfValues;
    private List<float> sortedKeys;

    public IntegratedFunction(Function f, float initialValue = 0f)
    {
        this.f = f;
        this.initialValue = initialValue;
        tableOfValues = new SortedDictionary<float, float>();
        sortedKeys = new List<float>();

        BuildTable(-300, 300, 0.5f);
    }

    private void BuildTable(float min, float max, float step)
    {
        float sum = initialValue;
        tableOfValues[0] = initialValue;
        sortedKeys.Add(0);

        // Forward fill
        for (float x = step; x <= max; x += step)
        {
            sum += step * f.Evaluate(x - step / 2);
            tableOfValues[x] = sum;
            sortedKeys.Add(x);
        }

        // Backward fill
        sum = initialValue;
        for (float x = -step; x >= min; x -= step)
        {
            sum -= step * f.Evaluate(x + step / 2);
            tableOfValues[x] = sum;
            sortedKeys.Insert(0, x); // add to beginning to keep sorted
        }
    }

    public override float Evaluate(float x)
    {
        float start = ClosestKey(x);
        float sum = tableOfValues[start];

        float stepSize = Math.Max(Math.Abs(x - start) / 1000f, 0.01f);

        if (x > start)
        {
            for (float i = start; i < x; i += stepSize)
                sum += stepSize * f.Evaluate(i + stepSize / 2);
        }
        else
        {
            for (float i = x; i < start; i += stepSize)
                sum -= stepSize * f.Evaluate(i + stepSize / 2);
        }

        return sum;
    }

    private float ClosestKey(float x)
    {
        int index = sortedKeys.BinarySearch(x);

        if (index >= 0)
            return sortedKeys[index]; // exact match

        index = ~index; // insertion point

        if (index == 0)
            return sortedKeys[0];

        if (index >= sortedKeys.Count)
            return sortedKeys[^1];

        float lower = sortedKeys[index - 1];
        float upper = sortedKeys[index];

        return Math.Abs(x - lower) < Math.Abs(x - upper) ? lower : upper;
    }
}
