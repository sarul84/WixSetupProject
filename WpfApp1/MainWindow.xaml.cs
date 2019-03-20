using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string actionUrl = "http://localhost:54522/Document/0603102633/NSM/upload";

            HttpContent Conversion_Source = new StringContent("IAssist");
            HttpContent Document_Type_Code = new StringContent("C.DEATH");
            HttpContent Password = new StringContent("A1D97A94930E1CB0711129542C5D5FDB");
            HttpContent Username = new StringContent("Remedy_Search_User");
            HttpContent PasswordHash = new StringContent("6EDE85476D00C7634BF9A51699B22F5F");
            HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(@"C:\iAssist\Presentation1.jpg"));
            using (var client = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(Conversion_Source, "conversionSource");
                    formData.Add(Document_Type_Code, "documentTypeCode");
                    formData.Add(Password, "password");
                    formData.Add(Username, "username");
                    formData.Add(PasswordHash, "passwordHash");
                    formData.Add(bytesContent, "document", "Presentation1.jpg");
                    var response = client.PostAsync(actionUrl, formData).GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Upload failed");
                    }
                }
            }
        }
    }
}
