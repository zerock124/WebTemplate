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
define(["require", "exports", "vue-property-decorator", "../service", "vue2-editor"], function (require, exports, vue_property_decorator_1, service_1, vue2_editor_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    vue2_editor_1 = __importDefault(vue2_editor_1);
    vue_property_decorator_1.Vue.use(vue2_editor_1.default);
    var ContactEditManagement = (function (_super) {
        __extends(ContactEditManagement, _super);
        function ContactEditManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.ContactItem = null;
            _this_1.ContactId = 0;
            _this_1.SexOptions = [
                { value: 0, text: '先生' },
                { value: 1, text: '小姐' }
            ];
            _this_1.EnumOptions = [
                { value: 0, text: '網站建置' },
                { value: 1, text: '聊天機器人' },
                { value: 2, text: '客製化抽獎' },
                { value: 3, text: '其它' }
            ];
            _this_1.StatusOptions = [
                { value: 0, text: '全新專案' },
                { value: 1, text: '改版增修' }
            ];
            _this_1.customToolbar = [
                ["bold", "italic", "underline"],
                [{ list: "ordered" },
                    { list: "bullet" }]
            ];
            _this_1.SaveForm = 'Loading';
            return _this_1;
        }
        ContactEditManagement.prototype.created = function () {
            var _this = this;
            _this.GetContactId();
            _this.GetContactItem();
        };
        ContactEditManagement.prototype.GetContactId = function () {
            var _this = this;
            var httpURL = window.location.href;
            _this.ContactId = parseInt(httpURL.split("?ContactId=")[1]);
        };
        ContactEditManagement.prototype.GetContactItem = function () {
            var _this = this;
            service_1.default.GetEditContactItem(_this.ContactId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ContactItem = res.Data;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ;
        ContactEditManagement.prototype.SetEditFontHome = function () {
            var _this = this;
            _this.$bvModal.show('ContactModal');
            _this.SaveForm = 'Loading';
            if (_this.ContactItem) {
                service_1.default.EditContactItem(_this.ContactItem).then(function (res) {
                    if (!res.Success) {
                        _this.SaveForm = 'Error';
                        console.log(res);
                    }
                    if (res.Success) {
                        _this.SaveForm = 'Success';
                    }
                }).catch(function (err) {
                    _this.SaveForm = 'Error';
                    console.log(err);
                });
            }
        };
        ContactEditManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('ContactModal');
        };
        ContactEditManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('ContactModal');
            var locationURL = this.httpURL.split("/Edit?")[0];
            document.location.href = locationURL;
        };
        ContactEditManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#ContactEditManagement'
            })
        ], ContactEditManagement);
        return ContactEditManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = ContactEditManagement;
});
//# sourceMappingURL=ContactEditManagement.js.map