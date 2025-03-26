﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Credentials", Namespace="http://schemas.datacontract.org/2004/07/VeCloud")]
    public partial class Credentials : object
    {
        
        private string IdKeyField;
        
        private string KundIdField;
        
        private string LkField;
        
        private string PasswordField;
        
        private int ProduktIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdKey
        {
            get
            {
                return this.IdKeyField;
            }
            set
            {
                this.IdKeyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KundId
        {
            get
            {
                return this.KundIdField;
            }
            set
            {
                this.KundIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Lk
        {
            get
            {
                return this.LkField;
            }
            set
            {
                this.LkField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password
        {
            get
            {
                return this.PasswordField;
            }
            set
            {
                this.PasswordField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProduktId
        {
            get
            {
                return this.ProduktIdField;
            }
            set
            {
                this.ProduktIdField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IVeCloudService")]
    public interface IVeCloudService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVeCloudService/GetRegnr", ReplyAction="http://tempuri.org/IVeCloudService/GetRegnrResponse")]
        System.Threading.Tasks.Task<string> GetRegnrAsync(ServiceReference.Credentials credentials, string regnr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVeCloudService/GetRandomRegnr", ReplyAction="http://tempuri.org/IVeCloudService/GetRandomRegnrResponse")]
        System.Threading.Tasks.Task<string[]> GetRandomRegnrAsync(ServiceReference.Credentials credentials, string lk);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVeCloudService/GetVeRegLog", ReplyAction="http://tempuri.org/IVeCloudService/GetVeRegLogResponse")]
        System.Threading.Tasks.Task<string> GetVeRegLogAsync(ServiceReference.Credentials credentials, string statusCode, System.DateTime fromDate, System.Nullable<System.DateTime> toDate, int page);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVeCloudService/Ping", ReplyAction="http://tempuri.org/IVeCloudService/PingResponse")]
        System.Threading.Tasks.Task<string> PingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface IVeCloudServiceChannel : ServiceReference.IVeCloudService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class VeCloudServiceClient : System.ServiceModel.ClientBase<ServiceReference.IVeCloudService>, ServiceReference.IVeCloudService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public VeCloudServiceClient() : 
                base(VeCloudServiceClient.GetDefaultBinding(), VeCloudServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.VeCloudService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public VeCloudServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(VeCloudServiceClient.GetBindingForEndpoint(endpointConfiguration), VeCloudServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public VeCloudServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(VeCloudServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public VeCloudServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(VeCloudServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public VeCloudServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> GetRegnrAsync(ServiceReference.Credentials credentials, string regnr)
        {
            return base.Channel.GetRegnrAsync(credentials, regnr);
        }
        
        public System.Threading.Tasks.Task<string[]> GetRandomRegnrAsync(ServiceReference.Credentials credentials, string lk)
        {
            return base.Channel.GetRandomRegnrAsync(credentials, lk);
        }
        
        public System.Threading.Tasks.Task<string> GetVeRegLogAsync(ServiceReference.Credentials credentials, string statusCode, System.DateTime fromDate, System.Nullable<System.DateTime> toDate, int page)
        {
            return base.Channel.GetVeRegLogAsync(credentials, statusCode, fromDate, toDate, page);
        }
        
        public System.Threading.Tasks.Task<string> PingAsync()
        {
            return base.Channel.PingAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.VeCloudService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.VeCloudService))
            {
                return new System.ServiceModel.EndpointAddress("https://ais-vecloud.azurewebsites.net/VeCloudService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return VeCloudServiceClient.GetBindingForEndpoint(EndpointConfiguration.VeCloudService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return VeCloudServiceClient.GetEndpointAddress(EndpointConfiguration.VeCloudService);
        }
        
        public enum EndpointConfiguration
        {
            
            VeCloudService,
        }
    }
}
