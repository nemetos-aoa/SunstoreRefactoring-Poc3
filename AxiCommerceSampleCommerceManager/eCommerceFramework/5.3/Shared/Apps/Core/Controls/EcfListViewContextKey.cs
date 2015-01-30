using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Core.Controls
{
    internal class EcfListViewContextKey
    {
        private string _appId;
        private string _viewId;

        public string AppId
        {
            get
            {
                return this._appId;
            }
            set
            {
                this._appId = value;
            }
        }

        public string ViewId
        {
            get
            {
                return this._viewId;
            }
            set
            {
                this._viewId = value;
            }
        }

        public EcfListViewContextKey()
        {
        }

        public EcfListViewContextKey(string appId, string viewId) // : base()
        {
            this.AppId = appId;
            this.ViewId = viewId;
        }
    }
}