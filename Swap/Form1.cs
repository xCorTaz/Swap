using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Swap
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public string uuID;
        public bool running;
        public static bool TellMe = false;
        public bool GetSettingsBool;
        public bool SwitchLoginBool;
        public CookieContainer Sessionz;
        private bool i;
        private int x;
        private int y;
        public string accSettings;
        public bool IfLoggedIn;
        public static string AccSettings2;
        public bool TurboGO;
        public int attempts;
        public static CookieContainer CookiesX = new CookieContainer();
        private IContainer component;
        internal delegate void anonRedir();
        public static string getaccountsettings(CookieContainer ssn)
        {
            string text = string.Empty;
            string text2 = string.Empty;
            string text3 = string.Empty;
            string text4 = string.Empty;
            string text5 = string.Empty;
            string text6 = string.Empty;
            string text7 = string.Empty;
            string text8 = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://i.instagram.com/api/v1/accounts/current_user/?edit=true");
            string str = Guid.NewGuid().ToString().ToUpper();
            httpWebRequest.Host = "i.instagram.com";
            httpWebRequest.UserAgent = "Instagram 9.4.0 Android (18/4.3; 320dpi; 720x1280; Xiaomi; HM 1SW; armani; qcom; en_US)";
            httpWebRequest.Headers.Add("Accept-Language", "ar;q=1, en;q=0.9");
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Proxy = null;
            httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
            httpWebRequest.CookieContainer = ssn;
            using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream()))
            {
                string input = streamReader.ReadToEnd();
                text = Regex.Match(input, "pk\": (.*?),").Value;
                bool flag = Operators.CompareString(text, null, false) == 0;
                if (flag)
                {
                    text = Regex.Match(input, "pk\": (.*?)}").Value;
                    bool flag2 = Operators.CompareString(text, null, false) == 0;
                    if (flag2)
                        return "|||||||";
                }
                text2 = Regex.Match(input, "full_name\": \"(.*?)\"").Value;
                text3 = Regex.Match(input, "is_private\": (.*?),").Value;
                bool flag3 = Operators.CompareString(text3, null, false) == 0;
                if (flag3)
                {
                    text3 = Regex.Match(input, "is_private\": (.*?)}").Value;
                    bool flag4 = Operators.CompareString(text3, null, false) == 0;
                    if (flag4)
                        return "|||||||";
                }
                text4 = Regex.Match(input, "phone_number\": \"(.*?)\"").Value;
                text5 = Regex.Match(input, "biography\": \"(.*?)\"").Value;
                text6 = Regex.Match(input, "gender\": (.*?),").Value;
                bool flag5 = Operators.CompareString(text6, null, false) == 0;
                if (flag5)
                {
                    text6 = Regex.Match(input, "gender\": (.*?)}").Value;
                    bool flag6 = Operators.CompareString(text6, null, false) == 0;
                    if (flag6)
                        return "|||||||";
                }
                text7 = Regex.Match(input, "email\": \"(.*?)\"").Value;
                text8 = Regex.Match(input, "external_url\": \"(.*?)\"").Value;
                Form1.AccSettings2 = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", new object[] { text, text2, text3, text4, text5, text6, text7, text8 });
            }
            return Form1.AccSettings2;
        }
        public static CookieContainer igLogin(string Username, string Password, string uuID)
        {
            try
            {
                string str = Guid.NewGuid().ToString().ToUpper();
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = stringBuilder;
                string text = string.Concat(new string[] { "{\"_uuid\":\"", uuID, "\",\"password\":\"", Password, "\",\"username\":\"", Username, "\",\"device_id\":\"", uuID, "\",\"from_reg\":false,\"_csrftoken\":\"missing\",\"login_attempt_count\":0}" });
                text = string.Format("{0}.{1}", Form1.Sha256_hash(text, "b03e0daaf2ab17cda2a569cace938d639d1288a1197f9ecf97efd0a4ec0874d7"), text);
                stringBuilder2.AppendLine("--" + str);
                stringBuilder2.AppendLine("Content-Disposition: form-data; name=\"signed_body\"");
                stringBuilder2.AppendLine();
                stringBuilder2.AppendLine(text);
                stringBuilder2.AppendLine("--" + str);
                stringBuilder2.AppendLine("Content-Disposition: form-data; name=\"ig_sig_key_version\"");
                stringBuilder2.AppendLine();
                stringBuilder2.AppendLine("4");
                stringBuilder2.Append("--" + str + "--");
                byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://i.instagram.com/api/v1/accounts/login/");
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
                httpWebRequest.Method = "POST";
                httpWebRequest.Host = "i.instagram.com";
                httpWebRequest.UserAgent = "Instagram 9.4.0 Android (18/4.3; 320dpi; 720x1280; Xiaomi; HM 1SW; armani; qcom; en_US)";
                httpWebRequest.Headers.Add("Accept-Language", "ar;q=1, en;q=0.9");
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Proxy = null;
                httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
                httpWebRequest.ContentLength = System.Convert.ToInt64(bytes.Length);
                httpWebRequest.CookieContainer = Form1.CookiesX;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                bool flag = streamReader.ReadToEnd().Contains("{\"logged_in_user");
                if (flag)
                    return Form1.CookiesX;
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
            }
            return null/* TODO Change to default(_) if this is not a reference type */;
        }
        public void redirLogin()
        {

        }
        private void StatusButton(Button btn, string input)
        {
            btn.Invoke(new Form1.anonRedir(() =>
            {
                btn.Text = string.Format(input, new object[0] { });
            }));
        }
        public Form1()
        {
            TellMe = false;
            CookiesX = new CookieContainer();
            running = false;
            TurboGO = false;
            attempts = 0;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uuID = Guid.NewGuid().ToString().ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = string.IsNullOrWhiteSpace(this.Userbox.Text);
            if (flag)
                MessageBox.Show("Username is empty!");
            else
            {
                bool flag2 = string.IsNullOrWhiteSpace(this.Passwordbox.Text);
                if (flag2)
                    MessageBox.Show("Password is empty!");
                else
                {
                    bool flag3 = this.running;
                    if (flag3)
                        this.StatusButton(button1, "start");
                    else
                    {
                        Thread thread = new Thread(this.redirLogin);
                        thread.Start();
                    }
                }
            }
        }
        public static string Sha256_hash(string value, string salt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            HMACSHA256 hmacsha = new HMACSHA256(Encoding.UTF8.GetBytes(salt));
            hmacsha.ComputeHash(Encoding.UTF8.GetBytes(value));
            byte[] hash = hmacsha.Hash;
            int num = hash.Length - 1;
            int i = 0;
            int num2;
            /*while(i <= num2)
            {
                stringBuilder.Append(hash[i].ToString("x2"));
                i += 1;
                num2 = num;
            }*/
            return stringBuilder.ToString();
        }
        public object swap(string Username)
        {
            object result = false;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = stringBuilder;
                string[] array = this.accSettings.Split(new char[] { '|' });
                string text = string.Concat(new string[] { "{\"_uid\":\"", array[0], "\",\"_csrftoken\":\"missing\",\"first_name\":\"Moved By @ak.p5(Cortaz)", "", "\",\"is_private\":\"", array[2], "\",\"phone_number\":\"", array[3], "\",\"biography\":\"", array[4], "\",\"username\":\"", Username, "\",\"gender\":\"", array[5], "\",\"email\":\"", array[6], "\",\"_uuid\":\"", this.uuID, "\",\"external_url\":\"", array[7], "\"}" });
                text = string.Format("{0}.{1}", Form1.Sha256_hash(text, "673581b0ddb792bf47da5f9ca816b613d7996f342723aa06993a3f0552311c7d"), text);
                stringBuilder2.AppendLine("signed_body=" + WebUtility.UrlEncode(text) + "&ig_sig_key_version=5");
                byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://i.instagram.com/api/v1/accounts/edit_profile/");
                httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
                httpWebRequest.Method = "POST";
                httpWebRequest.Host = "i.instagram.com";
                httpWebRequest.UserAgent = "Instagram 9.4.0 Android (18/4.3; 320dpi; 720x1280; Xiaomi; HM 1SW; armani; qcom; en_US";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                httpWebRequest.Headers.Add("Accept-Language", "ar;q=1, en;q=0.9");
                httpWebRequest.KeepAlive = true;
                httpWebRequest.ContentLength = System.Convert.ToInt64(bytes.Length);
                httpWebRequest.CookieContainer = this.Sessionz;
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                bool flag = streamReader.ReadToEnd().Contains("\"status\": \"ok\"");
                if (flag)
                    result = true;
                streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public void swapYuh()
        {
            // The following expression was wrapped in a checked-statement
            while (this.running)
            {
                bool flag = Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(this.swap(this.Targetbox.Text)));
                if (flag)
                {
                    MessageBox.Show("- Moved : @" + this.Targetbox.Text, "-@ak.p5, @1337abood", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.running = false;
                }
                else
                {
                    int num = this.attempts;
                    this.attempts = num + 1;
                    this.attemptsbox.Text = Conversions.ToString(this.attempts);
                }
                Thread.Sleep(System.Convert.ToInt32(Math.Round(Conversions.ToDouble(this.threadbox.Text))));
            }
        }
        public void SwapYuh2()
        {
            while (this.running)
            {
                bool flag = Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(this.swap(this.threadbox.Text)));
                if (flag)
                {
                    MessageBox.Show("- Moved : @" + this.Targetbox.Text, -"@ak.p5, @1337abood", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.running = false;
                }
                else
                {
                    int num = this.attempts;
                    this.attempts = num + 1;
                    this.attemptsbox.Text = Conversions.ToString(this.attempts);
                }
                Thread.Sleep(System.Convert.ToInt32(Math.Round(Conversions.ToDouble(this.threadbox.Text))));
            }
        }
        public void swapYuh3()
        {
            while (this.running)
            {
                bool flag = Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(this.swap(this.Targetbox.Text)));
                if (flag)
                {
                    MessageBox.Show("- Moved : @" + this.Targetbox.Text, "-@ak.p5, @1337abood", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.running = false;
                }
                else
                {
                    int num = this.attempts;
                    this.attempts = num + 1;
                    this.attemptsbox.Text = Conversions.ToString(this.attempts);
                }
                Thread.Sleep(System.Convert.ToInt32(Math.Round(Conversions.ToDouble(this.threadbox.Text))));
            }
        }

        private void SwitchButton(Button Btn, bool input)
        {
            Btn.Invoke(new Form1.anonRedir(() =>
            {
                Btn.Enabled = input;
            }));
        }

        private void SwitchCheckBox(CheckBox Chk, bool input)
        {
            Chk.Invoke(new Form1.anonRedir(() =>
            {
                Chk.Enabled = input;
            }));
        }

        private void SwitchTextz(TextBox txt, bool input)
        {
            txt.Invoke(new Form1.anonRedir(() =>
            {
                txt.Enabled = input;
            }));
        }
    }
}
