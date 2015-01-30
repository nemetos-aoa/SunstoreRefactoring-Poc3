using Mediachase.BusinessFoundation;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Security;
using Mediachase.Web.Console;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Common;
using Mediachase.Web.Console.Config;
using Mediachase.Web.Console.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Customer.Tabs
{
   public class RoleOverviewEditTab : ProfileBaseUserControl, IAdminTabControl, IDynamicParamControl, IAdminContextControl
  {
    private const string _RoleIdString = "RoleId";
    private const string _PermissionsKey = "Permissions";
    private const string _ModuleNodePrefix = "module_";
    private const string _GroupNodePrefix = "group_";
    private const string _PermissionNodePrefix = "permission_";
    private const string _ResourceFileName = "SharedStrings:";
    private const string _CoreViewPermission = "core:mng:login";
    private const string _BaseTabViewPermission = "tabviewpermission";
    private IEnumerable<SecurityPermission> _Permissions;
    protected HiddenField SelectedPermissions;
    protected Label Label1;
    protected TextBox tbRoleName;
    protected RequiredFieldValidator rfvRoleName;
    protected CustomValidator RoleNameCustomValidator;
    protected RegularExpressionValidator regExpValidator;
    protected HtmlTableRow PermissionsTr;
    protected Label Label3;
    protected UpdatePanel TreeUpdatePanel;
    protected TreeView PermissionsTree;


    private static Func<SecurityRole, string> CS__CachedAnonymousMethodDelegate5;

    public string RoleId
    {
      get
      {
        return ManagementHelper.GetValueFromQueryString("RoleId", string.Empty);
      }
    }

    public RoleOverviewEditTab() :base()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.IsPostBack)
        this.BindForm();
      if (this.Page.ClientScript.IsClientScriptIncludeRegistered("TreeView"))
        return;
      this.Page.ClientScript.RegisterClientScriptInclude("TreeView", this.Page.ResolveClientUrl("~/Apps/Shell/Scripts/TreeView.js"));
    }

    private void BindForm()
    {
      this.tbRoleName.Text = this.RoleId;
      if (string.IsNullOrEmpty(this.RoleId))
        SecurityContext.Current.CheckPermissionForCurentUser("customer:roles:mng:create", true);
      else
        SecurityContext.Current.CheckPermissionForCurentUser("customer:roles:mng:edit", true);
      this.tbRoleName.ReadOnly = !string.IsNullOrEmpty(this.RoleId);
      if (!string.IsNullOrEmpty(this.RoleId) && this.RoleId.Equals(AppRoles.AdminRole))
        this.PermissionsTr.Visible = false;
      else
        this.BindPermissionsTree();
    }

    private void BindPermissionsTree()
    {
      this.PermissionsTree.Nodes.Clear();
      foreach (ModuleConfig moduleConfig in ManagementContext.Current.Configs)
      {
        if (moduleConfig.Acl != null && moduleConfig.Acl.Groups != null)
        {
          foreach (AclGroup aclGroup in (CollectionBase) moduleConfig.Acl.Groups)
          {
            TreeNode treeNode = new TreeNode();
            treeNode.Value = this.GetTreeNodeUniqueId("group_", aclGroup.ID);
            treeNode.Text = UtilHelper.GetResFileString("{SharedStrings:" + aclGroup.Name + "}");
            treeNode.ShowCheckBox = new bool?(false);
            treeNode.SelectAction = TreeNodeSelectAction.Expand;
            treeNode.ImageUrl = this.Page.ResolveUrl("~/Apps/Customer/images/security_folder.png");
            foreach (AclGroup group in (CollectionBase) aclGroup.Groups)
            {
              if (this.AddAclGroup(group, treeNode) == 0)
                treeNode.Select();
            }
            this.PermissionsTree.Nodes.Add(treeNode);
          }
        }
      }
    }

    private int AddAclGroup(AclGroup group, TreeNode treeNode)
    {
      int num1 = -1;
      TreeNode treeNode1 = new TreeNode();
      treeNode1.Value = this.GetTreeNodeUniqueId("group_", group.ID);
      treeNode1.Text = UtilHelper.GetResFileString(group.Name);
      treeNode1.ShowCheckBox = new bool?(true);
      treeNode1.ImageUrl = this.Page.ResolveUrl("~/Apps/Customer/images/security_folder.png");
      treeNode1.SelectAction = TreeNodeSelectAction.Expand;
      treeNode.ChildNodes.Add(treeNode1);
      foreach (AclGroup group1 in (CollectionBase) group.Groups)
      {
        int num2 = this.AddAclGroup(group1, treeNode1);
        if (num2 == 2 && num1 == 1)
          num1 = 0;
        else if (num2 == 2 && num1 == -1)
          num1 = 2;
        else if (num2 == 1 && num1 == -1)
          num1 = 1;
        else if (num2 == 1 && num1 == 2)
          num1 = 0;
        else if (num2 == 0)
          num1 = 0;
      }
      IEnumerator enumerator = group.Permissions.GetEnumerator();
      try
      {
        Func<SecurityPermission, bool> func = (Func<SecurityPermission, bool>) null;
        RoleOverviewEditTab.DisplayClass2 cDisplayClass2 = new RoleOverviewEditTab.DisplayClass2();
        while (enumerator.MoveNext())
        {
          cDisplayClass2.permission = (AclPermission) enumerator.Current;
          TreeNode child = new TreeNode();
          child.Value = this.GetTreeNodeUniqueId("permission_", cDisplayClass2.permission.ToString());
          child.Text = UtilHelper.GetResFileString(cDisplayClass2.permission.Name);
          child.SelectAction = TreeNodeSelectAction.None;
          child.ImageUrl = this.Page.ResolveUrl("~/Apps/Customer/images/security_key.png");
          if (this._Permissions != null)
          {
            IEnumerable<SecurityPermission> source = this._Permissions;
            if (func == null)
            {
              // ISSUE: method pointer
              func = new Func<SecurityPermission, bool>(cDisplayClass2.AddAclGroup);
            }
            Func<SecurityPermission, bool> predicate = func;
            if (Enumerable.Count<SecurityPermission>(source, predicate) > 0)
            {
              child.Checked = true;
              if (num1 == -1)
                num1 = 1;
            }
          }
          if (!child.Checked && num1 == 1)
            num1 = 0;
          if (!child.Checked && num1 == -1)
            num1 = 2;
          treeNode1.ChildNodes.Add(child);
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
      treeNode1.Checked = num1 == 1;
      if (num1 == 0)
      {
        treeNode1.Select();
        treeNode1.Expand();
      }
      else
        treeNode1.Collapse();
      return num1;
    }

    private bool HasCheckedChildNodes(TreeNode node)
    {
      if (node.ChildNodes.Count == 0)
        return false;
      foreach (TreeNode node1 in node.ChildNodes)
      {
        if (node1.Checked || this.HasCheckedChildNodes(node1))
          return true;
      }
      return false;
    }

    public void RoleNameCheck(object sender, ServerValidateEventArgs args)
    {
      IEnumerable<SecurityRole> allRegisteredRoles = SecurityContext.Current.GetAllRegisteredRoles();
      if (RoleOverviewEditTab.CS__CachedAnonymousMethodDelegate5 == null)
      {
        // ISSUE: method pointer
        RoleOverviewEditTab.CS__CachedAnonymousMethodDelegate5 = new Func<SecurityRole, string>(RoleNameCheck);
      }
      Func<SecurityRole, string> selector = RoleOverviewEditTab.CS__CachedAnonymousMethodDelegate5;
      IEnumerable<string> source = Enumerable.Select<SecurityRole, string>(allRegisteredRoles, selector);
      bool flag = false;
      if (!string.IsNullOrEmpty(args.Value))
        flag = Enumerable.Contains<string>(source, args.Value);
      if (string.IsNullOrEmpty(this.RoleId) && flag)
        args.IsValid = false;
      else
        args.IsValid = true;
    }

    public void LoadContext(IDictionary context)
    {
      this._Permissions = (IEnumerable<SecurityPermission>) context[(object) "Permissions"];
    }

    public void SaveChanges(IDictionary context)
    {
      Func<SecurityRole, bool> func = (Func<SecurityRole, bool>) null;
      List<SecurityPermission> permissions = new List<SecurityPermission>();
      foreach (TreeNode node in this.PermissionsTree.CheckedNodes)
      {
        if (node.Value.StartsWith("permission_"))
        {
          permissions.Add(new SecurityPermission(this.GetIdFromTreeNodeUniqueId(node.Value)));
          this.GetTabViewPermission(node, permissions);
        }
      }
      if (string.IsNullOrEmpty(this.RoleId) || SecurityContext.Current.SecurityManagerInstance.GetRoleByName(this.RoleId) == (SecurityRole) null)
      {
        SecurityContext.Current.SecurityManagerInstance.CreateRole(this.tbRoleName.Text.Trim(), (IEnumerable<SecurityPermission>) permissions);
      }
      else
      {
        if (this.RoleId.Equals(AppRoles.AdminRole))
          return;
        IEnumerable<SecurityRole> allRegisteredRoles = SecurityContext.Current.SecurityManagerInstance.GetAllRegisteredRoles();
        if (func == null)
        {
          // ISSUE: method pointer
          func = new Func<SecurityRole, bool>(SaveChanges);
        }
        Func<SecurityRole, bool> predicate = func;
        SecurityRole role = Enumerable.First<SecurityRole>(allRegisteredRoles, predicate);
        role.Permissions = (IEnumerable<SecurityPermission>) permissions;
        SecurityContext.Current.SecurityManagerInstance.UpdateRole(role);
      }
    }

    private string GetTreeNodeUniqueId(string perfix, string id)
    {
      return perfix + id;
    }

    private string GetIdFromTreeNodeUniqueId(string id)
    {
      if (string.IsNullOrEmpty(id))
        return id;
      string str = id;
      int num = id.IndexOf('_');
      if (num >= 0 && num < id.Length - 1)
        str = id.Substring(num + 1);
      else if (num == id.Length - 1)
        str = string.Empty;
      return str.ToLower();
    }

    private void GetTabViewPermission(TreeNode node, List<SecurityPermission> permissions)
    {
      TreeNode parent = node.Parent;
      string id = (string) null;
      for (; parent != null; parent = parent.Parent)
        id = parent.Value;
      SecurityPermission securityPermission = new SecurityPermission(this.GetIdFromTreeNodeUniqueId(id) + ":tabviewpermission");
      if (!permissions.Contains(securityPermission))
        permissions.Add(securityPermission);
      if (permissions.Contains(new SecurityPermission("core:mng:login")))
        return;
      permissions.Add(new SecurityPermission("core:mng:login"));
    }

    private static string RoleNameCheck(SecurityRole x)
    {
      return x.RoleName;
    }

    private bool SaveChanges(SecurityRole x)
    {
      return string.Compare(x.RoleName, this.RoleId, true) == 0;
    }

    private sealed class DisplayClass2
    {
      public AclPermission permission;

      public DisplayClass2() : base()
      {
      }

      public bool AddAclGroup(SecurityPermission x)
      {
        return string.Compare(x.PermissionName, this.permission.ToString(), true) == 0;
      }
    }
  }
}