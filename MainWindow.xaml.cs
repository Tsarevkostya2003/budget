using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;
using System.IO;
using System.Data;

namespace OAP_Ex4
{

   

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {
        public List<string> sbud = new List<string>();
        public  List<Zametka> szam = new List<Zametka>();
        public List<Zametka> szamcur = new List<Zametka>();

        public MainWindow()
        {

            readjson1("C:\\Games\\Zametka.json");
            InitializeComponent();
            DateTime curdate = DateTime.Today;
            kalend.SelectedDate = curdate;

            readjson("c:\\games\\budget.json");
            InitializeComponent();
            szamcur.Clear();
            foreach (Zametka z in szam)
            {
                if (z.vrema == curdate)
                    { 
                        szamcur.Add(z);
                    }
            }

            MyTable.ItemsSource = szamcur;
            Lb.ItemsSource= sbud;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Res = new Window1();
            if (Res.ShowDialog() == true)
            {      
   
                sbud.Add(Res.Vbt);                
                Lb.ItemsSource= sbud;
                Lb.Items.Refresh();
                savejson("c:\\Games\\budget.json");
             
            }



        }
        public void savejson(string cF) // сериализация бюджета в файл   cf-имя файла
        {
            // Сериализация в json
            string json = JsonConvert.SerializeObject(sbud);
            File.WriteAllText(cF, json);

        }

        public void savejson1(string cF) // сериализация заметок
        {
            // Сериализация в json
            string json = JsonConvert.SerializeObject(szam);
            File.WriteAllText(cF, json);
        }

            public void readjson(string cF) // десерелизируем json из файла cSt
        {
            string cSt;
            cSt = File.ReadAllText(cF);
            sbud = JsonConvert.DeserializeObject<List<string>>(cSt);
           
        }

        public void readjson1(string cF) // десерелизируем json из файла cSt
        {
            string cSt;
            cSt = File.ReadAllText(cF);
            szam = JsonConvert.DeserializeObject<List<Zametka>>(cSt);

        }

        private void bD_Click(object sender, RoutedEventArgs e)

        { 
            Boolean vichet;
            double vv;
            if ( Convert.ToDouble( nS.Text) < 0)
            { 
                vichet = false;
                vv = Convert.ToDouble(nS.Text) * -1;
            }
            else 
            {
                vichet= true;
                vv = Convert.ToDouble(nS.Text);
            }

            szam.Add(new Zametka(NewZ.Text, vv  , vichet  ,Convert.ToString( Lb.SelectedItem), (DateTime) kalend.SelectedDate));
            MyTable.ItemsSource = null;
            szamcur.Clear();
            foreach (Zametka z in szam)
            {
                if (z.vrema == (DateTime) kalend.SelectedDate)
                {
                    szamcur.Add(z);
                }
            }
            MyTable.ItemsSource = szamcur;
            savejson1("C:\\Games\\Zametka.json"); // сохраняем массив в файл 
        }

        private void kalend_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MyTable.ItemsSource = null;
            szamcur.Clear();
            foreach (Zametka z in szam)
            {
                if (z.vrema == (DateTime)kalend.SelectedDate)
                {
                    szamcur.Add(z);
                }
            }
            MyTable.ItemsSource = szamcur;

        }

        private void MyTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Zametka f = (Zametka)MyTable.SelectedItem;
            if (f != null)
            {
                NewZ.Text = f.Name;
                if (f.Tip == true)
                {
                    nS.Text = f.sum.ToString();
                }
                else
                {
                    nS.Text = (f.sum * (-1)).ToString();
                }
                Lb.SelectedItem = f.vid;
            }
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Boolean vichet;
            double vv;
            if (Convert.ToDouble(nS.Text) < 0)
            {
                vichet = false;
                vv = Convert.ToDouble(nS.Text) * -1;
            }
            else
            {
                vichet = true;
                vv = Convert.ToDouble(nS.Text);
            }

            Zametka n = (Zametka)MyTable.SelectedItem;


            foreach (Zametka z in szam)
            {
                if (z.Name == n.Name & z.Tip == n.Tip & z.vid == n.vid)
                {
                    z.Name = NewZ.Text;
                    z.vid = Convert.ToString(Lb.SelectedItem);
                    z.sum = vv;
                    z.Tip = vichet;
                }
            }
           
            MyTable.ItemsSource = null;
            szamcur.Clear();
            foreach (Zametka z in szam)
            {
                if (z.vrema == (DateTime)kalend.SelectedDate)
                {
                    szamcur.Add(z);
                }
            }
            MyTable.ItemsSource = szamcur;
            savejson1("C:\\Games\\Zametka.json"); // сохраняем массив в файл 

        }
    }
}
