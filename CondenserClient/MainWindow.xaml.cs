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
using System.Windows.Forms;
using CondenserClient.CondenserServive;
using System.Globalization;


namespace CondenserClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CondenserServiceClient client = new CondenserServiceClient("NetTcpBinding_ICondenserService");
        List<ForClient> forclient = new List<ForClient>();
        DateTime[] MyDates;
        int days = 3;
        int obj;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TG3_Click(object sender, RoutedEventArgs e)
        {

            TG3.Background = new SolidColorBrush(Color.FromRgb(143, 143, 143));
            TG4.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG5.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG6.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));

            obj = 1;

            FillTheList(); //получение данных по ТГ-3 за определенный период


            Messages.ItemsSource = forclient;

        }

        private void TG4_Click(object sender, RoutedEventArgs e)
        {

               TG3.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
                TG4.Background = new SolidColorBrush(Color.FromRgb(143, 143, 143));
                TG5.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
                TG6.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));

                obj = 2;
                days = 3;


                FillTheList(); //получение данных по ТГ-4 за определенный период

            
                Messages.ItemsSource = forclient;

        }

        private void TG5_Click(object sender, RoutedEventArgs e)
        {

            TG3.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG4.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG5.Background = new SolidColorBrush(Color.FromRgb(143, 143, 143));
            TG6.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));

            obj = 3;
            days = 4;

            FillTheList(); //получение данных по ТГ-5 за определенный период


            Messages.ItemsSource = forclient;
        }

        private void TG6_Click(object sender, RoutedEventArgs e)
        {

            TG3.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG4.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG5.Background = new SolidColorBrush(Color.FromRgb(209, 209, 209));
            TG6.Background = new SolidColorBrush(Color.FromRgb(143, 143, 143));

            obj = 4;

            FillTheList(); //получение данных по ТГ-6 за определенный период


            Messages.ItemsSource = forclient;
        }

        private void index_Checked(object sender, RoutedEventArgs e)
        {
            IndexFunction.Plot.Clear();
            if (forclient.Count > 0)
            {
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clInd1;
                    i++;
                }
                var b = IndexFunction.Plot.Add.HorizontalLine(0);
                b.Color = ScottPlot.Colors.Black;

                var g = IndexFunction.Plot.Add.HorizontalLine(5);
                g.Color = ScottPlot.Colors.Green;

                var r = IndexFunction.Plot.Add.HorizontalLine(15);
                r.Color = ScottPlot.Colors.Red;

                IndexFunction.Refresh();
                IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                IndexFunction.Refresh();
                
            }

        }


        public void FillTheList()
        {
            forclient = new List<ForClient>();

            string[][] dataFromDB = new string[20][]; // создание массива массивов, в котором будут храниться данные рассчитанных параметров в методе Calculate_Ks из DiagnosisService
            dataFromDB[0] = new string[days]; // дата смены, когда были собраны данные
            dataFromDB[1] = new string[days]; // индекс загрязненности
            dataFromDB[2] = new string[days]; // степень загрязненности
            dataFromDB[3] = new string[days]; // мощность
            dataFromDB[4] = new string[days]; // коэффициент теплопередачи 
            dataFromDB[5] = new string[days]; // давл в конд
            dataFromDB[6] = new string[days]; // расход пара в конд
            dataFromDB[7] = new string[days]; // расх охл вод в конд
            dataFromDB[8] = new string[days]; // темп вход конд
            dataFromDB[9] = new string[days]; // темп вых конд
            dataFromDB[10] = new string[days]; // давл остр пара перед турб
            dataFromDB[11] = new string[days]; // темп остр пара перед турб
            dataFromDB[12] = new string[days]; // давл в даераторе
            dataFromDB[13] = new string[days]; // ПВД7
            dataFromDB[14] = new string[days]; // ПВД6
            dataFromDB[15] = new string[days]; // ПВД5
            dataFromDB[16] = new string[days]; // ПНД4
            dataFromDB[17] = new string[days]; // ПНД3
            dataFromDB[18] = new string[days]; // ПНД2
            dataFromDB[19] = new string[days]; // ПНД1

            dataFromDB = client.GetDataForClient(obj, days);

                 MyDates = dataFromDB[0].Select(dateString => DateTime.ParseExact(dateString, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture)).ToArray();
                 float[] ind1 = dataFromDB[1].Select(float.Parse).ToArray();
                 int[] deg1 = dataFromDB[2].Select(int.Parse).ToArray();
                 float[] Nk = dataFromDB[3].Select(float.Parse).ToArray();
                 float[] k12 = dataFromDB[4].Select(float.Parse).ToArray();
                 float[] P = dataFromDB[5].Select(float.Parse).ToArray();
                 float[] G1 = dataFromDB[6].Select(float.Parse).ToArray();
                 float[] G2 = dataFromDB[7].Select(float.Parse).ToArray();
                 float[] t20 = dataFromDB[8].Select(float.Parse).ToArray();
                 float[] t2 = dataFromDB[9].Select(float.Parse).ToArray();
                 float[] p0 = dataFromDB[10].Select(float.Parse).ToArray();
                 float[] t0 = dataFromDB[11].Select(float.Parse).ToArray();
                 float[] daer = dataFromDB[12].Select(float.Parse).ToArray();
                 float[] pvd7 = dataFromDB[13].Select(float.Parse).ToArray();
                 float[] pvd6 = dataFromDB[14].Select(float.Parse).ToArray();
                 float[] pvd5 = dataFromDB[15].Select(float.Parse).ToArray();
                 float[] pnd4 = dataFromDB[16].Select(float.Parse).ToArray();
                 float[] pnd3 = dataFromDB[17].Select(float.Parse).ToArray();
                 float[] pnd2 = dataFromDB[18].Select(float.Parse).ToArray();
                 float[] pnd1 = dataFromDB[19].Select(float.Parse).ToArray();

                 string rec = ""; // рекомендации по обслуживанию
                 string descr = ""; // описание проблемы
                 string turb = ""; // код турбины

                 for (int i = 0; i < MyDates.Length; i++)
                 {
                     switch (deg1[i])
                     {
                         case 1:
                             descr = "Поверхность нагрева пучков чистая";
                             rec = "Рекомендации отсутствуют";
                             break;
                         case 2:
                             descr = "Поверхность нагрева пучков частично загрязнена";
                             rec = "Необходимо усилить наблюдение";
                             break;
                         case 3:
                             descr = "Поверхность нагрева пучков загрязнена";
                             rec = "Необходимо произвести чистку";
                             break;
                     }

                     switch (obj)
                     {
                         case 1:
                             turb = "ТГ-3";
                             break;
                         case 2:
                             turb = "ТГ-4";
                             break;
                         case 3:
                             turb = "ТГ-5";
                             break;
                         case 4:
                             turb = "ТГ-6";
                             break;
                     }


                   forclient.Add(new ForClient //запись считанных данных в лист
                     {
                         clMyDate = MyDates[i],
                         clInd1 = ind1[i],
                         clDeg1 = deg1[i],
                         clDescription = descr,
                         clRecommendation = rec,
                         clObj = turb,
                         clSubSistem = "Конденсатор",
                         clG1 = G1[i], 
                         clG2 = G2[i],
                         clNk = Nk[i],
                         clP = P[i],
                         clp0 = p0[i],
                         clpDaer = daer[i],
                         clPND1 = pnd1[i],
                         clPND2 = pnd2[i],
                         clPND3 = pnd3[i],
                         clPND4 = pnd4[i],
                         clPVD5 = pvd5[i],
                         clPVD6 = pvd6[i],   
                         clPVD7 = pvd7[i],
                         clt0 = t0[i],
                         clT2 = t2[i],
                         clT20 = t20[i],
                         ClK12 = k12[i]
                     });

         }
        }

        private void Nk_Checked(object sender, RoutedEventArgs e)
        {
            
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clNk;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();
                
            }
        }

        private void P_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clP;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void k12_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.ClK12;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void G1_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clG1;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void G2_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clG2;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t20_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clT20;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t2_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clT2;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void p0_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clp0;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t0_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clt0;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void p_daer_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clpDaer;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pvd7_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPVD7;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pvd6_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPVD6;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pvd5_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPVD5;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pnd4_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPND4;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pnd3_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPND3;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pnd2_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPND2;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }

        private void t_pnd1_Checked(object sender, RoutedEventArgs e)
        {
            if (forclient.Count > 0)
            {
                IndexFunction.Plot.Clear();
                float[] graphData = new float[days];
                int i = 0;
                foreach (ForClient piece in forclient)
                {
                    graphData[i] = piece.clPND1;
                    i++;
                }
                IndexFunction.Plot.Axes.DateTimeTicksBottom();
                var l = IndexFunction.Plot.Add.Scatter(MyDates, graphData);
                IndexFunction.Plot.Axes.AutoScale();
                IndexFunction.Refresh();

            }
        }
    }
}
