using System;

[Serializable]
public class Rules
{
    public static Rules Easy { get; private set; } = new Rules()
    {
        ClientWaitTimeMultiplayer = 1.5f,
        SlicingTableSliceSpeedMultiplayer = 1.1f,
        CookingStoveCookSpeedMultiplayer = 1.1f,
    };
    public static Rules Normal { get; private set; } = new Rules()
    {

    };
    public static Rules Hard { get; private set; } = new Rules()
    {
        ClientWaitTimeMultiplayer = 0.8f,
    };
    /// <summary>
    /// How fast client will be dissatisfied by time
    /// </summary>
    public float ClientWaitTimeMultiplayer = 1.0f;
    /// <summary>
    /// How fast Slicing Table will slice food on it
    /// </summary>
    public float SlicingTableSliceSpeedMultiplayer = 1.0f;
    /// <summary>
    /// How fast Cooking Stove cook items in it
    /// </summary>
    public float CookingStoveCookSpeedMultiplayer = 1.0f;
    /// <summary>
    /// How fast Items which is cooking will Burnt
    /// </summary>
    public float FoodItemBurningSpeed = 0.1f;  

    public Rules()
    {

    }

    public Rules Copy()
    {
        Rules newRules = new Rules();
        newRules.ClientWaitTimeMultiplayer = ClientWaitTimeMultiplayer;
        newRules.SlicingTableSliceSpeedMultiplayer = SlicingTableSliceSpeedMultiplayer;
        newRules.CookingStoveCookSpeedMultiplayer = CookingStoveCookSpeedMultiplayer;

        return newRules;
    }
}
