System.register(["@kentico/xperience-admin-base","@kentico/xperience-admin-components","react"],(function(e,t){var a={},r={},o={};return{setters:[function(e){a.useFormComponentCommandProvider=e.useFormComponentCommandProvider,a.usePageCommand=e.usePageCommand},function(e){r.Button=e.Button,r.ButtonColor=e.ButtonColor,r.ButtonSize=e.ButtonSize,r.Dialog=e.Dialog,r.DropPlacement=e.DropPlacement,r.FormItemWrapper=e.FormItemWrapper,r.Headline=e.Headline,r.HeadlineSize=e.HeadlineSize,r.Icon=e.Icon,r.Input=e.Input,r.TreeNode=e.TreeNode,r.TreeNodeLeadingIcon=e.TreeNodeLeadingIcon,r.TreeNodeTitle=e.TreeNodeTitle,r.TreeNodeTrailingIcon=e.TreeNodeTrailingIcon,r.TreeView=e.TreeView},function(e){o.createContext=e.createContext,o.default=e.default,o.useContext=e.useContext,o.useEffect=e.useEffect,o.useMemo=e.useMemo,o.useReducer=e.useReducer,o.useState=e.useState}],execute:function(){e((()=>{var e={459:(e,t,a)=>{"use strict";a.d(t,{Z:()=>c});var r=a(81),o=a.n(r),n=a(645),l=a.n(n)()(o());l.push([e.id,".icon-clickable {\n    padding: 5px;\n    color: darkgray;\n    border-radius: 5px\n}\n    .icon-clickable.delete {\n        color: red;\n        margin-right: 5px;\n    }\n    .icon-clickable.add {\n        color: green;\n    }\n    .icon-clickable:hover {\n        opacity: 0.8;\n        background-color: darkgray;\n        color: white;\n    }\n    .icon-clickable.add:hover {\n        background-color: green;\n    }\n\n    .icon-clickable.delete:hover {\n        background-color: red;\n    }",""]);const c=l},970:(e,t,a)=>{"use strict";a.d(t,{Z:()=>c});var r=a(81),o=a.n(r),n=a(645),l=a.n(n)()(o());l.push([e.id,".dialog-z-index {\n    z-index: 3001;\n}\n",""]);const c=l},645:e=>{"use strict";e.exports=function(e){var t=[];return t.toString=function(){return this.map((function(t){var a="",r=void 0!==t[5];return t[4]&&(a+="@supports (".concat(t[4],") {")),t[2]&&(a+="@media ".concat(t[2]," {")),r&&(a+="@layer".concat(t[5].length>0?" ".concat(t[5]):""," {")),a+=e(t),r&&(a+="}"),t[2]&&(a+="}"),t[4]&&(a+="}"),a})).join("")},t.i=function(e,a,r,o,n){"string"==typeof e&&(e=[[null,e,void 0]]);var l={};if(r)for(var c=0;c<this.length;c++){var i=this[c][0];null!=i&&(l[i]=!0)}for(var s=0;s<e.length;s++){var d=[].concat(e[s]);r&&l[d[0]]||(void 0!==n&&(void 0===d[5]||(d[1]="@layer".concat(d[5].length>0?" ".concat(d[5]):""," {").concat(d[1],"}")),d[5]=n),a&&(d[2]?(d[1]="@media ".concat(d[2]," {").concat(d[1],"}"),d[2]=a):d[2]=a),o&&(d[4]?(d[1]="@supports (".concat(d[4],") {").concat(d[1],"}"),d[4]=o):d[4]="".concat(o)),t.push(d))}},t}},81:e=>{"use strict";e.exports=function(e){return e[1]}},379:e=>{"use strict";var t=[];function a(e){for(var a=-1,r=0;r<t.length;r++)if(t[r].identifier===e){a=r;break}return a}function r(e,r){for(var n={},l=[],c=0;c<e.length;c++){var i=e[c],s=r.base?i[0]+r.base:i[0],d=n[s]||0,g="".concat(s," ").concat(d);n[s]=d+1;var u=a(g),m={css:i[1],media:i[2],sourceMap:i[3],supports:i[4],layer:i[5]};if(-1!==u)t[u].references++,t[u].updater(m);else{var p=o(m,r);r.byIndex=c,t.splice(c,0,{identifier:g,updater:p,references:1})}l.push(g)}return l}function o(e,t){var a=t.domAPI(t);return a.update(e),function(t){if(t){if(t.css===e.css&&t.media===e.media&&t.sourceMap===e.sourceMap&&t.supports===e.supports&&t.layer===e.layer)return;a.update(e=t)}else a.remove()}}e.exports=function(e,o){var n=r(e=e||[],o=o||{});return function(e){e=e||[];for(var l=0;l<n.length;l++){var c=a(n[l]);t[c].references--}for(var i=r(e,o),s=0;s<n.length;s++){var d=a(n[s]);0===t[d].references&&(t[d].updater(),t.splice(d,1))}n=i}}},569:e=>{"use strict";var t={};e.exports=function(e,a){var r=function(e){if(void 0===t[e]){var a=document.querySelector(e);if(window.HTMLIFrameElement&&a instanceof window.HTMLIFrameElement)try{a=a.contentDocument.head}catch(e){a=null}t[e]=a}return t[e]}(e);if(!r)throw new Error("Couldn't find a style target. This probably means that the value for the 'insert' parameter is invalid.");r.appendChild(a)}},216:e=>{"use strict";e.exports=function(e){var t=document.createElement("style");return e.setAttributes(t,e.attributes),e.insert(t,e.options),t}},565:(e,t,a)=>{"use strict";e.exports=function(e){var t=a.nc;t&&e.setAttribute("nonce",t)}},795:e=>{"use strict";e.exports=function(e){if("undefined"==typeof document)return{update:function(){},remove:function(){}};var t=e.insertStyleElement(e);return{update:function(a){!function(e,t,a){var r="";a.supports&&(r+="@supports (".concat(a.supports,") {")),a.media&&(r+="@media ".concat(a.media," {"));var o=void 0!==a.layer;o&&(r+="@layer".concat(a.layer.length>0?" ".concat(a.layer):""," {")),r+=a.css,o&&(r+="}"),a.media&&(r+="}"),a.supports&&(r+="}");var n=a.sourceMap;n&&"undefined"!=typeof btoa&&(r+="\n/*# sourceMappingURL=data:application/json;base64,".concat(btoa(unescape(encodeURIComponent(JSON.stringify(n))))," */")),t.styleTagTransform(r,e,t.options)}(t,e,a)},remove:function(){!function(e){if(null===e.parentNode)return!1;e.parentNode.removeChild(e)}(t)}}}},589:e=>{"use strict";e.exports=function(e,t){if(t.styleSheet)t.styleSheet.cssText=e;else{for(;t.firstChild;)t.removeChild(t.firstChild);t.appendChild(document.createTextNode(e))}}},722:(e,t,a)=>{const r=a(905).R;t.s=function(e){if(e||(e=1),!a.y.meta||!a.y.meta.url)throw console.error("__system_context__",a.y),Error("systemjs-webpack-interop was provided an unknown SystemJS context. Expected context.meta.url, but none was provided");a.p=r(a.y.meta.url,e)}},905:(e,t,a)=>{t.R=function(e,t){var a=document.createElement("a");a.href=e;for(var r="/"===a.pathname[0]?a.pathname:"/"+a.pathname,o=0,n=r.length;o!==t&&n>=0;)"/"===r[--n]&&o++;if(o!==t)throw Error("systemjs-webpack-interop: rootDirectoryLevel ("+t+") is greater than the number of directories ("+o+") in the URL path "+e);var l=r.slice(0,n+1);return a.protocol+"//"+a.host+l};Number.isInteger},952:e=>{"use strict";e.exports=a},271:e=>{"use strict";e.exports=r},954:e=>{"use strict";e.exports=o}},n={};function l(t){var a=n[t];if(void 0!==a)return a.exports;var r=n[t]={id:t,exports:{}};return e[t](r,r.exports,l),r.exports}l.y=t,l.n=e=>{var t=e&&e.__esModule?()=>e.default:()=>e;return l.d(t,{a:t}),t},l.d=(e,t)=>{for(var a in t)l.o(t,a)&&!l.o(e,a)&&Object.defineProperty(e,a,{enumerable:!0,get:t[a]})},l.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),l.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},l.p="",l.nc=void 0;var c={};return(0,l(722).s)(1),(()=>{"use strict";l.r(c),l.d(c,{CategoryListFormComponent:()=>L,CategoryListPageTemplate:()=>S,CategorySelectContentItemTemplate:()=>w,CategorySelectPageTemplate:()=>P});var e=l(952),t=l(271),a=l(954),r=l(379),o=l.n(r),n=l(795),i=l.n(n),s=l(569),d=l.n(s),g=l(565),u=l.n(g),m=l(216),p=l.n(m),y=l(589),f=l.n(y),C=l(459),I={};I.styleTagTransform=f(),I.setAttributes=u(),I.insert=d().bind(null,"head"),I.domAPI=i(),I.insertStyleElement=p(),o()(C.Z,I),C.Z&&C.Z.locals&&C.Z.locals;const v=(e,t)=>{const{type:a,data:r}=t,o={...e};switch(a){case"categorySet":o.categories=[...o.categories];const e=o.categories.findIndex((e=>e.categoryID==r.categoryID));e>-1?o.categories[e]=r:o.categories.push(r),o.dialogOptions.currentCategory=null;break;case"categorySelect":o.dialogOptions.currentCategory=r;break;case"categoryDelete":o.categories=o.categories.filter((e=>e.categoryID!=r));break;case"setDialog":o.dialogOptions=r;break;case"categoriesSet":o.categories=[...o.categories];for(const e of r){const t=o.categories.findIndex((t=>t.categoryID==e.categoryID));t>-1?o.categories[t]=e:o.categories.push(e)}}return o},h={isOpen:!1,headline:"",message:""},D=(0,a.createContext)({setCurrentCategory:e=>{},setCategory:e=>{},setCategories:e=>{},deleteCategory:e=>{},setDialog:e=>{},dialogOptions:h,categories:[]}),x=()=>{const{setCategory:e,deleteCategory:r,dialogOptions:o,setDialog:n}=(0,a.useContext)(D),[l,c]=(0,a.useState)("");(0,a.useEffect)((()=>{c(o.currentCategory?.categoryDisplayName??"")}),[o.currentCategory]);const i=()=>{n({...o,isOpen:!1,currentCategory:null})};return a.default.createElement(t.Dialog,{isOpen:o.isOpen,headline:o.headline,onClose:i,isDismissable:!0,headerCloseButton:{tooltipText:"Close Dialog"},confirmAction:{label:"Okay",onClick:()=>{"setCategory"==o.type&&o.currentCategory?e({...o.currentCategory,categoryDisplayName:l}):"deleteCategory"==o.type&&o.currentCategory&&r(o.currentCategory.categoryID??-1),n({...o,isOpen:!1,currentCategory:null})}},cancelAction:{label:"Cancel",onClick:i}},"setCategory"==o.type&&a.default.createElement(t.Input,{label:"Display Name",value:l,onChange:e=>c(e.currentTarget.value)}),o.message)},E=({selected:e,category:r,hovered:o})=>{const{setDialog:n,dialogOptions:l}=(0,a.useContext)(D);return a.default.createElement("div",{style:{display:"flex",flexDirection:"row",alignItems:"center"}},a.default.createElement(t.TreeNodeLeadingIcon,{isSelected:e,draggable:!0},a.default.createElement(t.Icon,{name:o?"xp-dots-vertical":"xp-parent-child-scheme"})),a.default.createElement(t.TreeNodeTitle,{isSelected:e},r.categoryDisplayName),o&&a.default.createElement(a.default.Fragment,null,a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{className:"icon-clickable",onClick:e=>{e.stopPropagation(),n({...l,currentCategory:r,isOpen:!0,type:"setCategory",headline:"Update Category",message:""})}},a.default.createElement(t.Icon,{name:"xp-edit"}))),a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{className:"icon-clickable add",onClick:e=>{var t;e.stopPropagation(),t={categoryDisplayName:"",categoryParentID:r.categoryID},n({...l,currentCategory:t,isOpen:!0,type:"setCategory",headline:"Add Category",message:"Add category to parent category: "+r.categoryDisplayName})}},a.default.createElement(t.Icon,{name:"xp-plus-circle"}))),a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{className:"icon-clickable delete",onClick:e=>{e.stopPropagation(),n({...l,currentCategory:r,isOpen:!0,type:"deleteCategory",headline:"Delete Category",message:"Delete category: "+r.categoryDisplayName})}},a.default.createElement(t.Icon,{name:"xp-minus-circle"})))))},b=({category:e,level:r})=>{const{dialogOptions:o,setDialog:n,setCategories:l,categories:c,setCategory:i}=(0,a.useContext)(D),[s,d]=(0,a.useState)((e.children?.length??0)>0);return(0,a.useEffect)((()=>{!s&&(e.children?.length??0)>0&&d(!0)}),[e.children?.length]),a.default.createElement(t.TreeNode,{name:e.categoryName??"",hasChildren:(e.children?.length??0)>0,dropHandler:(e,a,r)=>{var o=c.find((t=>""+t.categoryID==e)),n=c.find((e=>""+e.categoryID==a));if(r==t.DropPlacement.Child)o&&n&&(o.categoryParentID=n.categoryID,i(o));else if(n){var s=n,d=c.filter((e=>e.categoryParentID==s.categoryParentID&&e.categoryID!=o?.categoryID)).sort(((e,t)=>(e.categoryOrder??0)-(t.categoryOrder??0))),g=d.findIndex((e=>e.categoryID==s.categoryID));if(r==t.DropPlacement.Below&&g++,o&&n){o.categoryParentID=n.categoryParentID;var u=[...d.slice(0,g),o,...d.slice(g)];l(u)}}},nodeIdentifier:e.categoryID?.toString()??"",isDraggable:!0,isToggleable:(e.children?.length??0)>0,onNodeToggle:e=>d(e),isExpanded:s,renderNode:(t,r,o)=>o&&o(a.default.createElement("div",null,a.default.createElement(E,{selected:t,category:e,hovered:r})),{}),level:r},e.children&&e.children.map((e=>a.default.createElement(b,{key:e.categoryID,category:e,level:r+1}))))},T=b,N=e=>{e.forEach((t=>{t.children=e.filter((e=>e.categoryParentID===t.categoryID)).sort(((e,t)=>(e.categoryOrder??0)-(t.categoryOrder??0)))}));var t=e.filter((e=>!e.categoryParentID)).sort(((e,t)=>(e.categoryOrder??0)-(t.categoryOrder??0)));return console.log(t),t},S=({categories:r})=>{const[o,n]=(0,a.useReducer)(v,{categories:r,dialogOptions:h}),{execute:l}=(0,e.usePageCommand)("SetCategory",{after:e=>{e&&n({type:"categorySet",data:e})}}),{execute:c}=(0,e.usePageCommand)("SetCategories",{after:e=>{e&&n({type:"categoriesSet",data:e})}}),{execute:i}=(0,e.usePageCommand)("DeleteCategory",{after:e=>{e&&n({type:"categoryDelete",data:e})}}),s=(0,a.useMemo)((()=>N(o.categories)),[o.categories]),d=e=>{n({type:"setDialog",data:e})};return a.default.createElement("div",null,a.default.createElement(t.Headline,{size:t.HeadlineSize.M},"Category List"),a.default.createElement(D.Provider,{value:{setCategory:e=>{if(e.categoryID)n({type:"categorySet",data:e});else{o.categories.filter((t=>t.categoryParentID==e.categoryParentID));var t=Math.max(...r.map((e=>e.categoryOrder??0)),0);e.categoryOrder=t+1}l(e)},setCategories:e=>{var t=e.map(((e,t)=>(e.categoryOrder=t,e)));n({type:"categoriesSet",data:e}),c(t)},deleteCategory:e=>{i(e)},setCurrentCategory:e=>{n({type:"categorySelect",data:e})},dialogOptions:o.dialogOptions,setDialog:d,categories:o.categories}},a.default.createElement(t.TreeView,null,a.default.createElement(t.TreeNode,{name:"",hasChildren:!0,nodeIdentifier:"",isDraggable:!0,dropHandler:()=>{},isToggleable:!1,isExpanded:!0,renderNode:(e,r)=>a.default.createElement(a.default.Fragment,null,a.default.createElement(t.TreeNodeLeadingIcon,{isSelected:e,draggable:!1},a.default.createElement(t.Icon,{name:"xp-parent-child-scheme"})),a.default.createElement(t.TreeNodeTitle,{isSelected:e},"Root Category"),r&&a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{className:"icon-clickable add",style:{marginRight:5},onClick:e=>{e.stopPropagation(),d({...o.dialogOptions,currentCategory:{categoryDisplayName:""},isOpen:!0,type:"setCategory",headline:"Add Category",message:""})}},a.default.createElement(t.Icon,{name:"xp-plus-circle"})))),level:0},s.map((e=>a.default.createElement(T,{key:e.categoryID,category:e,level:1}))))),a.default.createElement(x,null)))},O=(e,t)=>{const{type:a,data:r}=t,o={...e};switch(a){case"add-category":o.contentItemCategoryItems?.push({categoryID:r,contentItemID:o.contentItemID??-1}),o.contentItemCategoryItems&&(o.contentItemCategoryItems=[...o.contentItemCategoryItems]);break;case"remove-category":o.contentItemCategoryItems=o.contentItemCategoryItems?.filter((e=>e.categoryID!=r))}return o},k=(0,a.createContext)({categories:[]}),P=({categories:r,contentItemCategories:o,contentItemID:n})=>{const[l,c]=(0,a.useReducer)(O,{contentItemCategoryItems:o,contentItemID:n}),{execute:i}=(0,e.usePageCommand)("AddContentItemCategory",{after:e=>{e&&c({type:"add-category",data:e})}}),{execute:s}=(0,e.usePageCommand)("RemoveContentItemCategory",{after:e=>{e&&c({type:"remove-category",data:e})}}),d=(0,a.useMemo)((()=>N(r)),[r]);return a.default.createElement("div",{style:{padding:15}},a.default.createElement(t.Headline,{size:t.HeadlineSize.M},"Categories"),a.default.createElement("div",null,a.default.createElement(k.Provider,{value:{categories:r,addCategory:i,removeCategory:s,contentItemCategoryItems:l.contentItemCategoryItems}},a.default.createElement(t.TreeView,null,d.map((e=>a.default.createElement(M,{key:e.categoryID,category:e,level:1})))))))},w=P,M=({category:e,level:r})=>{var o=()=>(e.children?.length??0)>0;const[n,l]=(0,a.useState)(o()),{contentItemCategoryItems:c,addCategory:i,removeCategory:s}=(0,a.useContext)(k),d=(0,a.useMemo)((()=>c?.find((t=>t.categoryID==e.categoryID))),[e,c]);return a.default.createElement(t.TreeNode,{name:e.categoryName+"",hasChildren:o(),nodeIdentifier:""+e.categoryID,isDraggable:!0,isToggleable:o(),onNodeToggle:e=>l(e),isSelectable:!0,isExpanded:n,dropHandler:()=>{},renderNode:(r,o)=>a.default.createElement(a.default.Fragment,null,a.default.createElement(t.TreeNodeLeadingIcon,{isSelected:r,draggable:!1},a.default.createElement(t.Icon,{name:"xp-parent-child-scheme"})),a.default.createElement(t.TreeNodeTitle,{isSelected:r},e.categoryDisplayName),a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{style:{color:d?"green":"darkgray",paddingRight:10}},a.default.createElement(t.Icon,{name:d?"xp-check-circle":"xp-circle"})))),onNodeClick:()=>{d?s&&e.categoryID&&s(e.categoryID):i&&e.categoryID&&i(e.categoryID)},level:r},e.children&&e.children.map((e=>a.default.createElement(M,{key:e.categoryID,category:e,level:r+1}))))};var R=l(970),A={};A.styleTagTransform=f(),A.setAttributes=u(),A.insert=d().bind(null,"head"),A.domAPI=i(),A.insertStyleElement=p(),o()(R.Z,A),R.Z&&R.Z.locals&&R.Z.locals;const B=(e,t)=>{const{type:a,data:r}=t,o={...e};switch(a){case"categories":o.categories=r;break;case"dialog-options":o.dialogOptions=r}return o},H=(0,a.createContext)({categories:[],selectedCategories:[]}),L=r=>{var[o,n]=(0,a.useReducer)(B,{categories:[],dialogOptions:{isOpen:!1,selectedCategories:[]}});const{executeCommand:l}=(0,e.useFormComponentCommandProvider)();(0,a.useEffect)((()=>{l(r,"GetCategories").then((e=>{if(e){n({type:"categories",data:e});var t=e.map((e=>e.categoryID)),a=r.value,o=r.value.filter((e=>t.indexOf(e)>-1));a.length!=o.length&&r.onChange&&r.onChange(o)}}))}),[]);const c=()=>{n({type:"dialog-options",data:{isOpen:!1,selectedCategories:[]}})},i=(0,a.useMemo)((()=>N(o.categories)),[o.categories]);return a.default.createElement(t.FormItemWrapper,{label:r.label,explanationText:r.explanationText,invalid:r.invalid,validationMessage:r.validationMessage,markAsRequired:r.required,labelIcon:r.tooltip?"xp-i-circle":void 0,labelIconTooltip:r.tooltip,childrenWrapperClassnames:"category-list"},r.value.length>0&&a.default.createElement("div",{style:{display:"flex",flexDirection:"row",flexWrap:"wrap",alignItems:"flex-start",border:"1px solid lightgrey",borderRadius:"25px",padding:10,marginBottom:10}},r.value.map((e=>a.default.createElement(t.Button,{key:e,label:o.categories.find((t=>t.categoryID==e))?.categoryDisplayName,color:t.ButtonColor.Tertiary,size:t.ButtonSize.XS,trailingIcon:"xp-minus-circle",onClick:()=>{return t=e,a=r.value.filter((e=>e!=t)),void(r.onChange&&r.onChange(a));var t,a}})))),a.default.createElement(t.Button,{label:"Select Categories",onClick:()=>{n({type:"dialog-options",data:{isOpen:!0,selectedCategories:r.value}})},color:t.ButtonColor.Primary}),o.dialogOptions.isOpen&&a.default.createElement(t.Dialog,{isOpen:o.dialogOptions.isOpen,headline:"Select Categories",isDismissable:!0,headerCloseButton:{tooltipText:"Close Dialog"},confirmAction:{label:"Done",onClick:()=>{r.onChange&&r.onChange(o.dialogOptions.selectedCategories),c()}},cancelAction:{label:"Cancel",onClick:c},onClose:c,overlayClassName:"dialog-z-index"},a.default.createElement(H.Provider,{value:{categories:o.categories,selectedCategories:o.dialogOptions.selectedCategories,addCategory:e=>{var t=[...o.dialogOptions.selectedCategories];t.push(e),n({type:"dialog-options",data:{isOpen:!0,selectedCategories:t}})},removeCategory:e=>{var t=o.dialogOptions.selectedCategories.filter((t=>t!=e));n({type:"dialog-options",data:{isOpen:!0,selectedCategories:t}})}}},a.default.createElement(t.TreeView,null,i.map((e=>a.default.createElement(z,{key:e.categoryID,category:e,level:1})))))))},z=({category:e,level:r})=>{const o=()=>(e.children?.length??0)>0,[n,l]=(0,a.useState)(!1),{selectedCategories:c,addCategory:i,removeCategory:s}=(0,a.useContext)(H),d=(0,a.useMemo)((()=>c?.find((t=>t==e.categoryID))),[e,c]);return a.default.createElement(t.TreeNode,{name:e.categoryName+"",nodeIdentifier:e.categoryID+"",isDraggable:!0,isToggleable:o(),onNodeToggle:e=>l(e),isSelectable:!0,isExpanded:n,hasChildren:o(),dropHandler:()=>{},renderNode:(r,o)=>a.default.createElement(a.default.Fragment,null,a.default.createElement(t.TreeNodeLeadingIcon,{isSelected:r,draggable:!1},a.default.createElement(t.Icon,{name:"xp-parent-child-scheme"})),a.default.createElement(t.TreeNodeTitle,{isSelected:r},e.categoryDisplayName),a.default.createElement(t.TreeNodeTrailingIcon,null,a.default.createElement("span",{style:{color:d?"green":"darkgray",paddingRight:10}},a.default.createElement(t.Icon,{name:d?"xp-check-circle":"xp-circle"})))),onNodeClick:()=>{d?s&&e.categoryID&&s(e.categoryID):i&&e.categoryID&&i(e.categoryID)},level:r},e.children&&e.children.map((e=>a.default.createElement(z,{key:e.categoryID,category:e,level:r+1}))))}})(),c})())}}}));