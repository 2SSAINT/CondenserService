using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using math_model_t100_con;
using TsP;
using MathWorks.MATLAB.NET.Arrays;
using Knew;

namespace CondenserService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "CondenserService" в коде и файле конфигурации.
    public class CondenserService : ICondenserService
    {
        public void Calculate_k(int obj)
        {
            DB.ConnectionDB();
            Console.WriteLine("калькуляция");

            double P = 0;
            
            string[] forK = new string[15];
            forK = DB.GetDataForService(obj);
            Console.WriteLine(forK[0], forK[1]);
            math_model_t100_con.Balance bal = new Balance();// нахождение G1 и G2 при помощи сведения балансов
            MWArray Gs = bal.math_model_t100_con((MWArray)double.Parse(forK[0]), (MWArray)double.Parse(forK[1]), (MWArray)double.Parse(forK[2]), (MWArray)double.Parse(forK[3]), (MWArray)double.Parse(forK[4]), (MWArray)double.Parse(forK[5]), (MWArray)double.Parse(forK[6]), (MWArray)double.Parse(forK[7]),
                (MWArray)double.Parse(forK[8]), (MWArray)double.Parse(forK[9]), (MWArray)double.Parse(forK[10]), (MWArray)double.Parse(forK[13]), (MWArray)double.Parse(forK[11]), (MWArray)double.Parse(forK[12]));

            P = double.Parse(forK[13]) * 0.09801; //Перевод кг/см2 в МПа

            Ts ts = new Ts();
            MWArray tn = ts.TsP((MWArray)P); // Превод значения вакуума в температуру насыщенного пара

            Knew.K12 k = new K12();
            MWArray k12 = k.Knew((MWArray)double.Parse(forK[11]), (MWArray)double.Parse(forK[12]), tn, Gs[10], Gs[11]); //расчет коэффициента теплопередачи

            double k12_toDB = double.Parse(k12.ToString());
            double ind1 = 100 - ((k12_toDB / 3800) * 100); // расчет индекса загрязненности
            int deg1;

            if (ind1 > 5) // определение степени загрязненности за счет сравнения индекса с уставками из нормативных документов
            {
                if (ind1 <= 15)
                {
                    deg1 = 2;
                }
                else { deg1 = 3; }
            }
            else
            {
                deg1 = 1;
            }

            var culture = System.Globalization.CultureInfo.InvariantCulture;// для перевода G во float

            DB.InsertCalculations(int.Parse(forK[14]), double.Parse(Gs[10].ToString(), culture), double.Parse(Gs[11].ToString(), culture), ind1, deg1, k12_toDB); // запись данных в бд

            DB.CloseBD();
        }

        public void DoWork()
        {
        }

        public string[][] GetDataForClient(int obj, int days)
        {
            DB.ConnectionDB();
            
            string[][] dataFromDB = new string[20][];
            dataFromDB[0] = new string[days];
            dataFromDB[1] = new string[days];
            dataFromDB[2] = new string[days];
            dataFromDB[3] = new string[days];
            dataFromDB[4] = new string[days];
            dataFromDB[5] = new string[days];
            dataFromDB[6] = new string[days];
            dataFromDB[7] = new string[days];
            dataFromDB[8] = new string[days];
            dataFromDB[9] = new string[days];
            dataFromDB[10] = new string[days];
            dataFromDB[11] = new string[days];
            dataFromDB[12] = new string[days];
            dataFromDB[13] = new string[days];
            dataFromDB[14] = new string[days];
            dataFromDB[15] = new string[days];
            dataFromDB[16] = new string[days];
            dataFromDB[17] = new string[days];
            dataFromDB[18] = new string[days];
            dataFromDB[19] = new string[days];

            dataFromDB = DB.QuerryForClient(obj, days);
            DB.CloseBD();
            return dataFromDB;
        }

        public void InsertMesIntoDB(string[] intoDB)
        {
            DB.ConnectionDB();
            DB.insertMeasuresIntoDB(intoDB); // запись показателей работы
            DB.CloseBD();
            int obj = int.Parse(intoDB[0]);
            Calculate_k(obj); // рассчеты по введенным показателям работы
        }
    }
}
