//>>built
define("epi/shell/widget/CheckBox",["dojo/_base/declare","dijit/_Widget","dijit/_TemplatedMixin","dijit/_WidgetsInTemplateMixin","dijit/form/CheckBox"],function(_1,_2,_3,_4){return _1([_2,_3,_4],{templateString:"<div class=\"dijit dijitReset dijitInline\"><input data-dojo-type=\"dijit/form/CheckBox\" data-dojo-attach-point=\"checkbox\" data-dojo-attach-event=\"onChange: _onChange\"></div>",value:null,_setValueAttr:function(_5){this.value=_5;this.checkbox.set("checked",_5);},_setReadOnlyAttr:function(_6){this.checkbox.set("readOnly",_6);},_getReadOnlyAttr:function(){return this.checkbox.set("readOnly");},_getValueAttr:function(){return this.checkbox.get("checked");},_onChange:function(_7){this.value=_7;this.onChange(_7);},onChange:function(_8){}});});