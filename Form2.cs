using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;


namespace yazlabdeneme
{
    public partial class Form2 : Form
    {
        private List<PointLatLng> noktalar;

        int zamanfark1;
        int zamanfark2;
        int zamanfark3;
        int zamanfark4;
        int zamanfark5;

        String kullaniciName = "burakemir";
        String kullaniciSifre = "emir123";

        string[] araba1time = new string[1000000];
        double[] araba1lat = new double[100000];
        double[] araba1lng = new double[100000];
        string[] araba1car = new string[100000];

        string[] araba2time = new string[1000000];
        double[] araba2lat = new double[100000];
        double[] araba2lng = new double[100000];
        string[] araba2car = new string[100000];

        string[] araba3time = new string[1000000];
        double[] araba3lat = new double[100000];
        double[] araba3lng = new double[100000];
        string[] araba3car = new string[100000];

        string[] araba4time = new string[1000000];
        double[] araba4lat = new double[100000];
        double[] araba4lng = new double[100000];
        string[] araba4car = new string[100000];

        string[] araba5time = new string[100000];
        double[] araba5lat = new double[100000];
        double[] araba5lng = new double[100000];
        string[] araba5car = new string[100000];

        bool User1 = false;
        bool User2 = false;

        Form opener;

        public Form2(Form parentForm, String kullanici)
        {
            if (kullanici == "burak")
            {
                User1 = true;
            }

            if (kullanici == "fatih")
            {
                User2 = true;
            }
            InitializeComponent();
            label1.BackColor = System.Drawing.Color.Transparent;
            opener = parentForm;

        }

        public void Monggo()
        {

        }



        private void Form2_Load(object sender, EventArgs e)
        {

            var dbClient = new MongoClient("mongodb+srv://fatihtekin:123@cluster0.stzil.mongodb.net/node-blog?retryWrites=true&w=majority");
            IMongoDatabase db = dbClient.GetDatabase("node-blog");
            var cars = db.GetCollection<BsonDocument>("blogs");
            var documents = cars.Find(new BsonDocument()).ToList();

            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int ee = 0;



            foreach (BsonDocument doc in documents)
            {

                if (doc[4] == 1)
                {
                    araba1time[a] = (string)doc[1];
                    araba1lat[a] = Convert.ToDouble(doc[2]);
                    araba1lng[a] = Convert.ToDouble(doc[3]);
                    araba1car[a] = doc[4].ToString();
                    //Console.WriteLine("1 " + araba1time[a]);  
                    a++;


                }

                if (doc[4] == 2)
                {
                    araba2time[b] = (string)doc[1];
                    araba2lat[b] = Convert.ToDouble(doc[2]);
                    araba2lng[b] = Convert.ToDouble(doc[3]);
                    araba2car[b] = doc[4].ToString();

                    b++;

                }

                if (doc[4] == 3)
                {
                    araba3time[c] = (string)doc[1];
                    araba3lat[c] = Convert.ToDouble(doc[2]);
                    araba3lng[c] = Convert.ToDouble(doc[3]);
                    araba3car[c] = doc[4].ToString();
                    //Console.WriteLine("3 " + araba3time[a]);
                    c++;
                }

                if (doc[4] == 4)
                {
                    araba4time[d] = (string)doc[1];
                    araba4lat[d] = Convert.ToDouble(doc[2]);
                    araba4lng[d] = Convert.ToDouble(doc[3]);
                    araba4car[d] = doc[4].ToString();
                    d++;

                }

                if (doc[4] == 5)
                {
                    araba5time[ee] = (string)doc[1];
                    araba5lat[ee] = Convert.ToDouble(doc[2]);
                    araba5lng[ee] = Convert.ToDouble(doc[3]);
                    araba5car[ee] = doc[4].ToString();
                    //Console.WriteLine("5 " + araba5time[a]);
                    ee++;

                }


            }

            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAVHRhwshNUdWUr5FmBfTExhUfUUGk4Nss";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";

            map.DragButton = MouseButtons.Left;

            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.MinZoom = 10;
            map.MaxZoom = 18;
            map.Zoom = 10;
            map.Position = new PointLatLng(59.42190595212989, 17.822133881640568);



        }

        private void button1_Click(object sender, EventArgs e)
        {

            while (map.Overlays.Count > 0)
            {
                map.Overlays.RemoveAt(0);
            }

            map.Refresh();

            int sayi = Convert.ToInt32(txtt.Text);

            for (int ab = 1; ab < 44; ab++)
            {
                string endTime = "2018-10-04 07:25";

                TimeSpan duration1 = DateTime.Parse(araba1time[45]).Subtract(DateTime.Parse(araba1time[ab]));
                TimeSpan duration2 = DateTime.Parse(araba1time[45]).Subtract(DateTime.Parse(araba2time[ab]));
                TimeSpan duration3 = DateTime.Parse(araba1time[45]).Subtract(DateTime.Parse(araba3time[ab]));
                TimeSpan duration4 = DateTime.Parse(araba1time[45]).Subtract(DateTime.Parse(araba4time[ab]));
                TimeSpan duration5 = DateTime.Parse(araba1time[45]).Subtract(DateTime.Parse(araba5time[ab]));

                zamanfark1 = (int)duration1.TotalMinutes;
                zamanfark2 = (int)duration2.TotalMinutes;
                zamanfark3 = (int)duration3.TotalMinutes;
                zamanfark4 = (int)duration4.TotalMinutes;
                zamanfark5 = (int)duration5.TotalMinutes;

                if (((sayi > zamanfark1) && (zamanfark1 > 0)) && User1)
                {

                    double lat = araba1lat[ab];
                    double lng = araba1lng[ab];
                    PointLatLng nokta = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.pink_dot);
                    GMapOverlay markers = new GMapOverlay("markers");

                    markers.Markers.Add(marker);

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Offset = new Point(0,-40),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(10,10);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = "Araba numarası: " + araba1car[ab] + "\nKonumda görüldüğü zaman: " + araba1time[ab];

                    map.Overlays.Add(markers);

                }

                if (((sayi > zamanfark2) && (zamanfark2 > 0)) && User1)
                {

                    double lat = araba2lat[ab];
                    double lng = araba2lng[ab];
                    PointLatLng nokta = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.green_dot);
                    GMapOverlay markers = new GMapOverlay("markers");

                    markers.Markers.Add(marker);

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Offset = new Point(0, -40),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(10, 10);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = "Araba numarası: " + araba2car[ab] + "\nKonumda görüldüğü zaman: " + araba2time[ab];

                    map.Overlays.Add(markers);

                }

                if (((sayi > zamanfark3) && (zamanfark3 > 0)) && User2)
                {

                    double lat = araba3lat[ab];
                    double lng = araba3lng[ab];
                    PointLatLng nokta = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.yellow_dot);
                    GMapOverlay markers = new GMapOverlay("markers");

                    markers.Markers.Add(marker);

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Offset = new Point(0, -40),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(10, 10);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = "Araba numarası: " + araba3car[ab] + "\nKonumda görüldüğü zaman: " + araba3time[ab];

                    map.Overlays.Add(markers);

                }

                if (((sayi > zamanfark4) && (zamanfark4 > 0)) && User2)
                {
                    double lat = araba4lat[ab];
                    double lng = araba4lng[ab];
                    PointLatLng nokta = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.blue_dot);
                    GMapOverlay markers = new GMapOverlay("markers");

                    markers.Markers.Add(marker);

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Offset = new Point(0, -40),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(10, 10);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = "Araba numarası: " + araba4car[ab] + "\nKonumda görüldüğü zaman: " + araba4time[ab];

                    map.Overlays.Add(markers);

                }

                if (((sayi > zamanfark5) && (zamanfark5 > 0)) && User2)
                {
                    double lat = araba5lat[ab];
                    double lng = araba5lng[ab];
                    PointLatLng nokta = new PointLatLng(lat, lng);  
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.red_dot);
                    GMapOverlay markers = new GMapOverlay("markers");

                    markers.Markers.Add(marker);

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Offset = new Point(0, -40),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(10, 10);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = "Araba numarası: " + araba5car[ab] + "\nKonumda görüldüğü zaman: " + araba5time[ab];

                    map.Overlays.Add(markers);

                }


            }
        }

            private void SignIn()
            {

            }

            private void map_Load(object sender, EventArgs e)
            {

            }

  

        private void txtt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    }





    

