//>>built
require({cache:{"url:epi-cms/widget/templates/_HyperLinkFieldItem.html":"<div class='epi-formsRow epi-hyperLink'>\r\n    <input type='radio' name='${groupname}' id='${id}_${name}' value='${name}' data-dojo-type='dijit.form.RadioButton' data-dojo-attach-event='onchange:_onRadioButtonChange' data-dojo-attach-point='radioNode'/>\r\n    <label for='${id}_${name}'>${displayName}</label>\r\n    <div class='epi-formsWidgetWrapper' data-dojo-attach-point='containerNode'></div>\r\n</div>"}});define("epi-cms/widget/_HyperLinkFieldItem",["dojo/_base/declare","dijit/_Widget","dijit/_TemplatedMixin","dijit/_Container","dijit/_Contained","dojo/text!./templates/_HyperLinkFieldItem.html"],function(_1,_2,_3,_4,_5,_6){return _1([_2,_4,_5,_3],{templateString:_6,inputWidget:null,settings:[],_onRadioButtonChange:function(_7){this.onRadioButtonChange(this,_7);},onRadioButtonChange:function(_8,_9){},addChild:function(_a){this.inputWidget=_a;this.inherited(arguments);},validate:function(){return this.inputWidget?this.inputWidget.validate():false;},_setValueAttr:function(_b){if(this.inputWidget){this.inputWidget.set("value",_b);}},_getValueAttr:function(){return this.inputWidget.get("value");},_setRequiredAttr:function(_c){if(this.inputWidget){this.inputWidget.set("required",_c);}},_setSelectedAttr:function(_d){if(this.inputWidget){this.inputWidget.set("disabled",!_d);this.radioNode.checked=_d;}},_getSelectedAttr:function(){return this.radioNode.checked;},_setShowAllLanguagesAttr:function(_e){if(this.inputWidget){this.inputWidget.set("showAllLanguages",_e);}},focus:function(){if(this.inputWidget){this.inputWidget.focus();}}});});