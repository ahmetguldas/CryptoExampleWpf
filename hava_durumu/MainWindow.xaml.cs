using HtmlAgilityPack;
using System;
using System.Net;
using System.Windows;
using System.Windows.Threading;

namespace hava_durumu
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            tmrGuncelle = new DispatcherTimer(DispatcherPriority.Background);
            tmrGuncelle.Interval = TimeSpan.FromSeconds(3);
            tmrGuncelle.Tick += tmrGuncelle_Tick;          
        }

        DispatcherTimer tmrGuncelle;

        private void tmrGuncelle_Tick(object sender, EventArgs e)
        {
            Uri url = new Uri("http://bitcoin.tlkur.com/");

            WebClient client = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            String html = client.DownloadString(url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNode node = document.DocumentNode.SelectSingleNode("//*[@id=\"BTCTL\"]");
            HtmlNode btc = document.DocumentNode.SelectSingleNode("/html/body/div[2]/table/tbody/tr/td[1]/table/tbody/tr/td/div[3]/div[2]/div/div[1]/span[1]");

            if (node != null)
            {
                lblTL.Content = node.InnerText + " TL";
                lblZaman.Content = "Son Güncelleme : " + DateTime.Now.ToLongTimeString();
            }
            else
            {
                lblZaman.Content = "Son Güncelleme : Güncellenemedi";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            chkOto.IsChecked = tmrGuncelle.IsEnabled = true;
        }

        private void ChkOto_IsChecked(object sender, RoutedEventArgs e)
        {
            tmrGuncelle.IsEnabled = (bool)chkOto.IsChecked;
        }
    }
}
