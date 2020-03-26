var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
define(["require", "exports", "vue-property-decorator", "./service", "vue2-editor", "quill-image-resize-module"], function (require, exports, vue_property_decorator_1, service_1, vue2_editor_1, quill_image_resize_module_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    vue2_editor_1 = __importStar(vue2_editor_1);
    quill_image_resize_module_1 = __importDefault(quill_image_resize_module_1);
    vue2_editor_1.Quill.register('modules/imageResize', quill_image_resize_module_1.default);
    vue_property_decorator_1.Vue.use(vue2_editor_1.default);
    var icons = vue2_editor_1.Quill.import('ui/icons');
    icons['html'] = '<i class="fas fa-code"></i>';
    var StaticPageItem = (function (_super) {
        __extends(StaticPageItem, _super);
        function StaticPageItem() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.PageContent = '';
            _this_1.StaticPageItem = null;
            _this_1.editorOption = {};
            _this_1.htmlFromEditor = true;
            _this_1.customToolbar = [[{
                        header: [false, 1, 2, 3, 4, 5, 6]
                    }], ["bold", "italic", "underline", "strike"],
                [{
                        align: ""
                    }, {
                        align: "center"
                    }, {
                        align: "right"
                    }, {
                        align: "justify"
                    }], ["blockquote"], [{
                        list: "ordered"
                    }, {
                        list: "bullet"
                    }, {
                        list: "check"
                    }], [{
                        indent: "-1"
                    }, {
                        indent: "+1"
                    }],
                [{
                        color: []
                    }, {
                        background: []
                    }],
                ["link", "image", "video"], ["clean"], ["html"]
            ];
            return _this_1;
        }
        StaticPageItem.prototype.created = function () {
            var _this = this;
            _this.SetEditorOption();
        };
        StaticPageItem.prototype.mounted = function () {
            var _this = this;
            var htmlButton = _this.$el.querySelector('.ql-html');
            if (htmlButton != null) {
                htmlButton.addEventListener('click', function () {
                    var htmlEditor = _this.$el.querySelector('.ql-html-editor');
                    if (htmlEditor) {
                        var qlEditor = _this.$el.querySelector(".ql-editor");
                        if (qlEditor) {
                            var qlHTMLEditor = $('.ql-html-editor').val();
                            if (qlHTMLEditor) {
                                var qlChange = qlHTMLEditor.toString();
                                qlHTMLEditor = qlChange.replace(/\n/g, "");
                                qlEditor.innerHTML = qlHTMLEditor;
                            }
                            var qlContainerEditor = _this.$el.querySelector(".ql-container");
                            if (qlContainerEditor) {
                                qlContainerEditor.removeChild(htmlEditor);
                            }
                        }
                    }
                    else {
                        htmlEditor = document.createElement("textarea");
                        htmlEditor.className = 'ql-editor ql-html-editor';
                        var qlEditor = _this.$el.querySelector(".ql-editor");
                        if (qlEditor) {
                            var qlHTMLEditor = qlEditor.innerHTML;
                            htmlEditor.innerHTML = qlHTMLEditor.replace(/\n\n/g, "\n");
                            var qlContainerEditor = _this.$el.querySelector(".ql-container");
                            if (qlContainerEditor) {
                                qlContainerEditor.appendChild(htmlEditor);
                            }
                        }
                    }
                    ;
                });
            }
            ;
        };
        StaticPageItem.prototype.SetEditorOption = function () {
            var _this = this;
            _this.editorOption = {
                modules: {
                    imageResize: {
                        displaySize: true
                    }
                }
            };
        };
        StaticPageItem.prototype.SubmitCompanyProfile = function () {
            var _this = this;
            _this.SubmitStaticPage(_this.PageContent);
        };
        StaticPageItem.prototype.SubmitStaticPage = function (data) {
            var _this = this;
            if (_this.ListItem) {
                _this.ListItem.PageContent = data;
                service_1.default.EditStaticPage(_this.ListItem).then(function (res) {
                    if (!res.Success) {
                        console.log(res);
                    }
                    if (res.Data) {
                        _this.$bvToast.toast("變更'公司介紹'成功", {
                            title: '變更靜態頁',
                            autoHideDelay: 50,
                            variant: 'success'
                        });
                    }
                }).catch(function (err) {
                    console.log(err);
                });
            }
        };
        StaticPageItem.prototype.OnListItemChange = function () {
            var _this = this;
            if (_this.ListItem) {
                _this.PageContent = _this.ListItem.PageContent;
            }
        };
        __decorate([
            vue_property_decorator_1.Prop(Object)
        ], StaticPageItem.prototype, "ListItem", void 0);
        __decorate([
            vue_property_decorator_1.Watch('ListItem')
        ], StaticPageItem.prototype, "OnListItemChange", null);
        StaticPageItem = __decorate([
            vue_property_decorator_1.Component({
                template: '#StaticPageItem'
            })
        ], StaticPageItem);
        return StaticPageItem;
    }(vue_property_decorator_1.Vue));
    exports.default = StaticPageItem;
});
//# sourceMappingURL=StaticPageItem.js.map