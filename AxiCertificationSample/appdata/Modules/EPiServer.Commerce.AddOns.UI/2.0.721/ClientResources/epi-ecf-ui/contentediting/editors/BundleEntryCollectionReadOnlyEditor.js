//>>built
define("epi-ecf-ui/contentediting/editors/BundleEntryCollectionReadOnlyEditor",["dojo/_base/declare","./ReadOnlyCollectionEditor","../viewmodel/BundleEntryCollectionReadOnlyEditorModel","epi/i18n!epi/cms/nls/commerce.contentediting.editors.bundleentrycollectioneditor"],function(_1,_2,_3,_4){return _1([_2],{iconClass:"epi-iconObjectBundle",modelType:_3,_renderNoDataMessage:function(){this.grid.set("noDataMessage",_4.nodatamessage);this.inherited(arguments);},changeToView:"bundleview",buttonLabel:_4.editbuttontext});});