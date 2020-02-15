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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums"], function (require, exports, vue_property_decorator_1, Enums_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var CaseCreateManagement = (function (_super) {
        __extends(CaseCreateManagement, _super);
        function CaseCreateManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.image = '';
            _this_1.PhotoFile = null;
            _this_1.DefaultImage = '';
            _this_1.ImageName = '';
            _this_1.CaseEnum = 0;
            _this_1.Options = [{
                    value: 0,
                    text: '行銷活動'
                }, {
                    value: 1,
                    text: '臉書機器人'
                }, {
                    value: 2,
                    text: '官網'
                }];
            _this_1.CaseUrl = '';
            _this_1.CaseName = '';
            _this_1.CaseContent = '';
            _this_1.Status = false;
            return _this_1;
        }
        CaseCreateManagement.prototype.created = function () {
            var _this = this;
            _this.GetDefaultCaseUrl();
        };
        CaseCreateManagement.prototype.GetDefaultCaseUrl = function () {
            var _this = this;
            var BasePath = window.BasePath;
            var photo = "NoImage.jpg";
            _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.LatestNewsPhoto + '?filename=' + photo;
        };
        ;
        CaseCreateManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            console.log(file);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        CaseCreateManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        CaseCreateManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#CaseCreateManagement'
            })
        ], CaseCreateManagement);
        return CaseCreateManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = CaseCreateManagement;
});
//# sourceMappingURL=CaseCreateManagement.js.map