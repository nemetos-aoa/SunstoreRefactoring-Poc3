//>>built
require({cache:{"url:epi-ecf-ui/component/templates/PricingOverview.html":"<div>\r\n    <div data-dojo-attach-point=\"toolbar\" data-dojo-type=\"epi-cms/contentediting/StandardToolbar\" class=\"epi-viewHeaderContainer epi-localToolbar\"></div>\r\n    <div data-dojo-type='epi-ecf-ui/widget/PricingOverview' data-dojo-attach-point='pricingOverview'></div>\r\n</div>"}});define("epi-ecf-ui/component/PricingOverview",["dojo/_base/declare","dojo/dom-geometry","dijit/layout/_LayoutWidget","dijit/_TemplatedMixin","dijit/_WidgetsInTemplateMixin","dojo/text!./templates/PricingOverview.html","epi/i18n!epi/cms/nls/commerce.components.pricingoverview","epi-cms/contentediting/StandardToolbar","../widget/PricingOverview"],function(_1,_2,_3,_4,_5,_6,_7){return _1([_3,_4,_5],{resources:_7,templateString:_6,updateView:function(_8,_9){this.toolbar.update({currentContext:_9,viewConfigurations:{availableViews:_8.availableViews,viewName:_8.viewName}});this.set("value",_9);this.pricingOverview.showNotification();},_setValueAttr:function(_a){this.pricingOverview.set("value",_a);},layout:function(){var _b=_2.getMarginBox(this.toolbar.domNode);this.pricingOverview.resize({h:this._contentBox.h-_b.h,w:this._contentBox.w});}});});