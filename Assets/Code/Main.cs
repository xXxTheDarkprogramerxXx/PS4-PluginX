using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class Main : MonoBehaviour
{
    [DllImport("universal")]
    private static extern int FreeUnjail(int FWVersion);

    [DllImport("universal")]
    //will return the version as e.g.905 //this version can't be spoofed
    private static extern UInt16 get_firmware();

    [DllImport("universal")]
    private static extern int FreeMount();

    [DllImport("universal")]
    private static extern int FreeFTP();

    // Use this for initialization
    void Start()
    {
        if (Application.platform == RuntimePlatform.PS4)
        {
            //Escape sandbox
            FreeUnjail(get_firmware());

            FreeFTP(); //Enable FTP so we can ensure it works properly 

            //now do freemount so we have full read write access 
            FreeMount();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //controler X Button
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            return;
        }
        //Controler O Button
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
        //Controler /\
        else if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Keypad8))
        {

        }
        //Controler []
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            try
            {
                

                //create each and every file in the system so the user can replace from pc
                string[] DIrs = System.IO.Directory.GetDirectories("/system_ex/app/", "*.*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < DIrs.Length; i++)
                {
                    //usb 0
                    try
                    {
                        string USB = "usb0";
                        string MainDir = "/mnt/"+USB+"/PluginX";
                        if (!Directory.Exists(MainDir))
                        {
                            Directory.CreateDirectory(MainDir);
                        }
                        if(!Directory.Exists(MainDir + "/" + Path.GetDirectoryName(DIrs[i])))
                        {
                            Directory.CreateDirectory(MainDir + "/" + Path.GetDirectoryName(DIrs[i]));
                        }
                        return;
                    }
                    catch(Exception ex)
                    {

                    }
                    //usb 1
                    try
                    {
                        string USB = "usb1";
                        string MainDir = "/mnt/" + USB + "/PluginX";
                        if (!Directory.Exists(MainDir))
                        {
                            Directory.CreateDirectory(MainDir);
                        }
                        if (!Directory.Exists(MainDir + "/" + Path.GetDirectoryName(DIrs[i])))
                        {
                            Directory.CreateDirectory(MainDir + "/" + Path.GetDirectoryName(DIrs[i]));
                        }
                        return;
                    }
                    catch(Exception ex)
                    {

                    }
                    Assets.Wrapper.MessageBox.Show("Please connect a USB Device");
                }
            }
            catch
            { // (System.Exception ex)
                ; //LOG = "Error " + ex.Message;
            }
            return;
        }
        //Controler [   ] middle bar
        else if (Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Space))
        {

        }
        //Controler options
        else if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.RightAlt))
        {

        }
        //Controler L1
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.LeftControl))
        {

        }
        //Controler R1
        else if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.RightControl))
        {

        }
        //Controler right arrow
        else if (Input.GetKeyDown(KeyCode.Joystick1Button11) || Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
        //Controler left arrow
        else if (Input.GetKeyDown(KeyCode.Joystick1Button13) || Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }
        //Controler up arrow
        else if (Input.GetKeyDown(KeyCode.Joystick1Button10) || Input.GetKeyDown(KeyCode.UpArrow))
        {

        }
        //Controler down arrow
        else if (Input.GetKeyDown(KeyCode.Joystick1Button12) || Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
        //Controler L3
        else if (Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.LeftAlt))
        {

        }
        //Controler R3
        else if (Input.GetKeyDown(KeyCode.Joystick1Button9) || Input.GetKey(KeyCode.Mouse0))
        {
            //we want to use this mapping for changing icon colors by starkiller91
            //Make a backup on the system ? 
            //Add a replace option
            if (Application.platform == RuntimePlatform.PS4)
            {
                Assets.Wrapper.LoadingDialog.Show("Copying content from USB(s)");
            }
            {
                try
                {
                    {

                        string[] files1 = System.IO.Directory.GetDirectories(@"/mnt/usb0/PluginX/UI", "*.*", SearchOption.TopDirectoryOnly);
                        string[] files2 = System.IO.Directory.GetDirectories(@"/mnt/usb1/PluginX/UI", "*.*", SearchOption.TopDirectoryOnly);

                        //SendMessageToPS4("Files1:"+files1.Length.ToString() + "Files2:" + files2.Length.ToString());
                        string[] files = new string[files1.Length + files2.Length];
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (i >= files1.Length)
                                files[i] = files2[i - files1.Length];
                            else
                                files[i] = files1[i];
                        }

                        //now copy them over
                        for (int i = 0; i < files.Length; i++)
                        {
                            try
                            {
                                //save the dir name
                                string DirName = System.IO.Path.GetDirectoryName(files[i]);

                                //check if the files has a png 
                                //check for PNG files
                                Assets.Wrapper.LoadingDialog.Show("Getting Content for " + DirName);
                                List<string> pngFiles = System.IO.Directory.GetFiles(files[i], "*.png", SearchOption.AllDirectories).ToList();



                                for (int ix = 0; ix < pngFiles.Count; ix++)
                                {

                                    Assets.Wrapper.LoadingDialog.Show("Getting icon files for " + DirName + " (" + ix + "/" + pngFiles.Count + ")");
                                    //all items are in system_ex
                                    //copy and replace on the disk

                                    string _file = "/system_ex/app/" + DirName + "/" + System.IO.Path.GetFileName(pngFiles[i]);

                                    if (File.Exists(_file))
                                    {
                                        File.Copy(pngFiles[i], _file, true);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                    }
                }
                catch (Exception ex)
                {

                    Assets.Wrapper.MessageBox.Show("Error\n\n" + ex.Message);
                    //SendMessageToPS4("No pkg files found on usb do you have a device inserted ?");
                    //txtError.text = "Error process could not start " + ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace;
                }
            }

            if (Application.platform == RuntimePlatform.PS4)
            {
                Assets.Wrapper.LoadingDialog.Close();
            }
        }
        else
        {
            //unkown button
        }
    }
}
