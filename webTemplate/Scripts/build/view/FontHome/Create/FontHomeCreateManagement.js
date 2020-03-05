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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums", "moment", "../service"], function (require, exports, vue_property_decorator_1, Enums_1, moment, service_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var FontHomeCreateManagement = (function (_super) {
        __extends(FontHomeCreateManagement, _super);
        function FontHomeCreateManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.image = '';
            _this_1.PhotoFile = null;
            _this_1.ImageName = '';
            _this_1.FontHomeUrl = '';
            _this_1.StartDateTime = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.EndDateTime = moment().endOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.Remark = '';
            _this_1.Status = false;
            _this_1.DefaultImage = '';
            _this_1.SaveForm = 'Loading';
            return _this_1;
        }
        FontHomeCreateManagement.prototype.created = function () {
            var _this = this;
            _this.GetDefaultFontHomeUrl();
        };
        FontHomeCreateManagement.prototype.GetDefaultFontHomeUrl = function () {
            var _this = this;
            var BasePath = window.BasePath;
            var photo = "NoImage.jpg";
            _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.FontHomePhoto + '?filename=' + photo;
        };
        ;
        FontHomeCreateManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            console.log(file);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        FontHomeCreateManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        FontHomeCreateManagement.prototype.SetCreateFontHome = function () {
            var _this = this;
            _this.$bvModal.show('FontHomeModal');
            _this.SaveForm = 'Loading';
            var _a = this, PhotoFile = _a.PhotoFile, ImageName = _a.ImageName, FontHomeUrl = _a.FontHomeUrl, StartDateTime = _a.StartDateTime, EndDateTime = _a.EndDateTime, Remark = _a.Remark, Status = _a.Status;
            var _formdata = new FormData();
            _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '');
            _formdata.append('ImageName', ImageName);
            _formdata.append('FontHomeUrl', FontHomeUrl);
            _formdata.append('StartDateTime', StartDateTime);
            _formdata.append('EndDateTime', EndDateTime);
            _formdata.append('Remark', Remark);
            _formdata.append('Status', JSON.stringify(Status));
            _this.CreateFontHome(_formdata);
        };
        FontHomeCreateManagement.prototype.CreateFontHome = function (data) {
            var _this = this;
            service_1.default.CreateFontHome(data).then(function (res) {
                if (!res.Success) {
                    _this.SaveForm = 'Error';
                    console.log(res);
                }
                if (res.Data) {
                    _this.SaveForm = 'Success';
                }
            }).catch(function (err) {
                _this.SaveForm = 'Error';
                console.log(err);
            });
        };
        FontHomeCreateManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('FontHomeModal');
        };
        FontHomeCreateManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('FontHomeModal');
            var locationURL = this.httpURL.split("/Create")[0];
            document.location.href = locationURL;
        };
        FontHomeCreateManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#FontHomeCreateManagement'
            })
        ], FontHomeCreateManagement);
        return FontHomeCreateManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = FontHomeCreateManagement;
});
//# sourceMappingURL=FontHomeCreateManagement.js.map