using Mediachase.BusinessFoundation;
using Mediachase.BusinessFoundation.XmlObjectModel;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.XPath;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Core.Controls
{
    public class MetaToolbar : UserControl
  {
    private const string defaultClientHandler = "defaultToolbarOnClick";
    private bool _bindInPreRender;
    protected JsToolbar MainToolbar;

    public string ClassName
    {
      get
      {
        string str = string.Empty;
        if (this.ViewState["ClassName"] != null)
          str = this.ViewState["ClassName"].ToString();
        return str;
      }
      set
      {
        this.ViewState["ClassName"] = (object) value;
      }
    }

    public string ViewName
    {
      get
      {
        string str = string.Empty;
        if (this.ViewState["ViewName"] != null)
          str = this.ViewState["ViewName"].ToString();
        return str;
      }
      set
      {
        this.ViewState["ViewName"] = (object) value;
      }
    }

    public string PlaceName
    {
      get
      {
        string str = string.Empty;
        if (this.ViewState["PlaceName"] != null)
          str = this.ViewState["PlaceName"].ToString();
        return str;
      }
      set
      {
        this.ViewState["PlaceName"] = (object) value;
      }
    }

    public MetaToolbar.Mode ToolbarMode
    {
      get
      {
        MetaToolbar.Mode mode = MetaToolbar.Mode.MetaView;
        if (this.ViewState["ToolbarMode"] != null)
          mode = (MetaToolbar.Mode) Enum.Parse(typeof (MetaToolbar.Mode), this.ViewState["ToolbarMode"].ToString());
        return mode;
      }
      set
      {
        this.ViewState["ToolbarMode"] = (object) value;
      }
    }

    public string CssClassGeneral
    {
      get
      {
        if (this.ViewState["_CssClassGeneral"] == null)
          return string.Empty;
        return this.ViewState["_CssClassGeneral"].ToString();
      }
      set
      {
        this.ViewState["_CssClassGeneral"] = (object) value;
      }
    }

    public string BlankImageUrl
    {
      get
      {
        if (this.ViewState["_BlankImageUrl"] == null)
          return this.ResolveUrl("~/Apps/Shell/styles/Images/ext/default/s.gif");
        return this.ViewState["_BlankImageUrl"].ToString();
      }
      set
      {
        this.ViewState["_BlankImageUrl"] = (object) value;
      }
    }

    public string GridId
    {
      get
      {
        if (this.ViewState["__GridId"] != null)
          return this.ViewState["__GridId"].ToString();
        return string.Empty;
      }
      set
      {
        this.ViewState["__GridId"] = (object) value;
      }
    }

    public string GridClientId
    {
      get
      {
        if (this.ViewState["__GridClientId"] != null)
          return this.ViewState["__GridClientId"].ToString();
        return string.Empty;
      }
      set
      {
        this.ViewState["__GridClientId"] = (object) value;
      }
    }

    public string ContainerId
    {
      get
      {
        if (this.ViewState["__ContainerId"] != null)
          return this.ViewState["__ContainerId"].ToString();
        return string.Empty;
      }
      set
      {
        this.ViewState["__ContainerId"] = (object) value;
      }
    }

    public bool BindInPreRender
    {
      get
      {
        return this._bindInPreRender;
      }
      set
      {
        this._bindInPreRender = value;
      }
    }

    public MetaToolbar() : base()
    {
    }

    public JsToolbar GetJsToolbar()
    {
      return this.MainToolbar;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.BindInPreRender)
        this.BindToolbar();
      this.MainToolbar.CssClassGeneral = this.CssClassGeneral;
      this.MainToolbar.BlankImageUrl = this.BlankImageUrl;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
      if (!this.BindInPreRender)
        return;
      this.BindToolbar();
    }

    public override void DataBind()
    {
      this.BindToolbar();
    }

    private void BindToolbar()
    {
      if (this.MainToolbar.McToolbarItems.Count > 0)
        this.MainToolbar.McToolbarItems.Clear();
      Toolbar toolbar = XmlModelHelper.GetToolbar(this.ViewName, this.PlaceName, this.ClassName);
      if (toolbar == null || toolbar.Items == null)
        return;
      this.BindFromObject(toolbar.Items, this.MainToolbar.McToolbarItems);
    }

    private void BindFromObject(ToolBarItem[] itemCollection, List<McToolbarItem> toolbarItemsCollection)
    {
      if (itemCollection == null)
        return;
      foreach (ToolBarItem toolBarItem in itemCollection)
      {
        McToolbarItem mcToolbarItem1 = new McToolbarItem();
        if (toolBarItem is Menu)
          mcToolbarItem1.ItemType = McToolbarItemType.Menu;
        else if (toolBarItem is Button)
          mcToolbarItem1.ItemType = McToolbarItemType.Button;
        else if (toolBarItem is SplitButton)
          mcToolbarItem1.ItemType = McToolbarItemType.SplitButton;
        else if (toolBarItem is Text)
          mcToolbarItem1.ItemType = McToolbarItemType.Text;
        else if (toolBarItem is Splitter)
          mcToolbarItem1.ItemType = McToolbarItemType.Splitter;
        McToolbarItemAlign toolbarItemAlign = McToolbarItemAlign.Left;
        McToolbarItemSplitter toolbarItemSplitter = McToolbarItemSplitter.None;
        if (!string.IsNullOrEmpty(toolBarItem.align))
        {
          try
          {
            toolbarItemAlign = (McToolbarItemAlign) Enum.Parse(typeof (McToolbarItemAlign), toolBarItem.align);
          }
          catch
          {
            throw;
          }
        }
        if (!string.IsNullOrEmpty(toolBarItem.itemSplitter))
        {
          try
          {
            toolbarItemSplitter = (McToolbarItemSplitter) Enum.Parse(typeof (McToolbarItemSplitter), toolBarItem.itemSplitter);
          }
          catch
          {
            throw;
          }
        }
        if (toolBarItem.imageUrl != string.Empty && (mcToolbarItem1.ItemType == McToolbarItemType.Button || mcToolbarItem1.ItemType == McToolbarItemType.SplitButton || mcToolbarItem1.ItemType == McToolbarItemType.Menu))
          mcToolbarItem1.CssClass += "x-btn-wrap x-btn x-btn-text-icon ";
        mcToolbarItem1.Id = toolBarItem.id;
        mcToolbarItem1.Text = this.IsTopMenuWelcomeText(toolBarItem.text) ? this.GetTopMenuWelcomeText(toolBarItem.text) : UtilHelper.GetResFileString(toolBarItem.text);
        mcToolbarItem1.ImageUrl = this.ResolveClientUrl(toolBarItem.imageUrl);
        mcToolbarItem1.CssClass += toolBarItem.cssClass;
        mcToolbarItem1.ItemAlign = toolbarItemAlign;
        mcToolbarItem1.Handler = toolBarItem.handler;
        mcToolbarItem1.Tooltip = UtilHelper.GetResFileString(toolBarItem.tooltip);
        bool isEnabled = true;
        if (!string.IsNullOrEmpty(toolBarItem.commandName))
        {
          CommandParameters cp = new CommandParameters(toolBarItem.commandName);
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          dictionary.Add("GridId", this.GridId);
          dictionary.Add("GridClientId", this.GridClientId);
          dictionary.Add("permissions", toolBarItem.permissions);
          if (!string.IsNullOrEmpty(this.ContainerId))
            dictionary.Add("ContainerId", this.ContainerId);
          cp.CommandArguments = dictionary;
          string str = CommandManager.GetCurrent(this.Page).AddCommand(this.ClassName, this.ViewName, this.PlaceName, cp, out isEnabled);
          dictionary.Add("CommandManagerScript", str);
          cp.CommandArguments = dictionary;
          mcToolbarItem1.Params = cp.ToString();
          mcToolbarItem1.Handler = "defaultToolbarOnClick";
        }
        if (mcToolbarItem1.ItemType == McToolbarItemType.Menu || mcToolbarItem1.ItemType == McToolbarItemType.SplitButton)
        {
          if (toolBarItem is Menu)
            this.BindFromObject((toolBarItem as Menu).Items, mcToolbarItem1.Items);
          else if (toolBarItem is SplitButton)
            this.BindFromObject((toolBarItem as SplitButton).Items, mcToolbarItem1.Items);
        }
        if (isEnabled && (toolbarItemSplitter == McToolbarItemSplitter.Both || toolbarItemSplitter == McToolbarItemSplitter.Left))
        {
          McToolbarItem mcToolbarItem2 = new McToolbarItem(McToolbarItemType.Splitter);
          mcToolbarItem2.ItemAlign = toolbarItemAlign;
          toolbarItemsCollection.Add(mcToolbarItem2);
        }
        if (isEnabled)
          toolbarItemsCollection.Add(mcToolbarItem1);
        if (isEnabled && (toolbarItemSplitter == McToolbarItemSplitter.Both || toolbarItemSplitter == McToolbarItemSplitter.Right))
        {
          McToolbarItem mcToolbarItem2 = new McToolbarItem(McToolbarItemType.Splitter);
          mcToolbarItem2.ItemAlign = toolbarItemAlign;
          toolbarItemsCollection.Add(mcToolbarItem2);
        }
      }
    }

    private string GetTopMenuWelcomeText(string resourceString)
    {
      string fullName = CustomerContext.Current.CurrentContact.FullName;
      return string.Format("{0}, {1}", (object) UtilHelper.GetResFileString(resourceString), string.IsNullOrEmpty(fullName) ? (object) SecurityContext.Current.CurrentUserName : (object) fullName);
    }

    private bool IsTopMenuWelcomeText(string resourceString)
    {
      if (this.ViewName == "TopMenu")
        return resourceString == "{SharedStrings:Welcome}";
      return false;
    }

    private void GetToolbarItemsFromXml(XPathNavigator node, List<McToolbarItem> itemsCollection)
    {
      foreach (XPathNavigator node1 in node.SelectChildren(string.Empty, string.Empty))
      {
        McToolbarItem mcToolbarItem1 = new McToolbarItem();
        switch (node1.Name)
        {
          case "Text":
            mcToolbarItem1.ItemType = McToolbarItemType.Text;
            break;
          case "Splitter":
            mcToolbarItem1.ItemType = McToolbarItemType.Splitter;
            break;
          case "Button":
            mcToolbarItem1.ItemType = McToolbarItemType.Button;
            break;
          case "Menu":
            mcToolbarItem1.ItemType = McToolbarItemType.Menu;
            break;
          case "SplitButton":
            mcToolbarItem1.ItemType = McToolbarItemType.SplitButton;
            break;
          default:
            throw new NotSupportedException(string.Format("Unknown nodeType: {0}", (object) node.Name));
        }
        string attribute1 = node1.GetAttribute("id", string.Empty);
        string attribute2 = node1.GetAttribute("text", string.Empty);
        string attribute3 = node1.GetAttribute("imageUrl", string.Empty);
        string attribute4 = node1.GetAttribute("cssClass", string.Empty);
        string attribute5 = node1.GetAttribute("align", string.Empty);
        string attribute6 = node1.GetAttribute("handler", string.Empty);
        string attribute7 = node1.GetAttribute("commandName", string.Empty);
        string attribute8 = node1.GetAttribute("itemSplitter", string.Empty);
        string attribute9 = node1.GetAttribute("tooltip", string.Empty);
        string attribute10 = node1.GetAttribute("permissions", string.Empty);
        McToolbarItemAlign toolbarItemAlign = McToolbarItemAlign.Left;
        McToolbarItemSplitter toolbarItemSplitter = McToolbarItemSplitter.None;
        if (attribute5 != string.Empty)
        {
          try
          {
            toolbarItemAlign = (McToolbarItemAlign) Enum.Parse(typeof (McToolbarItemAlign), attribute5);
          }
          catch
          {
            throw;
          }
        }
        if (attribute8 != string.Empty)
        {
          try
          {
            toolbarItemSplitter = (McToolbarItemSplitter) Enum.Parse(typeof (McToolbarItemSplitter), attribute8);
          }
          catch
          {
            throw;
          }
        }
        mcToolbarItem1.Id = attribute1;
        mcToolbarItem1.Text = UtilHelper.GetResFileString(attribute2);
        if (attribute3 != string.Empty && (mcToolbarItem1.ItemType == McToolbarItemType.Button || mcToolbarItem1.ItemType == McToolbarItemType.SplitButton || mcToolbarItem1.ItemType == McToolbarItemType.Menu))
          mcToolbarItem1.CssClass += "x-btn-wrap x-btn x-btn-text-icon ";
        mcToolbarItem1.ImageUrl = this.ResolveClientUrl(attribute3);
        mcToolbarItem1.CssClass += attribute4;
        mcToolbarItem1.ItemAlign = toolbarItemAlign;
        mcToolbarItem1.Handler = attribute6;
        mcToolbarItem1.Tooltip = UtilHelper.GetResFileString(attribute9);
        CommandParameters cp = new CommandParameters(attribute7);
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("GridId", this.GridId);
        dictionary.Add("GridClientId", this.GridClientId);
        dictionary.Add("permissions", attribute10);
        if (!string.IsNullOrEmpty(this.ContainerId))
          dictionary.Add("ContainerId", this.ContainerId);
        cp.CommandArguments = dictionary;
        bool isEnabled = true;
        if (attribute7 != string.Empty)
        {
          string str = CommandManager.GetCurrent(this.Page).AddCommand(this.ClassName, this.ViewName, this.PlaceName, cp, out isEnabled);
          dictionary.Add("CommandManagerScript", str);
          cp.CommandArguments = dictionary;
          mcToolbarItem1.Params = cp.ToString();
          mcToolbarItem1.Handler = "defaultToolbarOnClick";
        }
        if (mcToolbarItem1.ItemType == McToolbarItemType.Menu || mcToolbarItem1.ItemType == McToolbarItemType.SplitButton)
        {
          if (node1.SelectChildren(string.Empty, string.Empty).Count > 0)
          {
            this.GetToolbarItemsFromXml(node1, mcToolbarItem1.Items);
            if (mcToolbarItem1.Items.Count == 0)
              continue;
          }
          else
            continue;
        }
        if (isEnabled && (toolbarItemSplitter == McToolbarItemSplitter.Both || toolbarItemSplitter == McToolbarItemSplitter.Left))
        {
          McToolbarItem mcToolbarItem2 = new McToolbarItem(McToolbarItemType.Splitter);
          mcToolbarItem2.ItemAlign = toolbarItemAlign;
          itemsCollection.Add(mcToolbarItem2);
        }
        if (isEnabled)
          itemsCollection.Add(mcToolbarItem1);
        if (isEnabled && (toolbarItemSplitter == McToolbarItemSplitter.Both || toolbarItemSplitter == McToolbarItemSplitter.Right))
        {
          McToolbarItem mcToolbarItem2 = new McToolbarItem(McToolbarItemType.Splitter);
          mcToolbarItem2.ItemAlign = toolbarItemAlign;
          itemsCollection.Add(mcToolbarItem2);
        }
      }
    }

    public enum Mode
    {
      MetaView = 1,
      ListViewUI = 2,
    }
  }
}