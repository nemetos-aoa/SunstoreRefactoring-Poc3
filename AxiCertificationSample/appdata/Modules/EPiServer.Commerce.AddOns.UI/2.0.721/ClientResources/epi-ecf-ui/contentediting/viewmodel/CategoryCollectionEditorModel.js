//>>built
define("epi-ecf-ui/contentediting/viewmodel/CategoryCollectionEditorModel",["dojo/_base/declare","dojo/_base/lang","dojo/topic","dojo/when","epi/shell/selection","epi-cms/contentediting/editors/model/CollectionEditorModel","./CategoryCollectionReadOnlyEditorModel","./_RelationCollectionEditorModelMixin","../../command/DetachFromCategory","epi/i18n!epi/nls/episerver.shared","epi/i18n!epi/cms/nls/commerce.contentediting.editors.categorycollectioneditor"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a,_b){return _1([_7,_8],{res:_b,getItemCommands:function(_c,_d,_e){var _f=this.inherited(arguments);if(_f){for(var i=0;i<_f.length;i++){if(_f[i].name=="remove"){var _10=_f[i];var _11=new _9({name:_10.name,category:_10.category,label:_10.label,iconClass:_10.iconClass,selection:new _5(),categoryLink:_c.target,isUsedByCollectionEditor:true});_4(this.getCurrentContent(),function(_12){_11.selection.data=[{data:_12,type:"epi.cms.contentdata"}];_11.set("model",{});});_f[i]=_11;}}}return _f;},addItem:function(_13,_14,_15){return _4(this.inherited(arguments),function(){_3.publish("relationChanged",_13);});}});});