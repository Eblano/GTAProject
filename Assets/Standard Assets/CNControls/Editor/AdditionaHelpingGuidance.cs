﻿using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

namespace CnControls
{
    [InitializeOnLoad]
    public class AdditionaHelpingGuidance
    {
        static AdditionaHelpingGuidance()
        {
            EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
        }

        private static void PlaymodeStateChanged()
        {
            // If we've just changed our mode to a playmode
            if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                var types = from t in Assembly.GetAssembly(typeof(SimpleButton)).GetTypes()
                            where 
                                t.IsClass 
                                && t.Namespace == "CnControls"
                                && t.IsSubclassOf(typeof(MonoBehaviour))
                            select t;

                // If there are any CnControls on the scene
                bool shouldCheckForErrors = types.Any(type => UnityEngine.Object.FindObjectOfType(type));

                if (shouldCheckForErrors)
                {
                    CheckForEventSystemPresence();
                }
            }
        }

        private static void CheckForEventSystemPresence()
        {
            var eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();

            if (eventSystem == null)
            {
                ErrorPopupWindow.ShowWindow();
            }
        }
    }
}
