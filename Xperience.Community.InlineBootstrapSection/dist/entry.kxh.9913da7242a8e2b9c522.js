System.register(["react","@kentico/xperience-admin-components"],(function(e,t){var o={},n={};return{setters:[function(e){o.default=e.default},function(e){n.FormItemWrapper=e.FormItemWrapper}],execute:function(){e((()=>{var e={722:(e,t,o)=>{const n=o(905).R;t.s=function(e){if(e||(e=1),!o.y.meta||!o.y.meta.url)throw console.error("__system_context__",o.y),Error("systemjs-webpack-interop was provided an unknown SystemJS context. Expected context.meta.url, but none was provided");o.p=n(o.y.meta.url,e)}},905:(e,t,o)=>{t.R=function(e,t){var o=document.createElement("a");o.href=e;for(var n="/"===o.pathname[0]?o.pathname:"/"+o.pathname,r=0,a=n.length;r!==t&&a>=0;)"/"===n[--a]&&r++;if(r!==t)throw Error("systemjs-webpack-interop: rootDirectoryLevel ("+t+") is greater than the number of directories ("+r+") in the URL path "+e);var l=n.slice(0,a+1);return o.protocol+"//"+o.host+l};Number.isInteger},271:e=>{"use strict";e.exports=n},954:e=>{"use strict";e.exports=o}},r={};function a(t){var o=r[t];if(void 0!==o)return o.exports;var n=r[t]={exports:{}};return e[t](n,n.exports,a),n.exports}a.y=t,a.d=(e,t)=>{for(var o in t)a.o(t,o)&&!a.o(e,o)&&Object.defineProperty(e,o,{enumerable:!0,get:t[o]})},a.o=(e,t)=>Object.prototype.hasOwnProperty.call(e,t),a.r=e=>{"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},a.p="";var l={};return(0,a(722).s)(1),(()=>{"use strict";a.r(l),a.d(l,{BootstrapRowFormComponent:()=>o});var e=a(954),t=a(271);const o=o=>{const r=()=>[...o.value].map((e=>({...e})));return e.default.createElement(t.FormItemWrapper,{label:o.label,explanationText:o.explanationText,invalid:o.invalid,validationMessage:o.validationMessage,markAsRequired:o.required,labelIcon:o.tooltip?"xp-i-circle":void 0,labelIconTooltip:o.tooltip},o.value.map(((t,a)=>e.default.createElement("div",{style:n.container,key:a},e.default.createElement("div",null,e.default.createElement("label",{style:n.label},"Column Size"),e.default.createElement("div",{style:n.wrapper},e.default.createElement("select",{style:n.components,className:"form-control",value:t.size,onChange:e=>((e,t)=>{if(o.onChange){var n=r();n.forEach(((o,n)=>{n==e&&(o.size=parseInt(t))})),o.onChange(n)}})(a,e.currentTarget.value)},e.default.createElement("option",{value:"12"},"12 Column (100%)"),e.default.createElement("option",{value:"11"},"11 Column (92%)"),e.default.createElement("option",{value:"10"},"10 Column (83%)"),e.default.createElement("option",{value:"9"},"9 Column (75%)"),e.default.createElement("option",{value:"8"},"8 Column (66%)"),e.default.createElement("option",{value:"7"},"7 Column (58%)"),e.default.createElement("option",{value:"6"},"6 Column (50%)"),e.default.createElement("option",{value:"5"},"5 Column (42%)"),e.default.createElement("option",{value:"4"},"4 Column (33%)"),e.default.createElement("option",{value:"3"},"3 Column (25%)"),e.default.createElement("option",{value:"2"},"2 Column (16%)"),e.default.createElement("option",{value:"1"},"1 Column (8%)")))),e.default.createElement("div",null,e.default.createElement("label",{style:n.label,htmlFor:"bootstrap-row-section-custom-class"+a},"Custom Class"),e.default.createElement("div",{style:n.wrapper},e.default.createElement("input",{style:n.components,type:"text",id:"bootstrap-row-section-custom-class"+a,value:t.customClass,onChange:e=>((e,t)=>{if(o.onChange){var n=r();n.forEach(((o,n)=>{n==e&&(o.customClass=t)})),o.onChange(n)}})(a,e.currentTarget.value)}))),a>0&&e.default.createElement("button",{style:{...n.button,...n.buttonDanger},onClick:e=>{e.preventDefault(),(e=>{if(o.onChange){var t=r();o.onChange(t.filter(((t,o)=>e!=o)))}})(a)}},"Remove Column")))),e.default.createElement("button",{style:{...n.button,...n.buttonSuccess},onClick:e=>{e.preventDefault(),(()=>{if(o.onChange){var e=r();e.push({size:12,customClass:""}),o.onChange(e)}})()}},"Add Column"))},n={label:{fontFamily:'"GT Walsheim",sans-serif',fontWeight:400,fontSize:14,lineHeight:"16px",color:"var(--color-text-low-emphasis)"},components:{fontFamily:'"Inter",sans-serif',fontWeight:400,fontSize:14,lineHeight:"20px",color:"var(--color-text-default-on-light)",resize:"none",backgroundColor:"var(--color-input-background)",border:"none",padding:"8px 16px",outline:"none",width:"100%"},wrapper:{borderRadius:"20px",border:"1px solid var(--color-input-border)",marginBottom:"5px"},button:{border:"none",padding:"8px 16px",borderRadius:"20px",cursor:"pointer"},buttonSuccess:{backgroundColor:"green",color:"white"},buttonDanger:{backgroundColor:"red",color:"white"},container:{border:"1px lightgray solid",padding:"5px",borderRadius:"10px",margin:"10px auto"}}})(),l})())}}}));