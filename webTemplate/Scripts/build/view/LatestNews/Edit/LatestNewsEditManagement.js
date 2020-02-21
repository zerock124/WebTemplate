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
define(["require", "exports", "vue-property-decorator", "../../Share/Enums", "../service", "moment", "vue2-editor"], function (require, exports, vue_property_decorator_1, Enums_1, service_1, moment, vue2_editor_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    service_1 = __importDefault(service_1);
    vue2_editor_1 = __importDefault(vue2_editor_1);
    vue_property_decorator_1.Vue.use(vue2_editor_1.default);
    var LatestNewsEditManagement = (function (_super) {
        __extends(LatestNewsEditManagement, _super);
        function LatestNewsEditManagement() {
            var _this_1 = _super !== null && _super.apply(this, arguments) || this;
            _this_1.httpURL = window.location.href;
            _this_1.image = '';
            _this_1.LatestNewsItem = null;
            _this_1.PhotoFile = null;
            _this_1.ImageName = '';
            _this_1.LatestNewsEnum = 0;
            _this_1.StartDateTime = moment().startOf('day').format("YYYY-MM-DD HH:mm:ss");
            _this_1.LatestNewsTitle = '';
            _this_1.LatestNewsContent = '';
            _this_1.Remark = '';
            _this_1.Status = false;
            _this_1.Options = [{
                    value: 0,
                    text: '媒體報導'
                }];
            _this_1.LatestNewsId = 0;
            _this_1.DefaultImage = '';
            _this_1.customToolbar = [
                ["bold", "italic", "underline"],
                [{ list: "ordered" },
                    { list: "bullet" }]
            ];
            return _this_1;
        }
        LatestNewsEditManagement.prototype.created = function () {
            var _this = this;
            _this.GetFontHomeId();
            _this.GetFontHomeItem();
        };
        LatestNewsEditManagement.prototype.GetFontHomeId = function () {
            var _this = this;
            var httpURL = window.location.href;
            _this.LatestNewsId = parseInt(httpURL.split("?LatestNewsId=")[1]);
        };
        LatestNewsEditManagement.prototype.GetFontHomeItem = function () {
            var _this = this;
            service_1.default.GetEditLatestNewsItem(_this.LatestNewsId).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Data) {
                    _this.LatestNewsItem = res.Data;
                    _this.ImageName = res.Data.ImageName;
                    _this.StartDateTime = moment(res.Data.StartDateTime).format("YYYY-MM- DD HH:mm:ss");
                    _this.LatestNewsTitle = res.Data.LatestNewsTitle;
                    _this.LatestNewsContent = res.Data.LatestNewsContent;
                    _this.Remark = res.Data.Remark;
                    _this.Status = res.Data.Status;
                    var photo = _this.ImageName;
                    var BasePath = window.BasePath;
                    _this.DefaultImage = BasePath + Enums_1.UrlPathEnum.LatestNewsPhoto + '?filename=' + photo;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        ;
        LatestNewsEditManagement.prototype.fileSelected = function (event) {
            var file = event.target.files.item(0);
            this.ImageName = file.name;
            var reader = new FileReader();
            reader.addEventListener('load', this.imageLoader);
            reader.readAsDataURL(file);
        };
        ;
        LatestNewsEditManagement.prototype.imageLoader = function (event) {
            this.image = event.target.result;
        };
        LatestNewsEditManagement.prototype.SetEditFontHome = function () {
            var _this = this;
            if (_this.LatestNewsItem) {
                var _a = this, LatestNewsId = _a.LatestNewsId, PhotoFile = _a.PhotoFile, ImageName = _a.ImageName, LatestNewsEnum = _a.LatestNewsEnum, StartDateTime = _a.StartDateTime, LatestNewsTitle = _a.LatestNewsTitle, LatestNewsContent = _a.LatestNewsContent, Status = _a.Status, Remark = _a.Remark;
                var _formdata = new FormData();
                _formdata.append('LatestNewsId', LatestNewsId.toString());
                _formdata.append('PhotoFile', PhotoFile ? PhotoFile : '');
                _formdata.append('ImageName', ImageName);
                _formdata.append('LatestNewsEnum', LatestNewsEnum.toString());
                _formdata.append('StartDateTime', StartDateTime);
                _formdata.append('LatestNewsTitle', LatestNewsTitle);
                _formdata.append('LatestNewsContent', LatestNewsContent);
                _formdata.append('Remark', Remark);
                _formdata.append('Status', JSON.stringify(Status));
                _this.EditFontHome(_formdata);
            }
        };
        LatestNewsEditManagement.prototype.EditFontHome = function (data) {
            var _this_1 = this;
            service_1.default.EditLatestNewsItem(data).then(function (res) {
                if (!res.Success) {
                    console.log(res);
                }
                if (res.Success) {
                    var locationURL = _this_1.httpURL.split("/Edit?")[0];
                    console.log(locationURL);
                    document.location.href = locationURL;
                }
            }).catch(function (err) {
                console.log(err);
            });
        };
        LatestNewsEditManagement = __decorate([
            vue_property_decorator_1.Component({
                template: '#LatestNewsEditManagement'
            })
        ], LatestNewsEditManagement);
        return LatestNewsEditManagement;
    }(vue_property_decorator_1.Vue));
    exports.default = LatestNewsEditManagement;
});
//# sourceMappingURL=LatestNewsEditManagement.js.map