using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SafeDialUI : MonoBehaviour
{
    [SerializeField] private List<Transform> safeDialObjects = new();
    [SerializeField] private AudioManagerSO audioManagerSO;
    [SerializeField] private EventChannelSO safeDoorEvents;
    [SerializeField] private SafeDialDataSO dataValues;
    [SerializeField] private UIDocument safeUIDocument;

    // Root element of the ui visual tree
    private VisualElement rootOfTree;

    // Number indicator and display objects
    private UIIndicatorObj firstNumber;
    private UIIndicatorObj secondNumber;
    private UIIndicatorObj thirdNumber;
    private UIIndicatorObj fourthNumber;

    // UI buttons
    private Button leftRotate;
    private Button rightRotate;
    private Button exit;

    // Rotation Point for dial (world coordinates)
    Vector3 _rotationPt = new Vector3(-13.87f, 2.166f, 91.5f);
    // Rotation Axis for dial
    Vector3 _rotationAxis = new Vector3(0.0f, 0.0f, 1.0f);

    void OnEnable()
    {
        /*
         * This section grabs a reference to all elements on the ui document
         */
        // Grab reference to root of ui visual element tree
        rootOfTree = safeUIDocument.rootVisualElement;

        // Instantiate number display objects
        firstNumber = new UIIndicatorObj(
            rootOfTree.Q<Label>("num-1"),
            rootOfTree.Q<Label>("num-1-focus"),
            dataValues.GetCurrentNum(1),
            dataValues.IsDisplayActive(1)
        );
        secondNumber = new UIIndicatorObj(
            rootOfTree.Q<Label>("num-2"),
            rootOfTree.Q<Label>("num-2-focus"),
            dataValues.GetCurrentNum(2),
            dataValues.IsDisplayActive(2)
        );
        thirdNumber = new UIIndicatorObj(
            rootOfTree.Q<Label>("num-3"),
            rootOfTree.Q<Label>("num-3-focus"),
            dataValues.GetCurrentNum(3),
            dataValues.IsDisplayActive(3)
        );
        fourthNumber = new UIIndicatorObj(
            rootOfTree.Q<Label>("num-4"),
            rootOfTree.Q<Label>("num-4-focus"),
            dataValues.GetCurrentNum(4),
            dataValues.IsDisplayActive(4)
        );

        // Grab button elements
        leftRotate = rootOfTree.Q<Button>("left-turn");
        rightRotate = rootOfTree.Q<Button>("right-turn");
        exit = rootOfTree.Q<Button>("exit-button");

        // Subscribe button click events
        exit.clicked += Leave;
        leftRotate.clicked += RotateLeft;
        rightRotate.clicked += RotateRight;
    }

    void Update()
    {
        // Checks each number's current value for the correct value to solve the puzzle; if correct play a lock tumbler setting sound
        if (firstNumber.GetNumber() == dataValues.firstNumber & !dataValues.GetSolved(1))
        {
            dataValues.SetSolvedFirstNum(true);
            // Plays lock tumbler sound at the position of the safe ui background
            SetLockTumbler();
        }
        else if (firstNumber.GetNumber() != dataValues.firstNumber)
        {
            dataValues.SetSolvedFirstNum(false);
        }

        if (secondNumber.GetNumber() == dataValues.secondNumber & !dataValues.GetSolved(2))
        {
            dataValues.SetSolvedSecondNum(true);
            SetLockTumbler();
        }
        else if (secondNumber.GetNumber() != dataValues.secondNumber)
        {
            dataValues.SetSolvedSecondNum(false);
        }

        if (thirdNumber.GetNumber() == dataValues.thirdNumber & !dataValues.GetSolved(3))
        {
            dataValues.SetSolvedThirdNum(true);
            SetLockTumbler();
        }
        else if (thirdNumber.GetNumber() != dataValues.thirdNumber)
        {
            dataValues.SetSolvedThirdNum(false);
        }

        if (fourthNumber.GetNumber() == dataValues.fourthNumber & !dataValues.GetSolved(4))
        {
            dataValues.SetSolvedFourthNum(true);
            SetLockTumbler();
        }
        else if (fourthNumber.GetNumber() != dataValues.fourthNumber)
        {
            dataValues.SetSolvedFourthNum(false);
        }

        // Check for a win condition and signal the end of the scene
        if (dataValues.GetSolved(1) & dataValues.GetSolved(2) & dataValues.GetSolved(3) & dataValues.GetSolved(4))
        {
            safeDoorEvents.EndThisScene();
        }
    }

    void OnDestroy()
    {
        dataValues.Reset();
    }

    private void RotateLeft()
    {
        float rotation_diff;
        int difference;
        int first_number;
        int fourth_number;

        if (secondNumber.IsActive())
        {
            secondNumber.Deactivate();
            thirdNumber.Activate();
            thirdNumber.SetNumber(secondNumber.GetNumber());
            dataValues.ToggleDisplays(2, 3);
            RotateDial(360f);
        }
        else if (fourthNumber.IsActive())
        {
            first_number = firstNumber.GetNumber();
            fourth_number = fourthNumber.GetNumber();
            difference = Math.Abs(first_number - fourth_number);
            fourthNumber.Deactivate();
            firstNumber.Activate();
            dataValues.ToggleDisplays(4, 1);

            if (difference > first_number)
            {
                rotation_diff = 3.6f * ((100 - fourth_number) + first_number);
            }
            else
            {
                rotation_diff = 3.6f * difference;
            }
            RotateDial(360f + rotation_diff);
        }
        else
        {
            if (firstNumber.IsActive())
            {
                firstNumber.Increment();
            }
            else if (thirdNumber.IsActive())
            {
                thirdNumber.Increment();
            }
            RotateDial(3.6f);
        }
    }

    private void RotateRight()
    {
        if (firstNumber.IsActive())
        {
            firstNumber.Deactivate();
            secondNumber.Activate();
            secondNumber.SetNumber(firstNumber.GetNumber());
            dataValues.ToggleDisplays(1, 2);
            RotateDial(-360f);
        }
        else if (thirdNumber.IsActive())
        {
            thirdNumber.Deactivate();
            fourthNumber.Activate();
            fourthNumber.SetNumber(thirdNumber.GetNumber());
            dataValues.ToggleDisplays(3, 4);
            RotateDial(-360f);
        }
        else
        {
            if (secondNumber.IsActive())
            {
                secondNumber.Decrement();
            }
            else if (fourthNumber.IsActive())
            {
                fourthNumber.Decrement();
            }
            RotateDial(-3.6f);
        }
    }

    private void Leave()
    {
        dataValues.SetCurrentFirstNum(firstNumber.GetNumber());
        dataValues.SetCurrentSecondNum(secondNumber.GetNumber());
        dataValues.SetCurrentThirdNum(thirdNumber.GetNumber());
        dataValues.SetCurrentFourthNum(fourthNumber.GetNumber());

        // Send signal to return normal user control
        safeDoorEvents.RestorePlayerControl();
    }

    private void RotateDial(float degrees)
    {
        /*
         * Rotates the safe dial arount the Z axis 
         */
        safeDialObjects[0].RotateAround(_rotationPt, _rotationAxis, degrees);
    }

    private void SetLockTumbler()
    {
        // Plays lock tumbler sound at the position of the safe ui background
        audioManagerSO.PlayAudio("Lock Tumbler", safeDialObjects[1].position, 20f);
    }
}
