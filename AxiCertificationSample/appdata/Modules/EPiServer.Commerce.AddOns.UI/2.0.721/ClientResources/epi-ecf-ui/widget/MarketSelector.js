//>>built
define("epi-ecf-ui/widget/MarketSelector",["dojo/_base/declare","dijit/form/Select","epi/dependency"],function(_1,_2,_3){return _1([_2],{keyProfileSetting:"market",addDefaultItem:true,excludeCurrencies:true,excludeInactive:true,postMixInProperties:function(){this.inherited(arguments);this.labelAttr="label";this.sortByLabel=false;this.store=this.store||_3.resolve("epi.storeregistry").get("epi.commerce.market");this.query={addDefaultItem:this.addDefaultItem,excludeCurrencies:this.excludeCurrencies,excludeInactive:this.excludeInactive};}});});