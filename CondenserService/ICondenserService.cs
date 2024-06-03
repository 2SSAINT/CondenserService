using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CondenserService
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "ICondenserService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ICondenserService
    {
        [OperationContract]
        void DoWork();
       
        [OperationContract]
        void Calculate_k(int obj);
       
        [OperationContract]
        string[][] GetDataForClient(int obj, int days);
        [OperationContract]
        void InsertMesIntoDB(string[] intoDB);

    }
}
