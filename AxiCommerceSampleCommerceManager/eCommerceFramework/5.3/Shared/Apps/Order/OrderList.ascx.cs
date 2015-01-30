using Mediachase.BusinessFoundation;
using Mediachase.Commerce.Orders.DataSources;
using Mediachase.Commerce.Orders.Search;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Common;
using Mediachase.Web.Console.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Mediachase.Web.Console.Config;
using Mediachase.Commerce.Manager.Core.Controls;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Order
{
    public class OrdersList : OrderBaseUserControl
    {
        private int _StartRowIndex;
        protected OrderDataSource OrderListDataSource;
        protected EcfListViewControl MyListView;

        public string FilterType
        {
            get
            {
                return this.Request.QueryString["filter"];
            }
        }

        public string ClassType
        {
            get
            {
                return ManagementHelper.GetStringValue((object)this.Request.QueryString["class"], "PurchaseOrder");
            }
        }

        public string Status
        {
            get
            {
                return this.Request.QueryString["status"];
            }
        }

        public OrdersList()
            : base()
        {
        }

        protected int GetMaximumRows()
        {
            return EcfListView.GetSavedPageSize(this.Page, this.MyListView.ViewId, 20);
        }

        private string GetPageTitle()
        {
            string str = string.Empty;
            string classType = this.ClassType;
            if (string.Compare(classType, "PurchaseOrder", StringComparison.InvariantCultureIgnoreCase) == 0)
                str = string.IsNullOrEmpty(this.Status) ? UtilHelper.GetResFileString("{OrderStrings:Order_List}") : UtilHelper.GetResFileString("{OrderStrings:Order_By_Status}");
            else if (string.Compare(classType, "ShoppingCart", StringComparison.InvariantCultureIgnoreCase) == 0)
                str = UtilHelper.GetResFileString("{OrderStrings:Cart_List}");
            else if (string.Compare(classType, "PaymentPlan", StringComparison.InvariantCultureIgnoreCase) == 0)
                str = UtilHelper.GetResFileString("{OrderStrings:PaymentPlan_List}");
            return str;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "SetTitleScriptKey", string.Format("CSManagementClient.SetPageTitle('{0}');", (object)this.GetPageTitle()), true);
            if (this.IsPostBack && string.Compare(this.Request.Form["__EVENTTARGET"], CommandManager.GetCurrent(this.Page).ID, false) != 0)
                return;
            if (!this.IsPostBack)
                this.MyListView.CurrentListView.PrimaryKeyId = EcfListView.MakePrimaryKeyIdString("OrderGroupId", "CustomerId");
            this.InitDataSource(this._StartRowIndex, this.GetMaximumRows(), true, "");
            this.DataBind();
        }

        protected override void OnInit(EventArgs e)
        {
            // ISSUE: method pointer
            this.MyListView.CurrentListView.PagePropertiesChanged += CurrentListView_PagePropertiesChanged;
            // ISSUE: method pointer
            this.MyListView.CurrentListView.PagePropertiesChanging += CurrentListView_PagePropertiesChanging;
            // ISSUE: method pointer
            this.MyListView.CurrentListView.Sorting += CurrentListView_Sorting;
            // ISSUE: method pointer
            this.Page.LoadComplete += Page_LoadComplete;
            base.OnInit(e);
        }

        private void CurrentListView_Sorting(object sender, ListViewSortEventArgs e)
        {
            foreach (ViewColumn viewColumn in (CollectionBase)this.MyListView.CurrentListView.GetAdminView().Columns)
            {
                if (viewColumn.AllowSorting && string.Compare(viewColumn.GetSortExpression(), e.SortExpression, true) == 0)
                {
                    this._StartRowIndex = 0;
                    this.InitDataSource(this._StartRowIndex, this.GetMaximumRows(), true, e.SortExpression + " " + (e.SortDirection == SortDirection.Descending ? "DESC" : "ASC"));
                }
            }
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!this.IsPostBack || !ManagementHelper.GetBindGridFlag(this.MyListView.CurrentListView.ID))
                return;
            this._StartRowIndex = 0;
            this.InitDataSource(this._StartRowIndex, this.GetMaximumRows(), true, this.MyListView.CurrentListView.SortExpression);
            this.DataBind();
            this.MyListView.MainUpdatePanel.Update();
        }

        private void CurrentListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this._StartRowIndex = e.StartRowIndex;
        }

        private void CurrentListView_PagePropertiesChanged(object sender, EventArgs e)
        {
            this.InitDataSource(this._StartRowIndex, this.GetMaximumRows(), true, this.MyListView.CurrentListView.SortExpression);
        }

        private void InitDataSource(int startRowIndex, int recordsCount, bool returnTotalCount, string orderByClause)
        {
            DateTime now = DateTime.Now;
            DateTime dateTime1 = now;
            DateTime dateTime2 = now;
            bool flag;
            bool marketFlag = false;
            string market = string.Empty;
            if (this.FilterType == "thisweek")
            {
                dateTime1 = ManagementHelper.GetStartOfWeek(now.Date);
                dateTime2 = now;
                flag = true;
            }
            else if (this.FilterType == "thismonth")
            {
                dateTime1 = new DateTime(now.Year, now.Month, 1);
                dateTime2 = now;
                flag = true;
            }
            else if (this.FilterType == "today")
            {
                dateTime1 = now.Date;
                dateTime2 = now;
                flag = true;
            }
            else
            {
                if (this.FilterType == "sunstore")
                {
                    market = this.FilterType;
                    marketFlag = true;
                }
                else if (this.FilterType == "amavita")
                {
                    market = this.FilterType;
                    marketFlag = true;
                }
                else if (this.FilterType == "coop")
                {
                    market = this.FilterType;
                    marketFlag = true;
                }
                flag = false;
            }
            if (flag)
                this.OrderListDataSource.Parameters.SqlMetaWhereClause = string.Format("META.Modified between '{0}' and '{1}'", (object)dateTime1.ToUniversalTime().ToString("s"), (object)dateTime2.ToUniversalTime().ToString("s"));            
            if (!string.IsNullOrEmpty(this.Status))
                this.OrderListDataSource.Parameters.SqlWhereClause = string.Format("Status = '{0}'", (object)ManagementHelper.MakeSafeSearchFilter(this.Status));
            if (marketFlag && !string.IsNullOrEmpty(market))
            {
                this.OrderListDataSource.Parameters.JoinSourceTable = "[dbo].[OrderForm]";
                this.OrderListDataSource.Parameters.JoinSourceTableKey = "[OrderGroupId]";
                this.OrderListDataSource.Parameters.JoinSourceTable = "[dbo].[OrderGroup]";
                this.OrderListDataSource.Parameters.JoinSourceTableKey = "[OrderGroupId]";
                if (!string.IsNullOrEmpty(this.OrderListDataSource.Parameters.SqlWhereClause))
                {
                    this.OrderListDataSource.Parameters.SqlWhereClause += string.Format("AND OrderGroup.MarketId = '{0}'", market);
                }
                else
                {
                    this.OrderListDataSource.Parameters.SqlWhereClause = string.Format("OrderGroup.MarketId = '{0}'", market);
                }
            }

            if (string.IsNullOrEmpty(orderByClause))
                orderByClause = string.Format("OrderGroupId DESC");
            OrderSearchOptions orderSearchOptions = new OrderSearchOptions();
            this.OrderListDataSource.Options.Namespace = "Mediachase.Commerce.Orders";
            this.OrderListDataSource.Options.Classes.Add(this.ClassType);
            this.MyListView.DataMember = !(this.ClassType == "ShoppingCart") ? (!(this.ClassType == "PaymentPlan") ? OrderDataSource.OrderDataSourceView.PurchaseOrdersViewName : OrderDataSource.OrderDataSourceView.PaymentPlansViewName) : OrderDataSource.OrderDataSourceView.CartsViewName;
            this.OrderListDataSource.Options.RecordsToRetrieve = recordsCount;
            this.OrderListDataSource.Options.StartingRecord = startRowIndex;
            this.OrderListDataSource.Parameters.OrderByClause = orderByClause;
        }
    }
}