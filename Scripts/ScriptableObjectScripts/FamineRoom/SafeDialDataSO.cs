using UnityEngine;

/// <summary>
/// Data container for safe UI persistence
/// </summary>

[CreateAssetMenu(menuName = "Scriptable Objects/FamineRoom/Safe Dial Data Container")]
public class SafeDialDataSO : ScriptableObject
{
    private int _currFirstNumb = 0;
    private int _currSecondNumb = 0;
    private int _currThirdNumb = 0;
    private int _currFourthNumb = 0;

    private bool _firstDisplayActive = true;
    private bool _secondDisplayActive = false;
    private bool _thirdDisplayActive = false;
    private bool _fourthDisplayActive = false;

    private bool _solvedFirstNumber = false;
    private bool _solvedSecondNumber = false;
    private bool _solvedThirdNumber = false;
    private bool _solvedFourthNumber = false;

    // Puzzle Combo
    [Tooltip("Input the value of each number for the final safe combination")]
    [SerializeField] public int firstNumber;
    [SerializeField] public int secondNumber;
    [SerializeField] public int thirdNumber;
    [SerializeField] public int fourthNumber;

    // Getter methods
    public int GetCurrentNum(int number)
    {
        switch (number)
        {
            case 1:
                return _currFirstNumb;
            case 2:
                return _currSecondNumb;
            case 3:
                return _currThirdNumb;
            case 4:
                return _currFourthNumb;
        }

        return 0;
    }

    public bool GetSolved(int number)
    {
        /*
         * Returns true or false depending of whether or not that number has been solved
         */
        switch (number)
        {
            case 1:
                return _solvedFirstNumber;
            case 2:
                return _solvedSecondNumber;
            case 3:
                return _solvedThirdNumber;
            case 4:
                return _solvedFourthNumber;
        }

        return false;
    }

    public bool IsDisplayActive(int dispNum)
    {
        switch (dispNum)
        {
            case 1:
                return _firstDisplayActive;
            case 2:
                return _secondDisplayActive;
            case 3:
                return _thirdDisplayActive;
            case 4:
                return _fourthDisplayActive;
        }

        return false;
    }

    // Setter methods
    public void SetCurrentFirstNum(int newNumber)
    {
        _currFirstNumb = newNumber;
    }

    public void SetCurrentSecondNum(int newNumber)
    {
        _currSecondNumb = newNumber;
    }

    public void SetCurrentThirdNum(int newNumber)
    {
        _currThirdNumb = newNumber;
    }

    public void SetCurrentFourthNum(int newNumber)
    {
        _currFourthNumb = newNumber;
    }

    public void SetSolvedFirstNum(bool is_solved)
    {
        _solvedFirstNumber = is_solved;
    }

    public void SetSolvedSecondNum(bool is_solved)
    {
        _solvedSecondNumber = is_solved;
    }

    public void SetSolvedThirdNum(bool is_solved)
    {
        _solvedThirdNumber = is_solved;
    }

    public void SetSolvedFourthNum(bool is_solved)
    {
        _solvedFourthNumber = is_solved;
    }

    public void ToggleDisplays(int firstDispNum, int secondDispNum)
    {
        ToggleDisplay(firstDispNum);
        ToggleDisplay(secondDispNum);
    }

    public void Reset()
    {
        _currFirstNumb = 0;
        _currSecondNumb = 0;
        _currThirdNumb = 0;
        _currFourthNumb = 0;

        _firstDisplayActive = true;
        _secondDisplayActive = false;
        _thirdDisplayActive = false;
        _fourthDisplayActive = false;

        _solvedFirstNumber = false;
        _solvedSecondNumber = false;
        _solvedThirdNumber = false;
        _solvedFourthNumber = false;
    }

    private void ToggleDisplay(int dispNum)
    {
        switch (dispNum)
        {
            case 1:
                // Sets the first display active if it's not and vice versa
                _firstDisplayActive = !_firstDisplayActive;
                break;
            case 2:
                // Sets the second display active if it's not and vice versa
                _secondDisplayActive = !_secondDisplayActive;
                break;
            case 3:
                // Sets the third display active if it's not and vice versa
                _thirdDisplayActive = !_thirdDisplayActive;
                break;
            case 4:
                // Sets the fourth display active if it's not and vice versa
                _fourthDisplayActive = !_fourthDisplayActive;
                break;
        }
    }
}
