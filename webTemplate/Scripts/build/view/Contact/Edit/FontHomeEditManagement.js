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
    var FontHomeEditManagement = (function (_super) {
        __extends(FontHomeEditManagement, _super);
        function FontHomeEditManagement() {
            var _this = _super !== null && _super.apply(this, arguments) || this;
            _this.httpURL = window.location.href;
            _this.ContactItem = null;
            _this.ContactId = 0;
            _this.CompanyName = '';
            _this.Name = '';
            _this.Sex = 0;
            _this.SexOptions = [
                { value: 0, text: '先生' },
                { value: 1, text: '小姐' }
            ];
            _this.Email = '';
            _this.ContactPhone = '';
            _this.ContactEnum = 0;
            _this.EnumOptions = [
                { value: 0, text: '網站建置' },
                { value: 1, text: '聊天機器人' },
                { value: 2, text: '客製化抽獎' },
                { value: 3, text: '其它' }
            ];
            _this.ContactStatus = 0;
            _this.StatusOptions = [
                { value: 0, text: '全新專案' },
                { value: 1, text: '改版增修' }
            ];
            _this.Budget = '';
            _this.OnlineDate = '';
            _this.Demand = '';
            _this.Remark = '';
            _this.customToolbar = [
                ["bold", "italic", "underline"],
                [{ list: "ordered" },
                    { list: "bullet" }]
            ];
            return _this;
        }
        FontHomeEditManagement.prototype.created = function () {
            var _this = this;
            _this.GetContactId();
            _this.GetContactItem();
        };
        FontHomeEditManagement.prototype.GetContactId = function () {
            var _this = this;
            var httpURL = window.location.href;
            _this.ContactId = parseInt(httpURL.split("?ContactId=")[1]);
        };
        FontHomeEditManagement.prototype.GetContactItem = function () {
            var _this = this;
            service_1.default.GetEditContactItem(_this.ContactId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.ContactItem = res.Data;
                    _this.CompanyName = res.Data.CompanyName;
                    _this.Name = res.Data.Name;
                    _this.Sex = res.Data.Sex;
                    _this.Email = res.Data.Email;
                    _this.Remark = res.Data.Remark;
                    _this.ContactPhone = res.Data.ContactPhone;
                    _this.ContactEnum = res.Data.ContactEnum;
                    _this.ContactStatus = res.Data.ContactStatus;
                    _this.Budget = res.Data.Budget;
                    _this.OnlineDate = res.Data.OnlineDate;
                    _this.Demand = res.Data.Demand;
                    _this.Remark = res.Data.Remark;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ;
        FontHomeEditManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#FontHomeEditManagement'
            })
        ], FontHomeEditManagement);
        return FontHomeEditManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = FontHomeEditManagement;
});
//# sourceMappingURL=FontHomeEditManagement.js.map