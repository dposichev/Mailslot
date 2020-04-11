using System;
using System.Text;
using System.Windows.Forms;
using MailSlot.Core;

namespace MailSlot.WinForms
{
    public partial class Form1 : Form
    {
        private MailslotServer _mailslotServer;
        private MailslotClient _mailslotClient;

        public Form1()
        {
            InitializeComponent();

            AddLog(SginatureError.INFO, "Приложение запущено");

            _mailslotServer = new MailslotServer();
            _mailslotClient = new MailslotClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mailslotName = textBox1.Text;

            AddLog(SginatureError.INFO, $"Создание mailslot {mailslotName}");

            int result = _mailslotServer.CreateMailSlot(mailslotName);

            if (result == -1)
            {
                AddLog(SginatureError.ERROR, $"Mailslot {mailslotName} не создан. HANDLE = {result}");
            }
            else
            {
                AddLog(SginatureError.INFO, $"Mailslot {mailslotName} создан. HANDLE = {result}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string message = textBox3.Text;

                int result = _mailslotClient.SendMessage(message);

                AddLog(SginatureError.INFO, $"Отправлено сообщение: {message} - {result} байт");
            }
            catch (Exception ex)
            {
                AddLog(SginatureError.ERROR, ex.Message);
            }
        }
         
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string result = _mailslotServer.ReadMessage();

                AddLog(SginatureError.INFO, $"Прочитано сообщение: {result}");
                richTextBox1.Text += $"{DateTime.Now.ToString()} - {result}\n";
            }
            catch (Exception ex)
            {
                AddLog(SginatureError.ERROR, ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string mailslotName = textBox2.Text;
                string remoteComputer = textBox5.Text;

                AddLog(SginatureError.INFO, $"Открытие mailslot {mailslotName}");

                int result;

                if (remoteComputer.Equals(""))
                {
                    result = _mailslotClient.OpenMailslot(mailslotName);
                }
                else
                {
                    result = _mailslotClient.OpenMailslot(mailslotName, remoteComputer);
                }

                if (result == -1)
                {
                    AddLog(SginatureError.ERROR, $"Mailslot {mailslotName} не открыт. HANDLE = {result}");
                }
                else
                {
                    AddLog(SginatureError.INFO, $"Mailslot {mailslotName} открыт. HANDLE = {result}");
                }
            }
            catch (Exception ex)
            {
                AddLog(SginatureError.ERROR, ex.Message);
            }
        }

        private enum SginatureError 
        {
            INFO, ERROR, FATAL_ERROR
        }

        private void AddLog(SginatureError sginatureError, string errorMessage)
        {
            StringBuilder log = new StringBuilder();

            log.Append(DateTime.Now.ToString());
            log.Append(" - ");
            log.Append(sginatureError.ToString());
            log.Append(" : ");
            log.Append(errorMessage);
            log.Append("\n");

            richTextBox2.Text += log.ToString();
        }
    }
}
