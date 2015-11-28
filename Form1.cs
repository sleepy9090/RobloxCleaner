using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RobloxCleaner {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            wipeRobloxFolder();
            wipeRobloxRegistryEntries();

            MessageBox.Show("Complete!", "Roblox Cleaner", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void wipeRobloxFolder() {

            this.textBox1.Clear();
            // Delete if exists
            // C:\Users\sleepy\AppData\Local\Roblox
            // 
            // C:\Users\<username>\AppData\Local\Microsoft\Windows\Temporary Internet Files\
            // C:\Users\<username>\AppData\Local\Microsoft\Windows\Temporary Internet Files\Low\
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string robloxPath = userPath + @"\AppData\Local\Roblox";
            //Console.WriteLine("User's path: " + userPath);
            //Console.WriteLine("Roblox path: " + robloxPath);

            this.textBox1.AppendText("Attempting to remove : " + robloxPath + "\n");

            try {
                Directory.Delete(robloxPath, true);

                this.textBox1.AppendText("The Roblox directory was successfully removed." + "\n");
            } catch (DirectoryNotFoundException ex) {
                this.textBox1.AppendText("The Roblox directory was not found." + "\n");
            } catch (Exception ex) {
                this.textBox1.AppendText("An exception was thrown while trying to remove the Roblox directory: " + ex + "\n");
            }

            
        }

        private void wipeRobloxRegistryEntries() {
            //Remove from registry:
            //HKEY_CURRENT_USER\Software\ROBLOX Corporation
            this.textBox1.AppendText(@"Attempting to remove registry key: HKEY_CURRENT_USER\Software\ROBLOX Corporation" + "\n");

            try {
                RegistryKey parentKey = Registry.CurrentUser;
                RegistryKey softwareKey = parentKey.OpenSubKey("SOFTWARE", true);
                softwareKey.DeleteSubKeyTree("ROBLOX Corporation");
                softwareKey.Close();
                parentKey.Close();
                this.textBox1.AppendText("The Roblox registry key was successfully removed." + "\n");
            } catch (ArgumentException ex) {
                this.textBox1.AppendText("The Roblox registry entries were not found." + "\n");
            } catch (Exception ex) {
                this.textBox1.AppendText("An exception was thrown while trying to remove the Roblox registry key: " + ex + "\n");
            }
        }
    }
}
