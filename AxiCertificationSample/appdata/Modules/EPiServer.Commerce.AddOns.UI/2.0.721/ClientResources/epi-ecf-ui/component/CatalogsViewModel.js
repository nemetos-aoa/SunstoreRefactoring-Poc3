//>>built
define("epi-ecf-ui/component/CatalogsViewModel",["dojo/_base/declare","epi-cms/widget/viewmodel/HierarchicalListViewModel","../widget/viewmodel/CatalogTreeStoreModel"],function(_1,_2,_3){return _1([_2],{treeStoreModelClass:_3,postscript:function(){this.isAvailableFlags=this.isAvailableFlags||this.menuType.ROOT|this.menuType.TREE|this.menuType.LIST;this.inherited(arguments);this.set("commands",[]);},_getSortSettings:function(){return [];},contentContextChanged:function(_4,_5){var _6=this.get("currentListItem");if(_6){return;}this.inherited(arguments);}});});