using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CondenserClient.CondenserServive;

namespace CondenserClient
{
    /// <summary>
    /// Логика взаимодействия для InsertData.xaml
    /// </summary>
    public partial class InsertData : Window
    {
        CondenserServiceClient client = new CondenserServiceClient("NetTcpBinding_ICondenserService");

        public InsertData()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] measureIntoDB = new string[16];

            // проверка на заполненность
            if ((tbNk.Text == "") || (tbp0.Text == "") || (tbt0.Text == "") || (tbdaer.Text == "") || (tbpvd7.Text == "") || (tbpvd6.Text == "") || (tbpvd5.Text == "") ||
                (tbpnd4.Text == "") || (tbpnd3.Text == "") || (tbpnd2.Text == "") || (tbpnd1.Text == "") || (tbP.Text == "") || (tbt20.Text == "") || (tbt2.Text == "") || (dpDate.SelectedDate == null) || (cbObj.SelectedItem == null)) 
            {
               MessageBox.Show("Заполните все поля!");
            }
            else
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cbObj.SelectedItem; // для получения объекта

                // Получаем значение контента выбранного элемента
                string selectedValue = selectedItem.Content.ToString();

                // проверка на верный формат всех данных
                if ((tbNk.BorderBrush == Brushes.Red) || (tbt0.BorderBrush == Brushes.Red) || (tbp0.BorderBrush == Brushes.Red) || (tbdaer.BorderBrush == Brushes.Red) ||(tbpvd7.BorderBrush == Brushes.Red) ||(tbpvd6.BorderBrush == Brushes.Red)
                    || (tbpvd5.BorderBrush == Brushes.Red) || (tbpnd4.BorderBrush == Brushes.Red) || (tbpnd3.BorderBrush == Brushes.Red) || (tbpnd2.BorderBrush == Brushes.Red) || (tbpnd1.BorderBrush == Brushes.Red) ||
                    (tbP.BorderBrush == Brushes.Red) || (tbt20.BorderBrush == Brushes.Red) || (tbt2.BorderBrush == Brushes.Red))
                {
                    MessageBox.Show("Введенные значения не соответствуют формату!");
                }
                else
                {
                    // получение данных с форм
                    switch (selectedValue)
                    {
                        case "ТГ-3":
                            measureIntoDB[0] = "1";
                            break;
                        case "ТГ-4":
                            measureIntoDB[0] = "2";
                            break;
                        case "ТГ-5":
                            measureIntoDB[0] = "3";
                            break;
                        case "ТГ-6":
                            measureIntoDB[0] = "4";
                            break;
                    }

                    measureIntoDB[1] = dpDate.SelectedDate.ToString();
                    measureIntoDB[2] = tbNk.Text;
                    measureIntoDB[3] = tbp0.Text; 
                    measureIntoDB[4] = tbt0.Text;
                    measureIntoDB[5] = tbdaer.Text;
                    measureIntoDB[6] = tbpvd7.Text; 
                    measureIntoDB[7] = tbpvd6.Text; 
                    measureIntoDB[8] = tbpvd5.Text;
                    measureIntoDB[9] = tbpnd4.Text; 
                    measureIntoDB[10] = tbpnd3.Text;
                    measureIntoDB[11] = tbpnd2.Text;
                    measureIntoDB[12] = tbpnd1.Text;
                    measureIntoDB[13] = tbP.Text;
                    measureIntoDB[14] = tbt20.Text;
                    measureIntoDB[15] = tbt2.Text;

                    client.InsertMesIntoDB(measureIntoDB); // передача на сервер и запись в бд

                    //очистка форм
                    tbNk.Text = "";
                    tbp0.Text = "";
                    tbt0.Text = "";
                    tbdaer.Text = "";
                    tbpvd7.Text = "";
                    tbpvd6.Text = "";
                    tbpvd5.Text = "";
                    tbpnd4.Text = "";
                    tbpnd3.Text = "";
                    tbpnd2.Text = "";
                    tbpnd1.Text = "";
                    tbP.Text = "";
                    tbt20.Text = "";
                    tbt2.Text = "";

                    MessageBox.Show("Данные записаны");
                }
            }
        }

        private void tbNk_TextChanged(object sender, TextChangedEventArgs e) // проверка на допустимые символы
        {
            string Input = tbNk.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbNk.BorderBrush = Brushes.Red;
            }
            else
            {
                tbNk.BorderBrush = Brushes.Gray;
            }
        }

        private void tbp0_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbp0.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbp0.BorderBrush = Brushes.Red;
            }
            else
            {
                tbp0.BorderBrush = Brushes.Gray;
            }
        }

        private void tbt0_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbt0.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbt0.BorderBrush = Brushes.Red;
            }
            else
            {
                tbt0.BorderBrush = Brushes.Gray;
            }
        }

        private void tbdaer_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbdaer.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbdaer.BorderBrush = Brushes.Red;
            }
            else
            {
                tbdaer.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpvd7_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpvd7.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpvd7.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpvd7.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpvd6_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpvd6.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpvd6.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpvd6.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpvd5_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpvd5.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpvd5.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpvd5.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpnd4_Копировать_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpnd4.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpnd4.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpnd4.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpnd3_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpnd3.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpnd3.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpnd3.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpnd2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpnd2.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpnd2.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpnd2.BorderBrush = Brushes.Gray;
            }
        }

        private void tbpnd1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbpnd1.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbpnd1.BorderBrush = Brushes.Red;
            }
            else
            {
                tbpnd1.BorderBrush = Brushes.Gray;
            }
        }

        private void tbP_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbP.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbP.BorderBrush = Brushes.Red;
            }
            else
            {
                tbP.BorderBrush = Brushes.Gray;
            }
        }

        private void tbt20_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbt20.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbt20.BorderBrush = Brushes.Red;
            }
            else
            {
                tbt20.BorderBrush = Brushes.Gray;
            }
        }

        private void tbt2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Input = tbt2.Text;
            if (!Regex.IsMatch(Input, @"^[0-9,]+$"))
            {
                // Ввод не соответствует требованиям
                tbt2.BorderBrush = Brushes.Red;
            }
            else
            {
                tbt2.BorderBrush = Brushes.Gray;
            }
        }

        private void cbObj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный ComboBoxItem
            ComboBoxItem selectedItem = (ComboBoxItem)cbObj.SelectedItem;

            // Получаем значение контента выбранного элемента
            string selectedValue = selectedItem.Content.ToString();
            if (selectedValue == "ТГ-4")
            {
                lbMessage.Content = "ТГ-4 вводится в систему \nавтоматически";
            }
            else
            {
                lbMessage.Content = "";
            }
        }
    }
}
