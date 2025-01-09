using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knew;
using MySql.Data.MySqlClient;

namespace CondenserService
{
    public class DB
    {
        static string DBConnetct = "server=localhost;port=3306;database=condenser_diagnodis;uid=root;password=root;";
        static public MySqlDataAdapter myDataAdapter;
        static MySqlConnection myconnect;
        static public MySqlCommand myCommand;
        static public MySqlDataReader dataReader;


        public static bool ConnectionDB()
        {
            try
            {
                myconnect = new MySqlConnection(DBConnetct);
                myconnect.Open();
                if (myconnect.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Он открыт");
                }
                myCommand = new MySqlCommand();
                myCommand.Connection = myconnect;
                myDataAdapter = new MySqlDataAdapter(myCommand);
                return true;
            }
            catch
            {
                Console.WriteLine("Ошибка соединения с БД!");
                return false;
            }
        }

        public static string[] GetDataForService(int obj) // получение данных для метода Calculate_k, который рассчитывает степень загрязнения конденсатора
        {
            string[] result = new string[15];
            Console.WriteLine("Я в базе");
            string query = "SELECT measure_Nk, measure_p0, measure_t0, measure_p_daer, measure_pvd7_out, measure_pvd6_out, measure_pvd5_out, measure_pnd4_out, measure_pnd3_out, measure_pnd2_out, measure_pnd1_out, measure_t20, measure_t2, measure_P, measure_shift FROM measure, shift WHERE measure_shift = shift_id AND shift_object = @obj ORDER BY measure_id DESC LIMIT 1";
            MySqlCommand myComm = new MySqlCommand(query, myconnect);
            try
            {
                myCommand = new MySqlCommand(query, myconnect);
                myCommand.Parameters.AddWithValue("@obj", obj);
                dataReader = myCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    result[0] = Convert.ToString(dataReader["measure_Nk"]);
                    result[1] = Convert.ToString(dataReader["measure_p0"]);
                    result[2] = Convert.ToString(dataReader["measure_t0"]);
                    result[3] = Convert.ToString(dataReader["measure_p_daer"]);
                    result[4] = Convert.ToString(dataReader["measure_pvd7_out"]);
                    result[5] = Convert.ToString(dataReader["measure_pvd6_out"]);
                    result[6] = Convert.ToString(dataReader["measure_pvd5_out"]);
                    result[7] = Convert.ToString(dataReader["measure_pnd4_out"]);
                    result[8] = Convert.ToString(dataReader["measure_pnd3_out"]);
                    result[9] = Convert.ToString(dataReader["measure_pnd2_out"]);
                    result[10] = Convert.ToString(dataReader["measure_pnd1_out"]);
                    result[11] = Convert.ToString(dataReader["measure_t20"]);
                    result[12] = Convert.ToString(dataReader["measure_t2"]);
                    result[13] = Convert.ToString(dataReader["measure_P"]);
                    result[14] = Convert.ToString(dataReader["measure_shift"]);
                }
            }
            catch
            {
                Console.WriteLine("Не сработал даталист");
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
            return result;
        }

        public static void InsertCalculations(int shift, double G1, double G2, double ind1, int deg1, double k12) //запись в БД результатов рассчетов матмоделей
        {
            Console.WriteLine("В БАЗУ");
            using (MySqlConnection connection = new MySqlConnection(DBConnetct))
            {
                Console.WriteLine("В БАЗУ");
                string query = "INSERT INTO calculate (calculate_id, calculate_shift, calculate_G1, calculate_G2, calculate_index, calculate_deg, calculate_k12) VALUES (NULL, @value1, @value2, @value3, @value4, @value5, @value6)";
                myCommand = new MySqlCommand(query, myconnect);
                myCommand.Parameters.AddWithValue("@value1", shift);
                myCommand.Parameters.AddWithValue("@value2", G1);
                myCommand.Parameters.AddWithValue("@value3", G2);
                myCommand.Parameters.AddWithValue("@value4", ind1);
                myCommand.Parameters.AddWithValue("@value5", deg1);
                myCommand.Parameters.AddWithValue("@value6", k12);
                myCommand.ExecuteNonQuery();
            }

        }

       
        public static void CloseBD()
        {
            myconnect.Close();
        }

        public static string[][] QuerryForClient(int obj, int days) // получение данных для клиента о показателях работы оборудования и степени загрязнения конденсатора
        {
            string query = "SELECT shift_date, calculate_index, calculate_deg, calculate_G1, calculate_G2, calculate_k12, measure_Nk, measure_t20, measure_t2, measure_P, measure_p0, measure_t0, measure_p_daer, measure_pvd7_out, measure_pvd6_out, measure_pvd5_out, measure_pnd4_out, measure_pnd3_out, measure_pnd2_out, measure_pnd1_out FROM" +
                " shift, calculate, measure WHERE shift_object = @obj AND measure_shift = calculate_shift AND shift_id = calculate_shift ORDER BY shift_id LIMIT @days;"; //устанавливается количество
            string[][] Arr = new string[20][];
            Arr[0] = new string[days];
            Arr[1] = new string[days];
            Arr[2] = new string[days];
            Arr[3] = new string[days];
            Arr[4] = new string[days];
            Arr[5] = new string[days]; 
            Arr[6] = new string[days];
            Arr[7] = new string[days];
            Arr[8] = new string[days];
            Arr[9] = new string[days];
            Arr[10] = new string[days];
            Arr[11] = new string[days];
            Arr[12] = new string[days];
            Arr[13] = new string[days];
            Arr[14] = new string[days];
            Arr[15] = new string[days];
            Arr[16] = new string[days];
            Arr[17] = new string[days];
            Arr[18] = new string[days];
            Arr[19] = new string[days];
            try
            {
                myCommand = new MySqlCommand(query, myconnect);
                myCommand.Parameters.AddWithValue("@obj", obj);
                myCommand.Parameters.AddWithValue("@days", days);
                dataReader = myCommand.ExecuteReader();
                int id = 0;
                while (dataReader.Read())
                {
                    Arr[0][id] = Convert.ToString(dataReader["shift_date"]);
                    Arr[1][id] = Convert.ToString(dataReader["calculate_index"]);
                    Arr[2][id] = Convert.ToString(dataReader["calculate_deg"]);
                    Arr[3][id] = Convert.ToString(dataReader["measure_Nk"]);
                    Arr[4][id] = Convert.ToString(dataReader["calculate_k12"]);
                    Arr[5][id] = Convert.ToString(dataReader["measure_P"]);
                    Arr[6][id] = Convert.ToString(dataReader["calculate_G1"]);
                    Arr[7][id] = Convert.ToString(dataReader["calculate_G2"]);
                    Arr[8][id] = Convert.ToString(dataReader["measure_t20"]);
                    Arr[9][id] = Convert.ToString(dataReader["measure_t2"]);
                    Arr[10][id] = Convert.ToString(dataReader["measure_p0"]);
                    Arr[11][id] = Convert.ToString(dataReader["measure_t0"]);
                    Arr[12][id] = Convert.ToString(dataReader["measure_p_daer"]);
                    Arr[13][id] = Convert.ToString(dataReader["measure_pvd7_out"]);
                    Arr[14][id] = Convert.ToString(dataReader["measure_pvd6_out"]);
                    Arr[15][id] = Convert.ToString(dataReader["measure_pvd5_out"]);
                    Arr[16][id] = Convert.ToString(dataReader["measure_pnd4_out"]);
                    Arr[17][id] = Convert.ToString(dataReader["measure_pnd3_out"]);
                    Arr[18][id] = Convert.ToString(dataReader["measure_pnd2_out"]);
                    Arr[19][id] = Convert.ToString(dataReader["measure_pnd1_out"]);
                    id++;
                }
            }
            catch
            {
                Console.WriteLine("Не сработал даталист");
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
            return Arr;
        }

        public static void insertMeasuresIntoDB(string[] intoDB) // запись в БД показателей работы турбины, введенных пользователем
        {
            int obj = int.Parse(intoDB[0]);
            DateTime shift = Convert.ToDateTime(intoDB[1]);
            int shiftID = 0;
            float Nk = float.Parse(intoDB[2]);
            float p0 = float.Parse(intoDB[3]);
            float t0 = float.Parse(intoDB[4]);
            float daer = float.Parse(intoDB[5]);
            float pvd7 = float.Parse(intoDB[6]);
            float pvd6 = float.Parse(intoDB[7]);
            float pvd5 = float.Parse(intoDB[8]);
            float pnd4 = float.Parse(intoDB[9]);
            float pnd3 = float.Parse(intoDB[10]);
            float pnd2 = float.Parse(intoDB[11]);
            float pnd1 = float.Parse(intoDB[12]);
            float P = float.Parse(intoDB[13]);
            float t20 = float.Parse(intoDB[14]);
            float t2 = float.Parse(intoDB[15]);

            using (MySqlConnection connection = new MySqlConnection(DBConnetct))
            {

                string query1 = "INSERT INTO shift (shift_id, shift_date, shift_object) VALUES (NULL, @date, @obj)";
                myCommand = new MySqlCommand(query1, myconnect);
                myCommand.Parameters.AddWithValue("@date", shift);
                myCommand.Parameters.AddWithValue("@obj", obj);
                myCommand.ExecuteNonQuery();

                try
                {
                    string query2 = "SELECT shift_id FROM shift WHERE shift_date = @date AND shift_object = @obj";
                    myCommand = new MySqlCommand(query2, myconnect);
                    myCommand.Parameters.AddWithValue("@date", shift);
                    myCommand.Parameters.AddWithValue("@obj", obj);
                    dataReader = myCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        shiftID = int.Parse(Convert.ToString(dataReader["shift_id"]));
                    }
                }
                catch
                {
                    Console.WriteLine("Не сработал даталист");
                }
                finally { dataReader.Close(); }

                string query3 = "INSERT INTO measure (measure_id, measure_shift, measure_Nk, measure_p0, measure_t0, measure_p_daer, " +
                    "measure_pvd7_out, measure_pvd6_out,measure_pvd5_out,measure_pnd4_out,measure_pnd3_out, measure_pnd2_out, measure_pnd1_out, measure_P, measure_t20, measure_t2)" +
                    " VALUES (NULL, @shift, @Nk, @p0, @t0, @daer, @pvd7, @pvd6, @pvd5, @pnd4, @pnd3, @pnd2, @pnd1, @P, @t20, @t2)";
                myCommand = new MySqlCommand(query3, myconnect);
                myCommand.Parameters.AddWithValue("@shift", shiftID);
                myCommand.Parameters.AddWithValue("@Nk", Nk);
                myCommand.Parameters.AddWithValue("@p0", p0);
                myCommand.Parameters.AddWithValue("@t0", t0);
                myCommand.Parameters.AddWithValue("@daer", daer);
                myCommand.Parameters.AddWithValue("@pvd7", pvd7);
                myCommand.Parameters.AddWithValue("@pvd6", pvd6);
                myCommand.Parameters.AddWithValue("@pvd5", pvd5);
                myCommand.Parameters.AddWithValue("@pnd4", pnd4);
                myCommand.Parameters.AddWithValue("@pnd3", pnd3);
                myCommand.Parameters.AddWithValue("@pnd2", pnd2);
                myCommand.Parameters.AddWithValue("@pnd1", pnd1);
                myCommand.Parameters.AddWithValue("@P", P);
                myCommand.Parameters.AddWithValue("@t20", t20);
                myCommand.Parameters.AddWithValue("@t2", t2);
                myCommand.ExecuteNonQuery();

            }
        }

        public static MySqlConnection GetConnection() => myconnect;
    }
}
