﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CondenserClient.CondenserServive {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CondenserServive.ICondenserService")]
    public interface ICondenserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/DoWork", ReplyAction="http://tempuri.org/ICondenserService/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/DoWork", ReplyAction="http://tempuri.org/ICondenserService/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/Calculate_k", ReplyAction="http://tempuri.org/ICondenserService/Calculate_kResponse")]
        void Calculate_k(int obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/Calculate_k", ReplyAction="http://tempuri.org/ICondenserService/Calculate_kResponse")]
        System.Threading.Tasks.Task Calculate_kAsync(int obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/GetDataForClient", ReplyAction="http://tempuri.org/ICondenserService/GetDataForClientResponse")]
        string[][] GetDataForClient(int obj, int days);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/GetDataForClient", ReplyAction="http://tempuri.org/ICondenserService/GetDataForClientResponse")]
        System.Threading.Tasks.Task<string[][]> GetDataForClientAsync(int obj, int days);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/InsertMesIntoDB", ReplyAction="http://tempuri.org/ICondenserService/InsertMesIntoDBResponse")]
        void InsertMesIntoDB(string[] intoDB);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICondenserService/InsertMesIntoDB", ReplyAction="http://tempuri.org/ICondenserService/InsertMesIntoDBResponse")]
        System.Threading.Tasks.Task InsertMesIntoDBAsync(string[] intoDB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICondenserServiceChannel : CondenserClient.CondenserServive.ICondenserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CondenserServiceClient : System.ServiceModel.ClientBase<CondenserClient.CondenserServive.ICondenserService>, CondenserClient.CondenserServive.ICondenserService {
        
        public CondenserServiceClient() {
        }
        
        public CondenserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CondenserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CondenserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CondenserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public void Calculate_k(int obj) {
            base.Channel.Calculate_k(obj);
        }
        
        public System.Threading.Tasks.Task Calculate_kAsync(int obj) {
            return base.Channel.Calculate_kAsync(obj);
        }
        
        public string[][] GetDataForClient(int obj, int days) {
            return base.Channel.GetDataForClient(obj, days);
        }
        
        public System.Threading.Tasks.Task<string[][]> GetDataForClientAsync(int obj, int days) {
            return base.Channel.GetDataForClientAsync(obj, days);
        }
        
        public void InsertMesIntoDB(string[] intoDB) {
            base.Channel.InsertMesIntoDB(intoDB);
        }
        
        public System.Threading.Tasks.Task InsertMesIntoDBAsync(string[] intoDB) {
            return base.Channel.InsertMesIntoDBAsync(intoDB);
        }
    }
}
