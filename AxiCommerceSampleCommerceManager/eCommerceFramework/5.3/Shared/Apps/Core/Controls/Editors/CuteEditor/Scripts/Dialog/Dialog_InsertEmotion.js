var OxOf649=[""," ","=\x22","\x22","src","^[a-z]*:[/][/][^/]*","Edit","\x3CIMG border=\x220\x22 align=\x22absmiddle\x22 src=\x22","\x22 src_cetemp=\x22","\x22\x3E","ImageTable","IMG","length","className","dialogButton","CuteEditor_ColorPicker_ButtonOver(this)","onmouseover","insert(this)","onclick"];var editor=Window_GetDialogArguments(window); function attr(name,Ox115){if(!Ox115||Ox115==OxOf649[0x0]){return OxOf649[0x0];} ;return OxOf649[0x1]+name+OxOf649[0x2]+Ox115+OxOf649[0x3];}  ; function insert(img){if(img){var src=img[OxOf649[0x4]]; src=src.replace( new RegExp(OxOf649[0x5],OxOf649[0x0]),OxOf649[0x0]) ;var Ox2b=OxOf649[0x0];if(editor.GetActiveTab()==OxOf649[0x6]){ Ox2b=OxOf649[0x7]+src+OxOf649[0x8]+src+OxOf649[0x9] ;} else { Ox2b=OxOf649[0x7]+src+OxOf649[0x9] ;} ; editor.PasteHTML(Ox2b) ; Window_CloseDialog(window) ;} ;}  ; function do_Close(){ Window_CloseDialog(window) ;}  ;var ImageTable=Window_GetElement(window,OxOf649[0xa],true);var images=ImageTable.getElementsByTagName(OxOf649[0xb]);var len=images[OxOf649[0xc]];for(var i=0x0;i<len;i++){var img=images[i]; img[OxOf649[0xd]]=OxOf649[0xe] ; img[OxOf649[0x10]]= new Function(OxOf649[0xf]) ; img[OxOf649[0x12]]= new Function(OxOf649[0x11]) ;} ;