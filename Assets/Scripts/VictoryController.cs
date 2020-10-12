﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryController : RetryController
{
    public void GoToMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
