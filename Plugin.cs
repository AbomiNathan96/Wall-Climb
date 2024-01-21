using BepInEx;
using System;
using UnityEngine;
using Utilla;
using GorillaLocomotion;
using GorillaLocomotion.Climbing;
using GorillaNetworking;
using UnityEngine.UIElements;
using UnityEngine.Device;
using Oculus.Platform;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using ComputerPlusPlus;
using ComputerPlusPlus.Tools;
using Unity.Mathematics;

namespace Wall_Climb
{

    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInDependency("com.kylethescientist.gorillatag.computerplusplus")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        public static float DetR = 0.2f;
        internal GameObject CustomPlatR;
        bool platSetR = false;
        bool platSetL = false;
        internal GameObject CustomPlatL;

        void Start()
        {

            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;

        }

        void OnEnable()
        {
            CustomPlatL.SetActive(true);
            CustomPlatR.SetActive(true);
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            CustomPlatR.SetActive(false);
            CustomPlatL.SetActive(false);
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            CustomPlatL = GameObject.CreatePrimitive(PrimitiveType.Cube);
            CustomPlatR = GameObject.CreatePrimitive(PrimitiveType.Cube);
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance !s= null) */
        }

        void Update()
        {
            if(inRoom == true)
            {

                // Check for overlapping colliders in a sphere
                Collider[] Lcolliders = Physics.OverlapSphere(GorillaLocomotion.Player.Instance.leftControllerTransform.position, DetR);
                foreach (Collider col in Lcolliders)
                {
                    // Check the tag or layer of the overlapping object
                    if (col.gameObject.layer == 0 || col.gameObject.layer == 9)
                    {

                        if (ControllerInputPoller.instance.leftControllerGripFloat >= 0.5f)
                        {

                            if (platSetL == false)
                            {

                                CustomPlatL.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position;
                                CustomPlatL.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                                platSetL = true;
                                CustomPlatL.transform.Translate(new Vector3(0f, 0f, 0f));
                                CustomPlatL.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                                CustomPlatL.GetComponent<Renderer>().enabled = false;


                            }
                        }
                        else
                        {

                            platSetL = false;
                            CustomPlatL.transform.position = new Vector3(0f, 0f, 0f);
                            CustomPlatL.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                            CustomPlatL.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                            CustomPlatL.GetComponent<Renderer>().enabled = false;

                        }
                    }
                }
                // Check for overlapping colliders in a sphere
                Collider[] Rcolliders = Physics.OverlapSphere(GorillaLocomotion.Player.Instance.rightControllerTransform.position, DetR);

                foreach (Collider col in Rcolliders)
                {
                    // Check the tag or layer of the overlapping object
                    if (col.gameObject.layer == 0 || col.gameObject.layer == 9)
                    {
                        if (ControllerInputPoller.instance.rightControllerGripFloat >= 0.5f)
                        {
                            if (platSetR == false)
                            {
                                CustomPlatR.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                                CustomPlatR.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                                platSetR = true;
                                CustomPlatR.transform.Translate(new Vector3(0f, 0f, 0f));
                                CustomPlatR.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                                CustomPlatR.GetComponent<Renderer>().enabled = true;


                            }
                        }
                        else
                        {
                            platSetR = false;
                            CustomPlatR.transform.position = new Vector3(0f, 0f, 0f);
                            CustomPlatR.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                            CustomPlatR.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                            CustomPlatR.GetComponent<Renderer>().enabled = true;

                        }
                    }
                }
            }

        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }

        //⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣏⡦⠤⣤⠽⠤⡄
        //⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡤⠤⠣⢈⠇⠀⠁⣠⡿⡄
        // ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠂⠉⠀⠀⠀⠀⠀⢀⡀⠈⠀⠀⠈
        //⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡔⠀⠀⠀⠀⠀⡀⠀⡰⣯⡀⠀⠀⠀⠀
        //⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⠁⠀⠀⠀⠀⠀⡹⠂⢽⠎⠁⠀⠀⠀⠀
        //⠀⠀⠀⠀⠀⠀⠀⣀⠠⠄⠃⣴⠀⠀⢀⡠⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀
        //⠈⠉⠉⠉⠉⠉⠉⠀⠀⠀⠀⠈⠧⣢⠌⣁⠐⠋⠂⠀⠀⠀⠀⠀⠀⠀
        //⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
        // OMG !!1! ITS A RAT FR FR GUYS

    }

}
