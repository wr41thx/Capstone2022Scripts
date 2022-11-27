using UnityEngine.UIElements;

public class UIIndicatorObj
{
    // UI visual elements that will be manipulated
    private readonly Label number;
    private readonly Label focusIndicator;

    // Constructor
    public UIIndicatorObj(Label number, Label focusIndicator, int initialNumber, bool active)
    {
        this.number = number;
        this.focusIndicator = focusIndicator;
        this.number.text = initialNumber.ToString();
        if (active)
        {
            focusIndicator.AddToClassList("active");
        }
    }

    public int GetNumber()
    {
        // Returns current display number
        return int.Parse(number.text);
    }

    public bool IsActive()
    {
        /*
         * Indicates if the current object is the active one in the ui
         */

        if (focusIndicator.ClassListContains("active"))
        {
            return true;
        }

        return false;
    }

    public void Increment()
    {
        /*
         * Increments the value of the label
         */

        int currentNum = int.Parse(number.text);

        if (currentNum < 99)
        {
            ++currentNum;
        }
        else
        {
            currentNum = 0;
        }

        number.text = currentNum.ToString();
    }

    public void Decrement()
    {
        /*
         * Decrements the value of the label
         */

        int currentNum = int.Parse(number.text);

        if (currentNum > 0)
        {
            --currentNum;
        }
        else
        {
            currentNum = 99;
        }

        number.text = currentNum.ToString();
    }

    public void Activate()
    {
        /*
         * Adds class to the indicator to recolor the background
         */

        focusIndicator.AddToClassList("active");
    }

    public void Deactivate()
    {
        /*
         * Removes class from the indicator to reset background color
         */

        focusIndicator.RemoveFromClassList("active");
    }

    public void SetNumber(int newNumber)
    {
        /*
         * Sets the number of the label 
         */

        number.text = newNumber.ToString();
    }
}
