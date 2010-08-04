﻿using System;
using System.Web;
using Composite.Extensions;
using Composite.WebClient.UiControlLib.Foundation;

namespace Composite.WebClient.UiControlLib
{
    /// <summary>
    /// Generates a tag like
    /// &lt;ui:postbackdialog id="uniqueID" callbackid="uniqueCallbackID" label="Hello" tooltip="Hello Master!" handle="Composite.Management.PageSelectorDialog" value="DEFAULT VALUE!" /&gt;
    /// and persists "value" attribute.
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public class PostBackDialog : BaseControl
    {
        protected const string DefaultSelectorTagName = "ui:postbackdialog";
        protected const string NullableSelectorTagName = "ui:nullpostbackdialog";

        public PostBackDialog(string emptyParameter) : base(DefaultSelectorTagName)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Persisting value from postback
            string formKey = this.Attributes["callbackid"];
            if (formKey.IsNullOrEmpty())
            {
                formKey = this.ClientID;
            }

            if (Page.IsPostBack 
                && Page.Request.Form["__EVENTTARGET"] == formKey
                || !Page.Request.Form[formKey].IsNullOrEmpty())
            {
                Value = HttpContext.Current.Server.UrlDecode ( Page.Request.Form[formKey]);
            }
        }

        /// <summary>
        /// Setting to true leads to inserting "ui:nullpostbackdatadialog" tag 
        /// </summary>
        public bool Nullable 
        {   
            get
            {
                return TagName == NullableSelectorTagName;
            } 
            set
            {
                TagName = value ? NullableSelectorTagName : DefaultSelectorTagName;
            }
        }

        public string Value { get; set; }

        protected override void RenderAttributes(System.Web.UI.HtmlTextWriter writer)
        {
        	// added for error balloons to fixitup good
        	Attributes["name"] = ClientID;
        
            if(Attributes["callbackid"].IsNullOrEmpty())
            {
                Attributes["callbackid"] = ClientID;
                Attributes["name"] = ClientID;
            }

            if(Value != null)
            {
                Attributes["value"] = HttpContext.Current.Server.UrlEncode(Value).Replace("+", "%20");
            }

            base.RenderAttributes(writer);
        }
    }
}
