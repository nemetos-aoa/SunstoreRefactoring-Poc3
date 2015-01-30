using Mediachase.BusinessFoundation;
using Mediachase.Commerce.Customers.Profile;
using Mediachase.Commerce.Manager.Core.Controls;
using Mediachase.Commerce.Security;
using Mediachase.Web.Console;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Config;
using Mediachase.Web.Console.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Core.Controls
{
    public class EcfListViewControl : BaseUserControl
  {
    private string _AppId;
    private string _ViewId;
    private bool _showTopToolbar;
    private string _DataKey;
    protected McDock DockTop;
    protected UpdatePanel panelToolbar;
    protected HtmlTable topTable;
    protected MetaToolbar MetaToolbar1;
    protected UpdateProgress UpdateProgress1;
    protected UpdatePanel panelMainListView;
    protected EcfListView MainListView;
    protected GridViewHeaderExtender gvHeaderExtender;
    protected HtmlTable emptyTable;
    private static Func<string, string> CS__CachedAnonymousMethodDelegate1;

    public bool LayoutResizeEnable
    {
      get
      {
        return this.gvHeaderExtender.LayoutResizeEnable;
      }
      set
      {
        this.gvHeaderExtender.LayoutResizeEnable = value;
      }
    }

    public new string AppId
    {
      get
      {
        return this._AppId;
      }
      set
      {
        this._AppId = value;
      }
    }

    public new string ViewId
    {
      get
      {
        return this._ViewId;
      }
      set
      {
        this._ViewId = value;
      }
    }

    public bool ShowTopToolbar
    {
      get
      {
        return this._showTopToolbar;
      }
      set
      {
        this._showTopToolbar = value;
      }
    }

    public EcfListView CurrentListView
    {
      get
      {
        return this.MainListView;
      }
    }

    public DataPager CurrentPager
    {
      get
      {
        return this.MainListView.FindControl("mainListViewPager2") as DataPager;
      }
    }

    public HtmlTable InnerListViewTable
    {
      get
      {
        return this.MainListView.FindControl("lvTable") as HtmlTable;
      }
    }

    public HtmlTableRow InnerListViewTableHeader
    {
      get
      {
        return this.MainListView.FindControl("headerTRow") as HtmlTableRow;
      }
    }

    public UpdatePanel MainUpdatePanel
    {
      get
      {
        return this.panelMainListView;
      }
    }

    public string DataKey
    {
      get
      {
        return this._DataKey;
      }
      set
      {
        this._DataKey = value;
      }
    }

    public string DataSourceID
    {
      get
      {
        return this.MainListView.DataSourceID;
      }
      set
      {
        this.MainListView.DataSourceID = value;
      }
    }

    public object DataSource
    {
      get
      {
        return this.MainListView.DataSource;
      }
      set
      {
        this.MainListView.DataSource = value;
      }
    }

    public string DataMember
    {
      get
      {
        return this.MainListView.DataMember;
      }
      set
      {
        this.MainListView.DataMember = value;
      }
    }

    public int PageSize
    {
      get
      {
        return this.MainListView.CurrentPageSize;
      }
    }

    public EcfListViewControl() : base()
    {
      this._AppId = string.Empty;
      this._ViewId = string.Empty;      
    }

    public string GetListViewClientID()
    {
      if (this.InnerListViewTable == null)
        return string.Empty;
      return this.InnerListViewTable.ClientID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      AdminView view = ManagementContext.Current.FindView(this.AppId, this.ViewId, string.Empty);
      if (view.Attributes.Contains((object) "permissions"))
      {
        string[] strArray = view.Attributes[(object) "permissions"].ToString().Split(',');
        if (EcfListViewControl.CS__CachedAnonymousMethodDelegate1 == null)
        {
          // ISSUE: method pointer
            EcfListViewControl.CS__CachedAnonymousMethodDelegate1 = new Func<string, string>(Page_Load__0);
        }
        Func<string, string> selector = EcfListViewControl.CS__CachedAnonymousMethodDelegate1;
        if (!SecurityContext.Current.CheckCurentUserHaveAnyPermissions(Enumerable.Select<string, string>((IEnumerable<string>) strArray, selector)))
          throw new UnauthorizedAccessException("Current user does not have enough rights to access the requested operation.");
      }
      this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "InitializeEcfListViewConstants", string.Format("CSManagementClient.EcfListView_PrimaryKeySeparator = '{0}';", (object) "::"), true);
      HtmlTable htmlTable = this.FindControl("topTable") as HtmlTable;
      if (htmlTable != null)
      {
        if (this.ShowTopToolbar)
        {
          htmlTable.Visible = true;
          this.MetaToolbar1.ViewName = this.ViewId;
          this.MetaToolbar1.GridClientId = this.GetListViewClientID();
          this.MetaToolbar1.DataBind();
        }
        else
        {
          this.DockTop.DefaultSize = 0;
          this.DockTop.Visible = false;
          htmlTable.Visible = false;
        }
      }
      if (this.InnerListViewTable != null)
        this.gvHeaderExtender.TargetControlID = this.InnerListViewTable.UniqueID;
      this.gvHeaderExtender.ContextKey = UtilHelper.JsonSerialize<EcfListViewContextKey>(new EcfListViewContextKey(this.AppId, this.ViewId));
      this.gvHeaderExtender.ServicePath = this.ResolveUrl("~/Apps/Core/Controls/WebServices/EcfListViewExtenderService.asmx");
      DropDownList dropDownList = this.MainListView.FindControl("ddPaging") as DropDownList;
      if (dropDownList != null)
      {
        // ISSUE: method pointer
          dropDownList.SelectedIndexChanged +=dropDownList_SelectedIndexChanged;
        //dropDownList.SelectedIndexChanged += new EventHandler((object) this, __methodptr(ddPaging_SelectedIndexChanged));
      }
      if (this.IsPostBack || this.CurrentPager == null)
        return;
      this.CurrentPager.SetPageProperties(0, this.CurrentListView.CurrentPageSize, true);
    }

void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
{
 	//throw new NotImplementedException();
}

    protected void Page_PreRender(object sender, EventArgs e)
    {
      if (this.InnerListViewTable != null)
      {
        this.BindGridStyles();
        this.BindExtenderColumns();
      }
      else if (this.MainListView.Items.Count == 0)
        this.gvHeaderExtender.TargetControlID = this.FindControl("emptyTable").UniqueID;
      if (this.InnerListViewTable == null)
        return;
      this.InnerListViewTable.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
    }

    public override void DataBind()
    {
      this.MainListView.AppId = this.AppId;
      this.MainListView.ViewId = this.ViewId;
      base.DataBind();
    }

    public void ResetPageNumber()
    {
      this.ResetPageNumber(false);
    }

    private void ResetPageNumber(bool dataBind)
    {
      DataPager currentPager = this.CurrentPager;
      if (currentPager == null)
        return;
      currentPager.SetPageProperties(0, this.MainListView.CurrentPageSize, dataBind);
    }

    private void BindGridInfo()
    {
      if (string.IsNullOrEmpty(this.DataKey))
        return;
      this.MainListView.DataKeyNames = new string[1]
      {
        this.DataKey
      };
    }

    private void BindGridStyles()
    {
      if (this.InnerListViewTable == null)
        return;
      this.InnerListViewTable.Attributes["class"] = "ecf-Grid";
    }

    private void BindExtenderColumns()
    {
      List<GridViewColumnInfo> list = new List<GridViewColumnInfo>();
      AdminView adminView = this.MainListView.GetAdminView();
      if (adminView != null)
      {
        CMPageSettings.MakeGridSettingsKey(this.AppId, this.ViewId);
        string input = string.Empty;
        EcfListViewPreferences listViewPreferences = (EcfListViewPreferences) null;
        if (!string.IsNullOrEmpty(input))
          listViewPreferences = UtilHelper.JsonDeserialize<EcfListViewPreferences>(input);
        this.gvHeaderExtender.ColumnsInfo.Clear();
        foreach (ViewColumn viewColumn in (CollectionBase) adminView.Columns)
        {
          if (viewColumn.Visible)
          {
            GridViewColumnInfo gridViewColumnInfo = new GridViewColumnInfo();
            gridViewColumnInfo.Sortable = viewColumn.ColumnType != ColumnType.Action && viewColumn.AllowSorting;
            gridViewColumnInfo.Resizable = viewColumn.ColumnType != ColumnType.CheckBox && viewColumn.ColumnType != ColumnType.Action && viewColumn.Resizable;
            int result = 0;
            if (viewColumn.ColumnType == ColumnType.CheckBox)
              result = 24;
            else if (listViewPreferences != null && listViewPreferences.ColumnProperties[(object) viewColumn.ColumnIndex.ToString()] != null)
              result = int.Parse((string) listViewPreferences.ColumnProperties[(object) viewColumn.ColumnIndex.ToString()]);
            else if (!int.TryParse(viewColumn.Width, out result))
              result = 100;
            gridViewColumnInfo.Width = result;
            this.gvHeaderExtender.ColumnsInfo.Add(gridViewColumnInfo);
          }
        }
      }
      this.gvHeaderExtender.HeaderHeight = 24;
      this.gvHeaderExtender.StyleInfo = new GridStylesInfo(this.MainListView.HeaderCssClass, this.MainListView.GridCssClass, this.MainListView.FooterCssClass, this.MainListView.HeaderInnerCssClass, this.MainListView.GridInnerCssClass, this.MainListView.GridSelectedRowCssClass);
    }

    protected void ddPaging_SelectedIndexChanged(object sender, EventArgs e)
    {
      DropDownList dropDownList = (DropDownList) sender;
      this.MainListView.CurrentPageSize = Convert.ToInt32(dropDownList.SelectedValue) == -1 ? int.MaxValue : Convert.ToInt32(dropDownList.SelectedValue);
      EcfListView.SavePageSize(this.Page, this.ViewId, this.MainListView.CurrentPageSize);
      this.ResetPageNumber(true);
    }

    private static string Page_Load__0(string x)
    {
      return x.Trim();
    }
  }
}