﻿using Olive;
using System;

public class Application
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        try
        {
            Zebble.UIRuntime.Initialize<Application>("Project2");
            UIKit.UIApplication.Main(args, null, typeof(IOS.AppDelegate));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToLogString());
        }
    }
}