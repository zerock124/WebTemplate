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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums", "../service", "moment"], function (require, exports, vue_property_decorator_1, Enums_1, service_1, moment) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    var FontHomeEditManagement = (function (_super) {
        __extends(FontHomeEditManagement, _super);
        function FontHomeEditManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.image = '';
            _this_1.FontHomeItem = null;
            _this_1.PhotoFile = null;
            _this_1.ImageName = '';
            _this_1.FontHomeUrl = '';
            _this_1.StartDateTime = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.EndDateTime = moment().endOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.Remark = '';
            _this_1.Status = false;
            _this_1.FontHomeId = 0;
            _this_1.FontHomeImage = '';
            _this_1.SaveForm = 'Loading';
            return _this_1;
        }
        FontHomeEditManagement.prototype.created = function () {
            var _this = this;
            _this.GetFontHomeId();
            _this.GetFontHomeItem();
        };
        FontHomeEditManagement.prototype.GetFontHomeId = function () {
            var _this = this;
            var httpURL = window.location.href;
            _this.FontHomeId = parseInt(httpURL.split("?FontHomeId=")[1]);
        };
        FontHomeEditManagement.prototype.GetFontHomeItem = function () {
            var _this = this;
            service_1.default.GetEditFontHomeItem(_this.FontHomeId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.FontHomeItem = res.Data;
                    _this.ImageName = res.Data.ImageName;
                    _this.FontHomeUrl = res.Data.FontHomeUrl;
                    _this.StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM- DD HH:mm:ss");
                    _this.EndDateTime = moment(res.Data.EndDateTime).format("YYYY-MM- DD HH:mm:ss");
                    _this.Remark = res.Data.Remark;
                    _this.Status = res.Data.Status;
                    var photo = _this.ImageName;
                    var BasePath = window.BasePath;
                    _this.FontHomeImage = BasePath + Enums_1.UrlPathEnum.FontHomePhoto + '?filename=' + photo;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ;
        FontHomeEditManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            console.log(file);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        FontHomeEditManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        FontHomeEditManagement.prototype.SetEditFontHome = function () {
            var _this = this;
            _this.$bvModal.show('FontHomeModal');
            _this.SaveForm = 'Loading';
            if (_this.FontHomeItem) {
                var _a = this, FontHomeId = _a.FontHomeId, PhotoFile = _a.PhotoFile, ImageName = _a.ImageName, FontHomeUrl = _a.FontHomeUrl, StartDateTime = _a.StartDateTime, EndDateTime = _a.EndDateTime, Status = _a.Status, Remark = _a.Remark;
                var _formdata = new FormData();
                _formdata.append('FontHomeId', FontHomeId.toString());
                _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '');
                _formdata.append('ImageName', ImageName);
                _formdata.append('FontHomeUrl', FontHomeUrl);
                _formdata.append('StartDateTime', StartDateTime);
                _formdata.append('EndDateTime', EndDateTime);
                _formdata.append('Remark', Remark);
                _formdata.append('Status', JSON.stringify(Status));
                _this.EditFontHome(_formdata);
            }
        };
        FontHomeEditManagement.prototype.EditFontHome = function (data) {
            var _this = this;
            service_1.default.EditFontHomeItem(data).then(function (res) {
                console.log(res);
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
        };
        FontHomeEditManagement.prototype.HideModal = function () {
            var _this = this;
            _this.$bvModal.hide('FontHomeModal');
        };
        FontHomeEditManagement.prototype.CloseModal = function () {
            var _this = this;
            _this.$bvModal.hide('FontHomeModal');
            var locationURL = this.httpURL.split("/GetEditFontHome")[0];
            document.location.href = locationURL;
        };
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