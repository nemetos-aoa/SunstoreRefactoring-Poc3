//>>built
require({cache:{"url:epi-ecf-ui/widget/templates/BundleEdit.html":"<div>\r\n    <div data-dojo-attach-point=\"toolbar\" data-dojo-type=\"epi-cms/contentediting/StandardToolbar\" class=\"epi-viewHeaderContainer epi-localToolbar\"></div>\r\n    <div data-dojo-attach-point=\"notificationBar\" data-dojo-type=\"epi-cms/contentediting/NotificationBar\" data-dojo-props=\"region:'top'\"></div>\r\n    <div data-dojo-type=\"dijit/layout/ContentPane\" data-dojo-attach-point=\"contentPane\" class=\"epi-bundleEdit\">\r\n        <div data-dojo-attach-point=\"collectionList\" data-dojo-type=\"epi-ecf-ui/contentediting/editors/BundleEntryCollectionEditor\"></div>\r\n    </div>\r\n</div>"}});define("epi-ecf-ui/widget/BundleEdit",["dojo/_base/declare","dijit/_TemplatedMixin","dijit/_WidgetsInTemplateMixin","epi/shell/widget/_ModelBindingMixin","epi/shell/TypeDescriptorManager","epi-cms/widget/Breadcrumb","epi-cms/widget/BreadcrumbCurrentItem","./_RelationViewBase","dojo/text!./templates/BundleEdit.html","epi-cms/contentediting/NotificationBar","epi-cms/contentediting/StandardToolbar","../contentediting/editors/BundleEntryCollectionEditor"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9){return _1([_8,_2,_3,_4],{templateString:_9,contentPane:this.contentPane,updateView:function(_a,_b){this.inherited(arguments);this.set("value",_b.id);},_setValueAttr:function(_c){this.collectionList.set("value",_c);}});});