using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.ServiceModel;

namespace CondenserHost
{
    internal class Program
    {
        private static AutoLoader.CondenserServiceClient _client; //ссылка на службу
        private static Timer _timer;
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(CondenserService.CondenserService)))
            {
                host.Open();
                Console.WriteLine("Хост стартовал!");

                // автоматический запуск метода рассчета индекса загрязненности. 
                _client = new AutoLoader.CondenserServiceClient();
                _timer = new Timer();
                _timer.Interval = 10000; // интервал раз в 20 секунд
             //   _timer.Elapsed += Timer_Elapsed;
                _timer.Start();

                // Запустить консольное приложение
                Console.WriteLine("Хост запущен. Нажмите любую клавишу для выхода...");
                Console.ReadKey();

                _timer.Stop();
                _client.Close();
                Console.ReadLine();
                //
            }
            Console.WriteLine("Ne");
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Вызвать метод WCF
            try
            {
                _client.Calculate_k(2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вызове метода WCF: {ex.Message}");
            }
        }
    }
}
