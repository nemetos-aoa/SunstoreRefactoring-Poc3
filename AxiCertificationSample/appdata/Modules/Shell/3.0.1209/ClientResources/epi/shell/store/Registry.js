//>>built
define("epi/shell/store/Registry",["dojo","dojo/store/Memory","epi/shell/store/JsonRest","epi/shell/store/Patchable","epi/shell/store/Throttle"],function(_1,_2,_3,_4,_5){return _1.declare(null,{constructor:function(_6){_1.mixin(this,_6);this._stores={};},add:function(_7,_8){if(this._stores[_7]){throw new Error("A store with the name '"+_7+"' already exists.");}return (this._stores[_7]=_8);},create:function(_9,_a,_b){if(this._stores[_9]){throw new Error("A store with the name '"+_9+"' already exists.");}var _c=new _2(_b),_d=new _3(_1.mixin({target:_a,preventCache:true},_b)),_e=_4(_5(_d,"get"),_c),_f=_5(_e,"query");return (this._stores[_9]=_f);},get:function(_10){var _11=this._stores[_10];if(!_11){throw new Error("No store by the name '"+_10+"' exists.");}return _11;}});});