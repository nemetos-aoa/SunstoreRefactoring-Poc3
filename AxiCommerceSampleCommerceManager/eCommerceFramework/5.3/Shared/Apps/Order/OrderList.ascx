<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mediachase.ConsoleManagerUpdate.eCommerceFramework._5._3.Shared.Apps.Order.OrdersList" Codebehind="OrderList.ascx.cs" %>
<%@ Register Src="~/Apps/Core/Controls/EcfListViewControl.ascx" TagName="EcfListViewControl" TagPrefix="core" %>
<orders:OrderDataSource runat="server" ID="OrderListDataSource"></orders:OrderDataSource>
<core:EcfListViewControl id="MyListView" runat="server" DataSourceID="OrderListDataSource" AppId="Order" ViewId="Orders-List" ShowTopToolbar="true"></core:EcfListViewControl>

