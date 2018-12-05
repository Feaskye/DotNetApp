﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MintCyclingService.SMSService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SMSRequest", Namespace="http://schemas.datacontract.org/2004/07/IngpalSMS.Message")]
    [System.SerializableAttribute()]
    public partial class SMSRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MsgSupplierField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToNumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageContent {
            get {
                return this.MessageContentField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageContentField, value) != true)) {
                    this.MessageContentField = value;
                    this.RaisePropertyChanged("MessageContent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MsgSupplier {
            get {
                return this.MsgSupplierField;
            }
            set {
                if ((object.ReferenceEquals(this.MsgSupplierField, value) != true)) {
                    this.MsgSupplierField = value;
                    this.RaisePropertyChanged("MsgSupplier");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToNumber {
            get {
                return this.ToNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ToNumberField, value) != true)) {
                    this.ToNumberField = value;
                    this.RaisePropertyChanged("ToNumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SMSResponse", Namespace="http://schemas.datacontract.org/2004/07/IngpalSMS.Message")]
    [System.SerializableAttribute()]
    public partial class SMSResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RetMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RetMessage {
            get {
                return this.RetMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.RetMessageField, value) != true)) {
                    this.RetMessageField = value;
                    this.RaisePropertyChanged("RetMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SMSService.ISMSService")]
    public interface ISMSService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/SendMessage", ReplyAction="http://tempuri.org/ISMSService/SendMessageResponse")]
        MintCyclingService.SMSService.SMSResponse SendMessage(MintCyclingService.SMSService.SMSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMSService/SendMessage", ReplyAction="http://tempuri.org/ISMSService/SendMessageResponse")]
        System.Threading.Tasks.Task<MintCyclingService.SMSService.SMSResponse> SendMessageAsync(MintCyclingService.SMSService.SMSRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISMSServiceChannel : MintCyclingService.SMSService.ISMSService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SMSServiceClient : System.ServiceModel.ClientBase<MintCyclingService.SMSService.ISMSService>, MintCyclingService.SMSService.ISMSService {
        
        public SMSServiceClient() {
        }
        
        public SMSServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SMSServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SMSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MintCyclingService.SMSService.SMSResponse SendMessage(MintCyclingService.SMSService.SMSRequest request) {
            return base.Channel.SendMessage(request);
        }
        
        public System.Threading.Tasks.Task<MintCyclingService.SMSService.SMSResponse> SendMessageAsync(MintCyclingService.SMSService.SMSRequest request) {
            return base.Channel.SendMessageAsync(request);
        }
    }
}
