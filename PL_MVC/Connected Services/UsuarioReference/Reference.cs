﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PL_MVC.UsuarioReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/SL_WCF")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Usuario))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Direccion))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Colonia))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Municipio))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Estado))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ML.Rol))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(object[]))]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool CorrectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object ObjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object[] ObjectsField;
        
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
        public bool Correct {
            get {
                return this.CorrectField;
            }
            set {
                if ((this.CorrectField.Equals(value) != true)) {
                    this.CorrectField = value;
                    this.RaisePropertyChanged("Correct");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Object {
            get {
                return this.ObjectField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectField, value) != true)) {
                    this.ObjectField = value;
                    this.RaisePropertyChanged("Object");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object[] Objects {
            get {
                return this.ObjectsField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectsField, value) != true)) {
                    this.ObjectsField = value;
                    this.RaisePropertyChanged("Objects");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UsuarioReference.ICrud")]
    public interface ICrud {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/DoWork", ReplyAction="http://tempuri.org/ICrud/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/DoWork", ReplyAction="http://tempuri.org/ICrud/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Delete", ReplyAction="http://tempuri.org/ICrud/DeleteResponse")]
        PL_MVC.UsuarioReference.Result Delete(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Delete", ReplyAction="http://tempuri.org/ICrud/DeleteResponse")]
        System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> DeleteAsync(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Add", ReplyAction="http://tempuri.org/ICrud/AddResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Municipio))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Estado))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Rol))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PL_MVC.UsuarioReference.Result))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Direccion))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Colonia))]
        PL_MVC.UsuarioReference.Result Add(ML.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Add", ReplyAction="http://tempuri.org/ICrud/AddResponse")]
        System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> AddAsync(ML.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Update", ReplyAction="http://tempuri.org/ICrud/UpdateResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Municipio))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Estado))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Rol))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PL_MVC.UsuarioReference.Result))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Direccion))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Colonia))]
        PL_MVC.UsuarioReference.Result Update(ML.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/Update", ReplyAction="http://tempuri.org/ICrud/UpdateResponse")]
        System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> UpdateAsync(ML.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/GetById", ReplyAction="http://tempuri.org/ICrud/GetByIdResponse")]
        PL_MVC.UsuarioReference.Result GetById(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/GetById", ReplyAction="http://tempuri.org/ICrud/GetByIdResponse")]
        System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> GetByIdAsync(int idUsuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/GetAll", ReplyAction="http://tempuri.org/ICrud/GetAllResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Municipio))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Estado))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Rol))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PL_MVC.UsuarioReference.Result))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Direccion))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Colonia))]
        PL_MVC.UsuarioReference.Result GetAll(ML.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICrud/GetAll", ReplyAction="http://tempuri.org/ICrud/GetAllResponse")]
        System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> GetAllAsync(ML.Usuario usuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICrudChannel : PL_MVC.UsuarioReference.ICrud, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CrudClient : System.ServiceModel.ClientBase<PL_MVC.UsuarioReference.ICrud>, PL_MVC.UsuarioReference.ICrud {
        
        public CrudClient() {
        }
        
        public CrudClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CrudClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CrudClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CrudClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public PL_MVC.UsuarioReference.Result Delete(int idUsuario) {
            return base.Channel.Delete(idUsuario);
        }
        
        public System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> DeleteAsync(int idUsuario) {
            return base.Channel.DeleteAsync(idUsuario);
        }
        
        public PL_MVC.UsuarioReference.Result Add(ML.Usuario usuario) {
            return base.Channel.Add(usuario);
        }
        
        public System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> AddAsync(ML.Usuario usuario) {
            return base.Channel.AddAsync(usuario);
        }
        
        public PL_MVC.UsuarioReference.Result Update(ML.Usuario usuario) {
            return base.Channel.Update(usuario);
        }
        
        public System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> UpdateAsync(ML.Usuario usuario) {
            return base.Channel.UpdateAsync(usuario);
        }
        
        public PL_MVC.UsuarioReference.Result GetById(int idUsuario) {
            return base.Channel.GetById(idUsuario);
        }
        
        public System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> GetByIdAsync(int idUsuario) {
            return base.Channel.GetByIdAsync(idUsuario);
        }
        
        public PL_MVC.UsuarioReference.Result GetAll(ML.Usuario usuario) {
            return base.Channel.GetAll(usuario);
        }
        
        public System.Threading.Tasks.Task<PL_MVC.UsuarioReference.Result> GetAllAsync(ML.Usuario usuario) {
            return base.Channel.GetAllAsync(usuario);
        }
    }
}
