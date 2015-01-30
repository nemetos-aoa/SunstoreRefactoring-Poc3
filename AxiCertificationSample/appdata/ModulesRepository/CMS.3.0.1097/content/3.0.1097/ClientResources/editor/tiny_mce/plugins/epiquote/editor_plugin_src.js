(function(tinymce){tinymce.PluginManager.requireLangPack("epiquote"),tinymce.create("tinymce.plugins.epiquote",{init:function(ed,url){var t=this;ed.addCommand("mceEPiQuote",function(){ed.windowManager.open({file:url+"/epiquote.htm",width:350,height:240,inline:1},{plugin_url:url})}),ed.addButton("epiquote",{title:"epiquote.desc",cmd:"mceEPiQuote","class":"mce_blockquote"}),ed.addShortcut("ctrl+shift+q","epiquote.desc","mceEPiQuote"),ed.onNodeChange.add(function(ed,cm,n,co){var insideQuote=null!=ed.dom.getParent(n,"q")||null!=ed.dom.getParent(n,"blockquote");cm.setDisabled("epiquote",co&&!insideQuote),cm.setActive("epiquote",insideQuote)}),ed.onKeyUp.addToTop(function(ed,e){if(13==e.keyCode){if(e.shiftKey||e.altKey||e.ctrlKey)return;t._quoteEscape(ed,e)}})},_quoteEscape:function(ed,ev){function isConsideredEmpty(node){return""==node.innerHTML||" "==node.innerHTML||"<br>"==node.innerHTML}var pElem,s=ed.selection,current=s.getNode(),parent=ed.dom.getParent(current,"blockquote");if(null!=parent){var children=parent.childNodes,len=children.length;isConsideredEmpty(current)&&children[len-1]===current&&isConsideredEmpty(children[len-2])&&(ed.dom.remove(children[len-2]),ed.dom.remove(current),pElem=ed.dom.create("p",null," "),ed.dom.insertAfter(pElem,parent),s.select(pElem,!0),s.collapse(),tinymce.dom.Event.cancel(ev),ed.undoManager.add())}},getInfo:function(){return{longname:"Quote plugin",author:"EPiServer AB",authorurl:"http://www.episerver.com",infourl:"http://www.episerver.com",version:"1.0"}}}),tinymce.PluginManager.add("epiquote",tinymce.plugins.epiquote)})(tinymce,epiJQuery);