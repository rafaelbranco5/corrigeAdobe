using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CorrigeAdobe.Entities;
using System.IO;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Diagnostics;

namespace CorrigeAdobe
{
    public partial class home : Form
    {
        List<String> messages = new List<string>();
        enderecoIp ip = new enderecoIp();
        // private static string remoteName;
        // RegistryKey remoteRegistryKey;

        // SecurityPermission sp = new SecurityPermission(SecurityPermissionFlag.Execution);
        // RegistryPermission rp = new RegistryPermission(PermissionState.Unrestricted);

        public home()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ip.ipAdress = txtbox_ipinput.Text;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            // sp.Demand();
            // rp.Demand();

            StringBuilder sb = new StringBuilder();

            //Delete Accessibility & ReadOutLoud files
            string[] files = new string[2] { "Accessibility.api", "ReadOutLoud.api" };

            foreach (string s in files)
            {
                String path = @"\C$\Program Files (x86)\Adobe\Acrobat Reader DC\Reader\plug_ins";

                path = "\\\\172.16." + ip.ipAdress + path + "\\" + s;

                try
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            File.Delete(path);
                            messages.Add(s + "  DELETED!");
                        }
                        catch (Exception expt_msg)
                        {
                            messages.Add("Error deleting: " + s);
                            messages.Add(expt_msg.Message);
                        }
                    }
                    else
                    {
                        messages.Add(s + "  Does not exist.");
                    }
                }
                catch (DirectoryNotFoundException dirNotFound)
                {
                    messages.Add(dirNotFound.Message);
                }
            }

            //Edit regestry to block protected mode & enhanced security mode

            List<String> regestryValuesToAdd = new List<string>();
            regestryValuesToAdd.Add("bProtectedMode");
            regestryValuesToAdd.Add("bEnhancedSecurityStandalone");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "CMD.exe";

            try
            {
                foreach (String s in regestryValuesToAdd)
                {
                    //Process.Start("cmd.exe", "/C REG ADD \"\\\\172.16." + ip.ipAdress + "\\HKLM\\SOFTWARE\\WOW6432Node\\Policies\\Adobe\\Acrobat Reader\\DC\\FeatureLockDown\" /v " + s + " /t REG_DWORD /d 0");

                    //Full command should be: REG ADD \\IP\HKLM\SOFTWARE\WOW6432Node\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\" /v 'value name' /t REG_DWORD /d 0
                    String strCommandPath = "REG ADD \"\\\\" + "172.16." + ip.ipAdress + "\\HKLM\\SOFTWARE\\WOW6432Node\\Policies\\Adobe\\Acrobat Reader\\DC\\FeatureLockDown\" /v ";
                    String strCommandValueInfo = " /t REG_DWORD /d 0";

                    // /C to "carry out the command"
                    startInfo.Arguments = "/C " + strCommandPath + s + strCommandValueInfo;

                    process.StartInfo = startInfo;
                    process.Start();

                    messages.Add("Regestry key " + s + " added!");

                    process.Close();
                }

            }
            catch (SystemException emsg)
            {
                messages.Add("ERROR: " + emsg);
            }




            /* remoteRegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "VM-RB-TESTES", RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey("SOTWARE\\WOW6432Node\\Policies\\Adobe\\Acrobat Reader\\DC\\FeatureLockDown");
            // if (remoteRegistryKey.GetValue("bProtectedMode").ToString() != "0")
            // {
            //     remoteRegistryKey.SetValue("bProtectedMode", 0, RegistryValueKind.DWord);
            // }

           //  if (remoteRegistryKey.GetValue("bEnhancedSecurityStandalone").ToString() != "0")
           //  {
           //      remoteRegistryKey.SetValue("bEnhancedSecurityStandalone", 0, RegistryValueKind.DWord);
           //  } */

            foreach (string s in messages)
            {
                sb.AppendLine(s);
            }
            MessageBox.Show(sb.ToString(), "Execution Info");
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
