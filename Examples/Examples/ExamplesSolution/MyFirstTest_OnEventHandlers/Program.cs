using System;
using System.Diagnostics;
using static System.Runtime.CompilerServices.RuntimeHelpers;


public class TestingEvent { 

    public event EventHandler OnSpacePressed = null;


    private void Start() { 
        OnSpacePressed += Testing_OnSpacePressed;
    }

    private void Testing_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Print("Space!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
        }
    }

}