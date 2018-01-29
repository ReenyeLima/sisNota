﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace nota
{
    public partial class ServiceGinfesImplService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        private System.Threading.SendOrPostCallback CancelarNfseOperationCompleted;

        private System.Threading.SendOrPostCallback CancelarNfseV3OperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarLoteRpsOperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarLoteRpsV3OperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarNfseOperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarNfsePorRpsOperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarNfsePorRpsV3OperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarNfseV3OperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarSituacaoLoteRpsOperationCompleted;

        private System.Threading.SendOrPostCallback ConsultarSituacaoLoteRpsV3OperationCompleted;

        private System.Threading.SendOrPostCallback RecepcionarLoteRpsOperationCompleted;

        private System.Threading.SendOrPostCallback RecepcionarLoteRpsV3OperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public ServiceGinfesImplService()
        {
            this.Url = global::nota.Properties.Settings.Default.nota_servico1_ServiceGinfesImplService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/> -- observaçoes
        public event CancelarNfseCompletedEventHandler CancelarNfseCompleted;

        /// <remarks/>
        public event CancelarNfseV3CompletedEventHandler CancelarNfseV3Completed;

        /// <remarks/>
        public event ConsultarLoteRpsCompletedEventHandler ConsultarLoteRpsCompleted;

        /// <remarks/>
        public event ConsultarLoteRpsV3CompletedEventHandler ConsultarLoteRpsV3Completed;

        /// <remarks/>
        public event ConsultarNfseCompletedEventHandler ConsultarNfseCompleted;

        /// <remarks/>
        public event ConsultarNfsePorRpsCompletedEventHandler ConsultarNfsePorRpsCompleted;

        /// <remarks/>
        public event ConsultarNfsePorRpsV3CompletedEventHandler ConsultarNfsePorRpsV3Completed;

        /// <remarks/>
        public event ConsultarNfseV3CompletedEventHandler ConsultarNfseV3Completed;

        /// <remarks/>
        public event ConsultarSituacaoLoteRpsCompletedEventHandler ConsultarSituacaoLoteRpsCompleted;

        /// <remarks/>
        public event ConsultarSituacaoLoteRpsV3CompletedEventHandler ConsultarSituacaoLoteRpsV3Completed;

        /// <remarks/>
        public event RecepcionarLoteRpsCompletedEventHandler RecepcionarLoteRpsCompleted;

        /// <remarks/>
        public event RecepcionarLoteRpsV3CompletedEventHandler RecepcionarLoteRpsV3Completed;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string CancelarNfse(string arg0)
        {
            object[] results = this.Invoke("CancelarNfse", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void CancelarNfseAsync(string arg0)
        {
            this.CancelarNfseAsync(arg0, null);
        }

        /// <remarks/>
        public void CancelarNfseAsync(string arg0, object userState)
        {
            if ((this.CancelarNfseOperationCompleted == null))
            {
                this.CancelarNfseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCancelarNfseOperationCompleted);
            }
            this.InvokeAsync("CancelarNfse", new object[] {
                        arg0}, this.CancelarNfseOperationCompleted, userState);
        }

        private void OnCancelarNfseOperationCompleted(object arg)
        {
            if ((this.CancelarNfseCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CancelarNfseCompleted(this, new CancelarNfseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string CancelarNfseV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("CancelarNfseV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void CancelarNfseV3Async(string arg0, string arg1)
        {
            this.CancelarNfseV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void CancelarNfseV3Async(string arg0, string arg1, object userState)
        {
            if ((this.CancelarNfseV3OperationCompleted == null))
            {
                this.CancelarNfseV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnCancelarNfseV3OperationCompleted);
            }
            this.InvokeAsync("CancelarNfseV3", new object[] {
                        arg0,
                        arg1}, this.CancelarNfseV3OperationCompleted, userState);
        }

        private void OnCancelarNfseV3OperationCompleted(object arg)
        {
            if ((this.CancelarNfseV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CancelarNfseV3Completed(this, new CancelarNfseV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarLoteRps(string arg0)
        {
            object[] results = this.Invoke("ConsultarLoteRps", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarLoteRpsAsync(string arg0)
        {
            this.ConsultarLoteRpsAsync(arg0, null);
        }

        /// <remarks/>
        public void ConsultarLoteRpsAsync(string arg0, object userState)
        {
            if ((this.ConsultarLoteRpsOperationCompleted == null))
            {
                this.ConsultarLoteRpsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarLoteRpsOperationCompleted);
            }
            this.InvokeAsync("ConsultarLoteRps", new object[] {
                        arg0}, this.ConsultarLoteRpsOperationCompleted, userState);
        }

        private void OnConsultarLoteRpsOperationCompleted(object arg)
        {
            if ((this.ConsultarLoteRpsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarLoteRpsCompleted(this, new ConsultarLoteRpsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarLoteRpsV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("ConsultarLoteRpsV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarLoteRpsV3Async(string arg0, string arg1)
        {
            this.ConsultarLoteRpsV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void ConsultarLoteRpsV3Async(string arg0, string arg1, object userState)
        {
            if ((this.ConsultarLoteRpsV3OperationCompleted == null))
            {
                this.ConsultarLoteRpsV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarLoteRpsV3OperationCompleted);
            }
            this.InvokeAsync("ConsultarLoteRpsV3", new object[] {
                        arg0,
                        arg1}, this.ConsultarLoteRpsV3OperationCompleted, userState);
        }

        private void OnConsultarLoteRpsV3OperationCompleted(object arg)
        {
            if ((this.ConsultarLoteRpsV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarLoteRpsV3Completed(this, new ConsultarLoteRpsV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarNfse(string arg0)
        {
            object[] results = this.Invoke("ConsultarNfse", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarNfseAsync(string arg0)
        {
            this.ConsultarNfseAsync(arg0, null);
        }

        /// <remarks/>
        public void ConsultarNfseAsync(string arg0, object userState)
        {
            if ((this.ConsultarNfseOperationCompleted == null))
            {
                this.ConsultarNfseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarNfseOperationCompleted);
            }
            this.InvokeAsync("ConsultarNfse", new object[] {
                        arg0}, this.ConsultarNfseOperationCompleted, userState);
        }

        private void OnConsultarNfseOperationCompleted(object arg)
        {
            if ((this.ConsultarNfseCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarNfseCompleted(this, new ConsultarNfseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarNfsePorRps(string arg0)
        {
            object[] results = this.Invoke("ConsultarNfsePorRps", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarNfsePorRpsAsync(string arg0)
        {
            this.ConsultarNfsePorRpsAsync(arg0, null);
        }

        /// <remarks/>
        public void ConsultarNfsePorRpsAsync(string arg0, object userState)
        {
            if ((this.ConsultarNfsePorRpsOperationCompleted == null))
            {
                this.ConsultarNfsePorRpsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarNfsePorRpsOperationCompleted);
            }
            this.InvokeAsync("ConsultarNfsePorRps", new object[] {
                        arg0}, this.ConsultarNfsePorRpsOperationCompleted, userState);
        }

        private void OnConsultarNfsePorRpsOperationCompleted(object arg)
        {
            if ((this.ConsultarNfsePorRpsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarNfsePorRpsCompleted(this, new ConsultarNfsePorRpsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarNfsePorRpsV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("ConsultarNfsePorRpsV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarNfsePorRpsV3Async(string arg0, string arg1)
        {
            this.ConsultarNfsePorRpsV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void ConsultarNfsePorRpsV3Async(string arg0, string arg1, object userState)
        {
            if ((this.ConsultarNfsePorRpsV3OperationCompleted == null))
            {
                this.ConsultarNfsePorRpsV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarNfsePorRpsV3OperationCompleted);
            }
            this.InvokeAsync("ConsultarNfsePorRpsV3", new object[] {
                        arg0,
                        arg1}, this.ConsultarNfsePorRpsV3OperationCompleted, userState);
        }

        private void OnConsultarNfsePorRpsV3OperationCompleted(object arg)
        {
            if ((this.ConsultarNfsePorRpsV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarNfsePorRpsV3Completed(this, new ConsultarNfsePorRpsV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarNfseV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("ConsultarNfseV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarNfseV3Async(string arg0, string arg1)
        {
            this.ConsultarNfseV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void ConsultarNfseV3Async(string arg0, string arg1, object userState)
        {
            if ((this.ConsultarNfseV3OperationCompleted == null))
            {
                this.ConsultarNfseV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarNfseV3OperationCompleted);
            }
            this.InvokeAsync("ConsultarNfseV3", new object[] {
                        arg0,
                        arg1}, this.ConsultarNfseV3OperationCompleted, userState);
        }

        private void OnConsultarNfseV3OperationCompleted(object arg)
        {
            if ((this.ConsultarNfseV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarNfseV3Completed(this, new ConsultarNfseV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarSituacaoLoteRps(string arg0)
        {
            object[] results = this.Invoke("ConsultarSituacaoLoteRps", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarSituacaoLoteRpsAsync(string arg0)
        {
            this.ConsultarSituacaoLoteRpsAsync(arg0, null);
        }

        /// <remarks/>
        public void ConsultarSituacaoLoteRpsAsync(string arg0, object userState)
        {
            if ((this.ConsultarSituacaoLoteRpsOperationCompleted == null))
            {
                this.ConsultarSituacaoLoteRpsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarSituacaoLoteRpsOperationCompleted);
            }
            this.InvokeAsync("ConsultarSituacaoLoteRps", new object[] {
                        arg0}, this.ConsultarSituacaoLoteRpsOperationCompleted, userState);
        }

        private void OnConsultarSituacaoLoteRpsOperationCompleted(object arg)
        {
            if ((this.ConsultarSituacaoLoteRpsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarSituacaoLoteRpsCompleted(this, new ConsultarSituacaoLoteRpsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string ConsultarSituacaoLoteRpsV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("ConsultarSituacaoLoteRpsV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void ConsultarSituacaoLoteRpsV3Async(string arg0, string arg1)
        {
            this.ConsultarSituacaoLoteRpsV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void ConsultarSituacaoLoteRpsV3Async(string arg0, string arg1, object userState)
        {
            if ((this.ConsultarSituacaoLoteRpsV3OperationCompleted == null))
            {
                this.ConsultarSituacaoLoteRpsV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarSituacaoLoteRpsV3OperationCompleted);
            }
            this.InvokeAsync("ConsultarSituacaoLoteRpsV3", new object[] {
                        arg0,
                        arg1}, this.ConsultarSituacaoLoteRpsV3OperationCompleted, userState);
        }

        private void OnConsultarSituacaoLoteRpsV3OperationCompleted(object arg)
        {
            if ((this.ConsultarSituacaoLoteRpsV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarSituacaoLoteRpsV3Completed(this, new ConsultarSituacaoLoteRpsV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string RecepcionarLoteRps(string arg0)
        {
            object[] results = this.Invoke("RecepcionarLoteRps", new object[] {
                        arg0});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void RecepcionarLoteRpsAsync(string arg0)
        {
            this.RecepcionarLoteRpsAsync(arg0, null);
        }

        /// <remarks/>
        public void RecepcionarLoteRpsAsync(string arg0, object userState)
        {
            if ((this.RecepcionarLoteRpsOperationCompleted == null))
            {
                this.RecepcionarLoteRpsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecepcionarLoteRpsOperationCompleted);
            }
            this.InvokeAsync("RecepcionarLoteRps", new object[] {
                        arg0}, this.RecepcionarLoteRpsOperationCompleted, userState);
        }

        private void OnRecepcionarLoteRpsOperationCompleted(object arg)
        {
            if ((this.RecepcionarLoteRpsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecepcionarLoteRpsCompleted(this, new RecepcionarLoteRpsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://homologacao.ginfes.com.br", ResponseNamespace = "http://homologacao.ginfes.com.br", Use = System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("return")]
        public string RecepcionarLoteRpsV3(string arg0, string arg1)
        {
            object[] results = this.Invoke("RecepcionarLoteRpsV3", new object[] {
                        arg0,
                        arg1});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void RecepcionarLoteRpsV3Async(string arg0, string arg1)
        {
            this.RecepcionarLoteRpsV3Async(arg0, arg1, null);
        }

        /// <remarks/>
        public void RecepcionarLoteRpsV3Async(string arg0, string arg1, object userState)
        {
            if ((this.RecepcionarLoteRpsV3OperationCompleted == null))
            {
                this.RecepcionarLoteRpsV3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnRecepcionarLoteRpsV3OperationCompleted);
            }
            this.InvokeAsync("RecepcionarLoteRpsV3", new object[] {
                        arg0,
                        arg1}, this.RecepcionarLoteRpsV3OperationCompleted, userState);
        }

        private void OnRecepcionarLoteRpsV3OperationCompleted(object arg)
        {
            if ((this.RecepcionarLoteRpsV3Completed != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RecepcionarLoteRpsV3Completed(this, new RecepcionarLoteRpsV3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void CancelarNfseCompletedEventHandler(object sender, CancelarNfseCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CancelarNfseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CancelarNfseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void CancelarNfseV3CompletedEventHandler(object sender, CancelarNfseV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CancelarNfseV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CancelarNfseV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarLoteRpsCompletedEventHandler(object sender, ConsultarLoteRpsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarLoteRpsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarLoteRpsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarLoteRpsV3CompletedEventHandler(object sender, ConsultarLoteRpsV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarLoteRpsV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarLoteRpsV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarNfseCompletedEventHandler(object sender, ConsultarNfseCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarNfseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarNfseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarNfsePorRpsCompletedEventHandler(object sender, ConsultarNfsePorRpsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarNfsePorRpsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarNfsePorRpsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarNfsePorRpsV3CompletedEventHandler(object sender, ConsultarNfsePorRpsV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarNfsePorRpsV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarNfsePorRpsV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarNfseV3CompletedEventHandler(object sender, ConsultarNfseV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarNfseV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarNfseV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarSituacaoLoteRpsCompletedEventHandler(object sender, ConsultarSituacaoLoteRpsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarSituacaoLoteRpsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarSituacaoLoteRpsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void ConsultarSituacaoLoteRpsV3CompletedEventHandler(object sender, ConsultarSituacaoLoteRpsV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarSituacaoLoteRpsV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ConsultarSituacaoLoteRpsV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void RecepcionarLoteRpsCompletedEventHandler(object sender, RecepcionarLoteRpsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecepcionarLoteRpsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal RecepcionarLoteRpsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    public delegate void RecepcionarLoteRpsV3CompletedEventHandler(object sender, RecepcionarLoteRpsV3CompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17379")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RecepcionarLoteRpsV3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal RecepcionarLoteRpsV3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

