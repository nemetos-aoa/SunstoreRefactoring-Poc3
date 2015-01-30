using Mediachase.BusinessFoundation;
using Mediachase.BusinessFoundation.XmlObjectModel;
using Mediachase.Commerce.Core;
using Mediachase.Commerce.Customers;
using Mediachase.Commerce.Security;
using Mediachase.Web.Console.BaseClasses;
using Mediachase.Web.Console.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;

namespace Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Shell.Pages
{
    public class TreeSource : BasePage
    {
        public TreeSource()
            : base()
        {
            string a = string.Empty;
            //base();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request["mode"] == "full")
                this.BindFullTree();
            else if (this.Request.Form["node"] != "leftTemplate_tree_rootId")
                this.BindNode(this.Request.Form["node"]);
            else
                this.BindJsTree();
        }

        private void BindJsTree()
        {
            string str = string.Empty;
            if (this.Request["tab"] != null)
                str = this.Request["tab"];
            if (string.IsNullOrEmpty(str))
                return;
            List<JsonTreeNode> nodes = new List<JsonTreeNode>();
            Navigation navigation = XmlModelHelper.GetNavigation("LeftMenu", string.Empty);
            if (navigation == null || navigation.Tabs == null)
                return;
            foreach (Tab tab in navigation.Tabs.Tab)
            {
                if (!string.IsNullOrEmpty(str) && str == tab.id || string.IsNullOrEmpty(str))
                {
                    JsonTreeNode jsonTreeNode = new JsonTreeNode();
                    jsonTreeNode.id = tab.id;
                    jsonTreeNode.text = UtilHelper.GetResFileString(tab.text);
                    jsonTreeNode.cls = "nodeCls";
                    if (!string.IsNullOrEmpty(tab.imageUrl))
                        jsonTreeNode.icon = this.ResolveUrl(tab.imageUrl);
                    jsonTreeNode.children = new List<JsonTreeNode>();
                    jsonTreeNode.expanded = true;
                    int num = 0;
                    if (tab.Items != null)
                        num = this.BindRecursive(jsonTreeNode.children, tab.Items);
                    if (num == 0)
                    {
                        jsonTreeNode.leaf = true;
                        jsonTreeNode.children = (List<JsonTreeNode>)null;
                    }
                    nodes.Add(jsonTreeNode);
                }
            }
            this.WriteArray(nodes);
        }

        public static bool IsEnableCommand(string className, string viewName, string placeName, string commandName, Dictionary<string, string> parameters)
        {
            bool flag = true;
            XmlCommand commandByName = XmlCommand.GetCommandByName(className, viewName, placeName, commandName);
            if (commandByName == null || string.IsNullOrEmpty(commandByName.EnableHandler))
                return flag;
            object obj = AssemblyUtil.LoadObject(commandByName.EnableHandler);
            if (!(obj is ICommandEnableHandler))
                return flag;
            CommandParameters commandParameters = new CommandParameters(commandName);
            commandParameters.CommandArguments = parameters;
            if (!string.IsNullOrEmpty(commandByName.Params))
            {
                if (commandParameters.CommandArguments == null)
                    commandParameters.CommandArguments = new Dictionary<string, string>();
                commandParameters.CommandArguments.Add("_commandParamsKey", commandByName.Params);
            }
            return ((ICommandEnableHandler)obj).IsEnable((object)null, (object)commandParameters);
        }

        private int BindRecursive(List<JsonTreeNode> nodes, Link[] linkItem)
        {

            int num1 = 0;
            foreach (Link link in linkItem)
            {
                if (link.id.Equals("Order_PurchaseOrdersSunstore") || link.id.Equals("Order_PurchaseOrdersAmavita") || link.id.Equals("Order_PurchaseOrdersCoop") ||
                    link.id.Equals("ShippingReceivingSunstore") || link.id.Equals("ShippingReceivingAmavita") || link.id.Equals("ShippingReceivingCoop")
                    )
                {
                    if (CheckifUserHasRoleOnMarket(link.id))
                    {
                        string permissions = link.permissions;
                        JsonTreeNode jsonTreeNode = new JsonTreeNode();
                        string resFileString = UtilHelper.GetResFileString(link.text);
                        jsonTreeNode.text = resFileString;
                        jsonTreeNode.id = link.id;
                        jsonTreeNode.cls = "nodeCls";
                        jsonTreeNode.iconCls = "iconNodeCls";
                        if (!string.IsNullOrEmpty(link.iconUrl))
                            jsonTreeNode.icon = this.ResolveUrl(link.iconUrl);
                        if (!string.IsNullOrEmpty(link.iconCss))
                            jsonTreeNode.iconCls = link.iconCss;
                        if (string.IsNullOrEmpty(link.command))
                        {
                            if (!string.IsNullOrEmpty(permissions) && !SecurityContext.Current.CheckPermissionForCurentUser(permissions, false))
                                continue;
                        }
                        else
                        {
                            Dictionary<string, string> parameters = new Dictionary<string, string>();
                            parameters.Add("permissions", permissions);
                            if (TreeSource.IsEnableCommand("", "LeftMenu", "", link.command, parameters))
                            {
                                string str = CommandManager.GetCommandString(link.command, (Dictionary<string, string>)null).Replace("\"", "&quot;");
                                jsonTreeNode.href = string.Format("javascript:{0}", (object)str);
                                XmlCommand.GetCommandByName("", "LeftMenu", "", link.command);
                            }
                            else
                                continue;
                        }
                        if (link.expanded)
                            jsonTreeNode.expanded = true;
                        if (!string.IsNullOrEmpty(link.type))
                            jsonTreeNode.type = link.type;
                        bool flag = false;
                        string treeLoader = link.treeLoader;
                        string treeLoaderPath = link.treeLoaderPath;
                        if (!string.IsNullOrEmpty(treeLoader))
                            flag = true;
                        if (!string.IsNullOrEmpty(treeLoaderPath))
                        {
                            flag = true;
                            jsonTreeNode.treeLoader = this.ResolveClientUrl(treeLoaderPath);
                        }
                        if (!flag)
                        {
                            jsonTreeNode.children = new List<JsonTreeNode>();
                            int num2 = 0;
                            if (link.Items != null)
                                num2 = this.BindRecursive(jsonTreeNode.children, link.Items);
                            if (num2 == 0)
                            {
                                jsonTreeNode.leaf = true;
                                jsonTreeNode.children = (List<JsonTreeNode>)null;
                            }
                        }
                        nodes.Add(jsonTreeNode);
                        ++num1;
                    }
                }
                else
                {
                    string permissions = link.permissions;
                    JsonTreeNode jsonTreeNode = new JsonTreeNode();
                    string resFileString = UtilHelper.GetResFileString(link.text);
                    jsonTreeNode.text = resFileString;
                    jsonTreeNode.id = link.id;
                    jsonTreeNode.cls = "nodeCls";
                    jsonTreeNode.iconCls = "iconNodeCls";
                    if (!string.IsNullOrEmpty(link.iconUrl))
                        jsonTreeNode.icon = this.ResolveUrl(link.iconUrl);
                    if (!string.IsNullOrEmpty(link.iconCss))
                        jsonTreeNode.iconCls = link.iconCss;
                    if (string.IsNullOrEmpty(link.command))
                    {
                        if (!string.IsNullOrEmpty(permissions) && !SecurityContext.Current.CheckPermissionForCurentUser(permissions, false))
                            continue;
                    }
                    else
                    {
                        Dictionary<string, string> parameters = new Dictionary<string, string>();
                        parameters.Add("permissions", permissions);
                        if (TreeSource.IsEnableCommand("", "LeftMenu", "", link.command, parameters))
                        {
                            string str = CommandManager.GetCommandString(link.command, (Dictionary<string, string>)null).Replace("\"", "&quot;");
                            jsonTreeNode.href = string.Format("javascript:{0}", (object)str);
                            XmlCommand.GetCommandByName("", "LeftMenu", "", link.command);
                        }
                        else
                            continue;
                    }
                    if (link.expanded)
                        jsonTreeNode.expanded = true;
                    if (!string.IsNullOrEmpty(link.type))
                        jsonTreeNode.type = link.type;
                    bool flag = false;
                    string treeLoader = link.treeLoader;
                    string treeLoaderPath = link.treeLoaderPath;
                    if (!string.IsNullOrEmpty(treeLoader))
                        flag = true;
                    if (!string.IsNullOrEmpty(treeLoaderPath))
                    {
                        flag = true;
                        jsonTreeNode.treeLoader = this.ResolveClientUrl(treeLoaderPath);
                    }
                    if (!flag)
                    {
                        jsonTreeNode.children = new List<JsonTreeNode>();
                        int num2 = 0;
                        if (link.Items != null)
                            num2 = this.BindRecursive(jsonTreeNode.children, link.Items);
                        if (num2 == 0)
                        {
                            jsonTreeNode.leaf = true;
                            jsonTreeNode.children = (List<JsonTreeNode>)null;
                        }
                    }
                    nodes.Add(jsonTreeNode);
                    ++num1;
                }
            }
            return num1;
        }

        private int BindRecursive(List<JsonTreeNode> nodes, XPathNavigator linkItem)
        {
            int num = 0;
            foreach (XPathNavigator linkItem1 in linkItem.SelectChildren(string.Empty, string.Empty))
            {
                string attribute1 = linkItem1.GetAttribute("permissions", string.Empty);
                JsonTreeNode jsonTreeNode = new JsonTreeNode();
                string resFileString = UtilHelper.GetResFileString(linkItem1.GetAttribute("text", string.Empty));
                string attribute2 = linkItem1.GetAttribute("id", string.Empty);
                jsonTreeNode.text = resFileString;
                jsonTreeNode.id = attribute2;
                jsonTreeNode.cls = "nodeCls";
                jsonTreeNode.iconCls = "iconNodeCls";
                string attribute3 = linkItem1.GetAttribute("iconUrl", string.Empty);
                if (!string.IsNullOrEmpty(attribute3))
                    jsonTreeNode.icon = this.ResolveUrl(attribute3);
                string attribute4 = linkItem1.GetAttribute("iconCss", string.Empty);
                if (!string.IsNullOrEmpty(attribute4))
                    jsonTreeNode.iconCls = attribute4;
                string attribute5 = linkItem1.GetAttribute("command", string.Empty);
                if (string.IsNullOrEmpty(attribute5))
                {
                    if (!string.IsNullOrEmpty(attribute1) && !SecurityContext.Current.CheckPermissionForCurentUser(attribute1, false))
                        continue;
                }
                else
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    parameters.Add("permissions", attribute1);
                    if (TreeSource.IsEnableCommand("", "", "", attribute5, parameters))
                    {
                        string str = CommandManager.GetCommandString(attribute5, (Dictionary<string, string>)null).Replace("\"", "&quot;");
                        jsonTreeNode.href = string.Format("javascript:{0}", (object)str);
                    }
                    else
                        continue;
                }
                string attribute6 = linkItem1.GetAttribute("expanded", string.Empty);
                if (!string.IsNullOrEmpty(attribute6) && attribute6.ToLower().Equals("true"))
                    jsonTreeNode.expanded = true;
                string attribute7 = linkItem1.GetAttribute("type", string.Empty);
                if (!string.IsNullOrEmpty(attribute7))
                    jsonTreeNode.type = attribute7;
                bool flag = false;
                string attribute8 = linkItem1.GetAttribute("treeLoader", string.Empty);
                string attribute9 = linkItem1.GetAttribute("treeLoaderPath", string.Empty);
                if (!string.IsNullOrEmpty(attribute8))
                    flag = true;
                if (!string.IsNullOrEmpty(attribute9))
                {
                    flag = true;
                    jsonTreeNode.treeLoader = this.ResolveClientUrl(attribute9);
                }
                if (!flag)
                {
                    jsonTreeNode.children = new List<JsonTreeNode>();
                    if (this.BindRecursive(jsonTreeNode.children, linkItem1) == 0)
                    {
                        jsonTreeNode.leaf = true;
                        jsonTreeNode.children = (List<JsonTreeNode>)null;
                    }
                }
                nodes.Add(jsonTreeNode);
                ++num;
            }
            return num;
        }

        private void BindNode(string returnNodeId)
        {
        }

        private void WriteArray(List<JsonTreeNode> nodes)
        {
            this.Response.Write(JsonSerializer.Serialize((object)nodes));
        }

        private void WriteArray(string json)
        {
            this.Response.CacheControl = "no-cache";
            this.Response.AddHeader("Pragma", "no-cache");
            this.Response.Expires = -1;
            this.Response.Write(json);
        }

        private void BindFullTree()
        {
            List<JsonTreeNode> nodes = new List<JsonTreeNode>();
            XmlBuilder.GetCustomizableXml2(StructureType.View, 1 != 0, new Selector(new string[1]
      {
        "LeftMenu"
      }));
            Navigation navigation = XmlModelHelper.GetNavigation("LeftMenu", string.Empty);
            if (navigation == null || navigation.Tabs == null)
                return;
            if (navigation.Commands.Command != null)
            {
                foreach (Mediachase.BusinessFoundation.XmlObjectModel.Command command in navigation.Commands.Command)
                    XmlCommand.GetCommandByName("", "LeftMenu", "", command.id);
            }
            foreach (Tab tab in navigation.Tabs.Tab)
            {
                JsonTreeNode jsonTreeNode = new JsonTreeNode();
                jsonTreeNode.id = tab.id;
                jsonTreeNode.text = UtilHelper.GetResFileString(tab.text);
                jsonTreeNode.cls = "nodeCls";
                if (!string.IsNullOrEmpty(tab.imageUrl))
                    jsonTreeNode.icon = this.ResolveUrl(tab.imageUrl);
                jsonTreeNode.children = new List<JsonTreeNode>();
                int num = 0;
                if (tab.Items != null)
                    num = this.BindRecursiveNoAsync(jsonTreeNode.children, tab.Items);
                if (num == 0)
                {
                    jsonTreeNode.leaf = true;
                    jsonTreeNode.children = (List<JsonTreeNode>)null;
                }
                nodes.Add(jsonTreeNode);
            }
            this.WriteArray(nodes);
        }

        private int BindRecursiveNoAsync(List<JsonTreeNode> nodes, Link[] linkItems)
        {
            int num1 = 0;
            foreach (Link link in linkItems)
            {
                JsonTreeNode jsonTreeNode = new JsonTreeNode();
                jsonTreeNode.id = link.id;
                jsonTreeNode.text = UtilHelper.GetResFileString(link.text);
                string str = link.order.ToString();
                if (!string.IsNullOrEmpty(str))
                    jsonTreeNode.text = "<span class=\"rightColumn\">" + str + "</span><span class=\"leftColumn\">" + jsonTreeNode.text + "</span>";
                jsonTreeNode.cls = "nodeCls";
                jsonTreeNode.iconCls = "iconNodeCls";
                if (!string.IsNullOrEmpty(link.iconUrl))
                    jsonTreeNode.icon = this.ResolveUrl(link.iconUrl);
                if (!string.IsNullOrEmpty(link.iconCss))
                    jsonTreeNode.iconCls = link.iconCss;
                jsonTreeNode.children = new List<JsonTreeNode>();
                int num2 = 0;
                if (link.Items != null)
                    num2 = this.BindRecursiveNoAsync(jsonTreeNode.children, link.Items);
                if (num2 == 0)
                {
                    jsonTreeNode.leaf = true;
                    jsonTreeNode.children = (List<JsonTreeNode>)null;
                }
                nodes.Add(jsonTreeNode);
                ++num1;
            }
            return num1;
        }

        private int BindRecursiveNoAsync(List<JsonTreeNode> nodes, XPathNavigator linkItem)
        {
            int num = 0;
            foreach (XPathNavigator linkItem1 in linkItem.SelectChildren(string.Empty, string.Empty))
            {
                JsonTreeNode jsonTreeNode = new JsonTreeNode();
                jsonTreeNode.id = linkItem1.GetAttribute("id", string.Empty);
                jsonTreeNode.text = UtilHelper.GetResFileString(linkItem1.GetAttribute("text", string.Empty));
                string attribute1 = linkItem1.GetAttribute("order", string.Empty);
                if (!string.IsNullOrEmpty(attribute1))
                    jsonTreeNode.text = "<span class=\"rightColumn\">" + attribute1 + "</span><span class=\"leftColumn\">" + jsonTreeNode.text + "</span>";
                jsonTreeNode.cls = "nodeCls";
                jsonTreeNode.iconCls = "iconNodeCls";
                string attribute2 = linkItem1.GetAttribute("iconUrl", string.Empty);
                if (!string.IsNullOrEmpty(attribute2))
                    jsonTreeNode.icon = this.ResolveUrl(attribute2);
                string attribute3 = linkItem1.GetAttribute("iconCss", string.Empty);
                if (!string.IsNullOrEmpty(attribute3))
                    jsonTreeNode.iconCls = attribute3;
                jsonTreeNode.children = new List<JsonTreeNode>();
                if (this.BindRecursiveNoAsync(jsonTreeNode.children, linkItem1) == 0)
                {
                    jsonTreeNode.leaf = true;
                    jsonTreeNode.children = (List<JsonTreeNode>)null;
                }
                nodes.Add(jsonTreeNode);
                ++num;
            }
            return num;
        }

        public string CurrentUserMarket()
        {
            string marketRole = string.Empty;

            if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "SunstoreUser"))
                marketRole = "SunstoreUser";

            if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "AmavitaUser"))
                marketRole = "AmavitaUser";

            if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "CoopUser"))
                marketRole = "CoopUser";

            return marketRole;
        }

        public bool CheckifUserHasRoleOnMarket(string tabID)
        {
            bool isInMArket = false;
            if (!string.IsNullOrEmpty(tabID))
            {
                if ((tabID.Equals("Order_PurchaseOrdersSunstore")) || (tabID.Equals("ShippingReceivingSunstore")))
                {
                    if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "SunstoreUser"))
                        isInMArket = true;
                }

                if ((tabID.Equals("Order_PurchaseOrdersAmavita")) || (tabID.Equals("ShippingReceivingAmavita")))
                {
                    if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "AmavitaUser"))
                        isInMArket = true;
                }

                if ((tabID.Equals("Order_PurchaseOrdersCoop")) || (tabID.Equals("ShippingReceivingCoop")))
                {
                    if (SecurityContext.Current.CheckUserInGlobalRole(SecurityContext.Current.CurrentUser, "CoopUser"))
                        isInMArket = true;
                }
            }

            return isInMArket;
        }
    }
}