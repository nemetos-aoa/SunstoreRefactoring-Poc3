//>>built
define("epi-ecf-ui/contentediting/editors/RelationGroupSelectionEditor",["dojo/_base/declare","dijit/form/ComboBox","epi/dependency"],function(_1,_2,_3){return _1([_2],{storeKey:"epi.commerce.relationgroupdefinition",constructor:function(){this.store=this.store||_3.resolve("epi.storeregistry").get(this.storeKey);this.autoComplete=false;}});});