using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Maps
{
    public partial class Form1 : Form
    {
        private List<PointLatLng> noktalar;

        ArrayList kargoID = new ArrayList();
        ArrayList kargoAD = new ArrayList();
        ArrayList kargoIF = new ArrayList();
        ArrayList kargoLat = new ArrayList();
        ArrayList kargoLng = new ArrayList();
        ArrayList kargoDurum = new ArrayList();




        public Form1()
        {
            InitializeComponent();
            noktalar = new List<PointLatLng>();
        }




        List<int> routeOverlays = new List<int>();


        private void mySqlRdy()
        {
            string cs = @"server=34.118.75.75;userid=root;password=1234;database=kargo";

            var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM kargobilgi";
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                kargoID.Add(rdr.GetString(0));
                kargoAD.Add(rdr.GetString(1));
                kargoIF.Add(rdr.GetString(2));
                kargoLat.Add(rdr.GetString(3));
                kargoLng.Add(rdr.GetString(4));
                kargoDurum.Add(rdr.GetString(5));
            }
        }

       



        private void Form1_Load_1(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyA8pfiDIbQygeURLhkIFQzBJjKlWB0_9oE";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.CacheLocation = @"cache";

            map.DragButton = MouseButtons.Left;

            map.MapProvider = GMapProviders.GoogleMap;
            map.ShowCenter = false;
            map.MinZoom = 10;
            map.MaxZoom = 18;
            map.Zoom = 14;
            map.Position = new PointLatLng(40.766666, 29.916668);
            
        }



        private void DenemeButton_Click(object sender, EventArgs e)
        {

            
            
            noktalar.Clear();
            kargoAD.Clear();
            kargoID.Clear();
            kargoIF.Clear();
            kargoLat.Clear();
            kargoLng.Clear();
            kargoDurum.Clear();
            
                
                    while (map.Overlays.Count > 0)
                    {
                        map.Overlays.RemoveAt(0);
                    }
                
                map.Refresh();

            
           
            Console.WriteLine(map.Overlays.Count+" "+ noktalar.Count+ " " +kargoLat.Count);
            mySqlRdy();

            GMapOverlay markers = new GMapOverlay("markers");
            for (int a = 0; a < kargoLat.Count; a++)
            {

                string durum = "" + kargoDurum[a];

                if (durum == "Teslim Edilmedi")
                {

                    double lat = Convert.ToDouble(kargoLat[a]);
                    double lng = Convert.ToDouble(kargoLng[a]);
                    PointLatLng nokta = new PointLatLng(lat, lng);
                    GMapMarker marker = new GMarkerGoogle(nokta, GMarkerGoogleType.red_dot);

                    markers.Markers.Add(marker);

                    noktalar.Add(new PointLatLng(Convert.ToDouble(lat),
                     Convert.ToDouble(lng)));

                    var tooltip = new GMap.NET.WindowsForms.ToolTips.GMapBaloonToolTip(marker)
                    {
                        Stroke = new Pen(new SolidBrush(Color.Black)),
                        Font = new Font("Arial", 9, FontStyle.Bold | FontStyle.Underline),
                        Fill = new SolidBrush(Color.Gray),
                        Foreground = new SolidBrush(Color.White)
                    };
                    marker.ToolTip = tooltip;
                    marker.ToolTip.TextPadding = new Size(0, 0);
                    marker.ToolTip.Format.Alignment = StringAlignment.Far;

                    marker.ToolTipText = $"{kargoAD[a]}\n{kargoIF[a]}";

                }



            }
            map.Overlays.Add(markers);

            int[] matrix = new int[noktalar.Count];
            

            for (int a = 0; a < noktalar.Count-1; a++)
            {
                GDirections rota;
                GMapProviders.GoogleMap.GetDirections(out rota, noktalar[a], noktalar[a+1], false, false, false, false, false);
                var r = new GMapRoute(rota.Route, "My Route")
                {
                    Stroke = new Pen(Color.DarkViolet, 5)
                };

                var routes = new GMapOverlay("routes");
                routes.Routes.Add(r);
                map.Overlays.Add(routes);
                matrix[a] = (int)rota.DistanceValue;

            }
            
        }
    }
}

