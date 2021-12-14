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
            string USB = "usb0";
            string MainDir = "/mnt/" + USB + "/PluginX";
            //replace the bgm file from usb
            string _File = MainDir + "/bgm.at9";
            string USB1 = "usb1";
            MainDir = "/mnt/" + USB1 + "/PluginX";
            string _File1 = MainDir + "/bgm.at9";


            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //temp working env
                _File = @"C:\Users\3de Echelon\Downloads\OrbisTitleMetadataDatabase-master\OrbisTitleMetadataDatabase-master\OrbisTitleMetadataDatabase\bin\Debug\EP0001-CUSA00009_00-AC4GAMEPS4000001\snd0.at9";
            }

            //Set source 
            //"C:\Users\3de Echelon\Desktop\ps4\system_ex\app\NPXS20001\systembgm\bgm_main.at9"
            string source = "/system_ex/app/NPXS20001/systembgm/bgm_main.at9";
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                source = @"C:\TEMP\ps4\bgm_main.at9";
            }
            if (File.Exists(_File))
            {
                File.Copy(source, source + ".org");//make a backup of the orginal

                string BeforeFIleName = Path.GetFileName(_File);

                File.Copy(_File, source, true);

                if (Application.platform == RuntimePlatform.PS4)
                {
                    Assets.Wrapper.MessageBox.Show("BGM Has been replaced");
                }
                return;
            }
            else if (File.Exists(_File1))
            {
                File.Copy(source, source + ".org");//make a backup of the orginal

                string BeforeFIleName = Path.GetFileName(_File1);

                File.Copy(_File1, source, true);

                if (Application.platform == RuntimePlatform.PS4)
                {
                    Assets.Wrapper.MessageBox.Show("BGM Has been replaced");
                }
                return;
            }
            else
            {
                if (Application.platform == RuntimePlatform.PS4)
                {
                    Assets.Wrapper.MessageBox.Show("Could not load bgm.at9 from usb0 or usb1\nPlease ensure the file exists");
                }
            }

        }
        //Controler []
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            try
            {

                bool error = true;
                string[] DIrs;

                //create each and every file in the system so the user can replace from pc
                if (Application.platform == RuntimePlatform.PS4)
                {
                    DIrs = System.IO.Directory.GetDirectories("/system_ex/app/", "*.*", SearchOption.TopDirectoryOnly);
                }
                else
                {
                    DIrs = System.IO.Directory.GetDirectories(@"C:\Users\3de Echelon\Desktop\ps4\system_ex\app\", "*.*", SearchOption.TopDirectoryOnly);
                }
                for (int i = 0; i < DIrs.Length; i++)
                {
                    //usb 0
                    try
                    {
                        string USB = "usb0";
                        string MainDir = "/mnt/" + USB + "/PluginX";
                        if (!Directory.Exists(MainDir))
                        {
                            Directory.CreateDirectory(MainDir);
                        }
                        if (!Directory.Exists(MainDir + "/" + Path.GetFileName(DIrs[i])))
                        {
                            Directory.CreateDirectory(MainDir + "/" + Path.GetFileName(DIrs[i]));
                        }

                        //get all files from the folders
                        List<string> pngfiels = System.IO.Directory.GetFiles(DIrs[i] + "/sce_sys", "*.png", SearchOption.AllDirectories).ToList();
                        foreach (var item in pngfiels)
                        {
                            string savepath = MainDir + "/" + Path.GetFileName(DIrs[i]) + "/" + Path.GetFileName(item);

                            File.Copy(item, savepath);
                        }

                        error = false;
                    }
                    catch (Exception ex)
                    {
                        //if (ex.InnerException is AccessViolationException)
                        //{

                        //}
                        //else
                        //{
                        //    if (Application.platform == RuntimePlatform.PS4)
                        //    {
                        //        Assets.Wrapper.MessageBox.Show("Error\n" + ex.Message + "\n\n" + ex.StackTrace);
                        //    }
                        //}
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
                        if (!Directory.Exists(MainDir + "/" + Path.GetFileName(DIrs[i])))
                        {
                            Directory.CreateDirectory(MainDir + "/" + Path.GetFileName(DIrs[i]));
                        }

                        //get all files from the folders
                        List<string> pngfiels = System.IO.Directory.GetFiles(DIrs[i] + "/sce_sys", "*.png", SearchOption.AllDirectories).ToList();
                        foreach (var item in pngfiels)
                        {
                            string savepath = MainDir + "/" + Path.GetFileName(DIrs[i]) + "/" + Path.GetFileName(item);

                            File.Copy(item, savepath);
                        }


                        error = false;
                    }
                    catch (Exception ex)
                    {
                        //if (ex.InnerException is AccessViolationException)
                        //{

                        //}
                        //else
                        //{
                        //    if (Application.platform == RuntimePlatform.PS4)
                        //    {
                        //        Assets.Wrapper.MessageBox.Show("Error\n" + ex.Message + "\n\n" + ex.StackTrace);
                        //    }
                        //}
                    }
                    if (Application.platform == RuntimePlatform.WindowsEditor)
                    {
                        string MainDir = "C:\\Temp\\PS4\\PluginX";
                        if (!Directory.Exists(MainDir))
                        {
                            Directory.CreateDirectory(MainDir);
                        }
                        if (!Directory.Exists(MainDir + "\\" + Path.GetFileName(DIrs[i])))
                        {
                            Directory.CreateDirectory(MainDir + "\\" + Path.GetFileName(DIrs[i]));
                        }
                        //get all files from the folders
                        List<string> pngfiels = System.IO.Directory.GetFiles(DIrs[i] + "\\sce_sys", "*.png", SearchOption.AllDirectories).ToList();
                        foreach (var item in pngfiels)
                        {
                            string savepath = MainDir + "\\" + Path.GetFileName(DIrs[i]) + "\\" + Path.GetFileName(item);

                            File.Copy(item, savepath);
                        }


                        error = false;
                    }
                }
                if (error == true)
                {
                    Assets.Wrapper.MessageBox.Show("Please connect a USB Device");
                }
                else
                {
                    Assets.Wrapper.MessageBox.Show("Directories created and files copied to USB replace any item and press R3 To change them on the system");
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
                //Assets.Wrapper.LoadingDialog.Show("Copying content from USB(s)");
            }

            try
            {
                {
                    List<string> FilesReplaced = new List<string>();
                    string[] files = null;

                    try
                    {
                        string[] files1 = new string[0];
                        string[] files2 = new string[0];
                        try
                        {
                            files1 = System.IO.Directory.GetDirectories("/mnt/usb0/PluginX", "*.*", SearchOption.TopDirectoryOnly);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            files2 = System.IO.Directory.GetDirectories("/mnt/usb1/PluginX", "*.*", SearchOption.TopDirectoryOnly);
                        }
                        catch (Exception ex)
                        {

                        }

                        //SendMessageToPS4("Files1:"+files1.Length.ToString() + "Files2:" + files2.Length.ToString());
                        files = new string[files1.Length + files2.Length];
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (i >= files1.Length)
                                files[i] = files2[i - files1.Length];
                            else
                                files[i] = files1[i];
                        }
                    }
                    catch (Exception ex)
                    {
                        Assets.Wrapper.MessageBox.Show("Error\n\n" + ex.Message);
                    }
                    if (Application.platform == RuntimePlatform.WindowsEditor)
                    {
                        files = System.IO.Directory.GetDirectories(@"C:\TEMP\ps4\PluginX", "*.*", SearchOption.TopDirectoryOnly);
                    }

                    if (files == null)
                    {

                        if (Application.platform == RuntimePlatform.PS4)
                        {
                            Assets.Wrapper.MessageBox.Show("No Files found");
                        }
                        return;
                    }
                    //now copy them over
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {
                            //save the dir name
                            string DirName = System.IO.Path.GetFileName(files[i]);

                            //check if the files has a png 
                            //check for PNG files
                            if (Application.platform == RuntimePlatform.PS4)
                            {
                                Assets.Wrapper.LoadingDialog.Show("Getting Content for " + DirName);
                            }
                            List<string> pngFiles = System.IO.Directory.GetFiles(files[i]+"/", "*.png", SearchOption.AllDirectories).ToList();



                            for (int ix = 0; ix < pngFiles.Count; ix++)
                            {
                                if (Application.platform == RuntimePlatform.PS4)
                                {
                                    Assets.Wrapper.LoadingDialog.Show("Getting icon files for " + DirName + " (" + ix.ToString() + "/" + pngFiles.Count.ToString() + ")");
                                }
                                //all items are in system_ex
                                //copy and replace on the disk

                                string _file = "/system_ex/app/" + DirName + "/sce_sys/" + System.IO.Path.GetFileName(pngFiles[ix]);
                                if (Application.platform == RuntimePlatform.WindowsEditor)
                                {
                                    _file = @"C:\Users\3de Echelon\Desktop\ps4\system_ex\app\" + DirName + @"\sce_sys\" + System.IO.Path.GetFileName(pngFiles[ix]);
                                }
                                if (File.Exists(_file))
                                {
                                    var fi1 = new FileInfo(pngFiles[ix]);
                                    var fi2 = new FileInfo(_file);
                                    if (!Assets.Wrapper.FileUtil.FilesContentsAreEqual(fi1, fi2))
                                    {
                                        FilesReplaced.Add(pngFiles[ix]);
                                        File.Copy(pngFiles[ix], _file, true);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Assets.Wrapper.MessageBox.Show(ex.Message + "\n\n"+ex.StackTrace);
                        }

                    }
                    if (FilesReplaced.Count != 0)
                    {

                        string Msg = "Files replaced \nReboot the system for the affect changes\n\nList Of Changed Files\n\n";
                        string combined = string.Join(Environment.NewLine, FilesReplaced.ToArray());
                        Assets.Wrapper.MessageBox.Show(Msg + combined);
                    }
                    else
                    {
                        Assets.Wrapper.MessageBox.Show("No files replaced either all files are the same or a usb is not connected");
                    }
                }
            }
            catch (Exception ex)
            {

                Assets.Wrapper.MessageBox.Show("Error\n\n" + ex.Message);
                //SendMessageToPS4("No pkg files found on usb do you have a device inserted ?");
                //txtError.text = "Error process could not start " + ex.Message + "\n" + ex.Data + "\n" + ex.StackTrace;
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
