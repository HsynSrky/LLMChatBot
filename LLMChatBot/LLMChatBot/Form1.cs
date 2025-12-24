using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json; // NuGet'ten yüklediğin paket

namespace LLMChatBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = "Hazır";
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserInput.Text)) return;

            string userMsg = txtUserInput.Text;

            // Kullanıcı mesajını ekrana yazdır
            AppendMessage("Siz", userMsg, Color.Blue);
            txtUserInput.Clear();

            lblStatus.Text = "Bot yanıt üretiyor...";
            btnSend.Enabled = false;

            // LLM simülasyonundan yanıt bekle
            string botReply = await GetLLMResponse(userMsg);

            // Botun cevabını ekrana yazdır
            AppendMessage("AI Bot", botReply, Color.DarkGreen);

            lblStatus.Text = "Hazır";
            btnSend.Enabled = true;
        }

        // Mesajları RichTextBox'a ekleyen yardımcı metod
        private void AppendMessage(string sender, string message, Color color)
        {
            rtbChatHistory.SelectionStart = rtbChatHistory.TextLength;
            rtbChatHistory.SelectionLength = 0;
            rtbChatHistory.SelectionColor = color;
            rtbChatHistory.SelectionFont = new Font(rtbChatHistory.Font, FontStyle.Bold);
            rtbChatHistory.AppendText(sender + ": ");

            rtbChatHistory.SelectionColor = Color.Black;
            rtbChatHistory.SelectionFont = new Font(rtbChatHistory.Font, FontStyle.Regular);
            rtbChatHistory.AppendText(message + Environment.NewLine + Environment.NewLine);

            rtbChatHistory.ScrollToCaret();
        }

        // LLM Mantığı: Veriyi JSON olarak işleyen metod
        private async Task<string> GetLLMResponse(string input)
        {
            await Task.Delay(1500); // Gerçekçi bekleme süresi

            // Veriyi nesne olarak oluştur (NTP)
            var responseData = new
            {
                answer = "İsteğinizi aldım. '" + input + "' analizi yapıldı. Ben bir LLM simülasyonuyum.",
                status = "Success"
            };

            // JSON Serileştirme ve Geri Çözme
            string jsonOutput = JsonConvert.SerializeObject(responseData);
            var result = JsonConvert.DeserializeObject<dynamic>(jsonOutput);

            return (string)result.answer;
        }
    }
}